# Phase 1: Foundation — Authentication

**Ziel**: Spieler kann sich registrieren, einloggen und validiert sein

**TL;DR**: JWT-basierte Auth mit Username/Passwort/Email + Refresh-Tokens in Redis. OAuth-vorbereitet (ProviderType-Feld). Rollen von Anfang an (neue User = Player). Andere Services nutzen JWT-Claims, nicht direct User-DB.

---

## Phase A: Datenmodell & Enums

### 1.1 User Entity anpassen

- Felder hinzufügen: `Email` (unique, required), `PasswordHash` (wird gefüllt), `EUserRole` (Enum-referenz), `ProviderType` (für OAuth-Vorbereitung), `ProviderUserId` (Google ID später)
- Migrations: neuer Index auf `Email` (unique), `(ProviderType, ProviderUserId)` (für OAuth lookup)

**Files**:

- `Domain/Entities/User.cs` — Entity erweitern
- `Data/Contexts/BISDbContext.cs` — Constraints/Indices definieren

### 1.2 EUserRole Enum erstellen

- Werte: `Player`, `Admin`, `Moderator` (später erweiterbar)
- File: `Common/Enums/EUserRole.cs`

### 1.3 RefreshToken Entity (Redis)

- Struktur: `UserId`, `Token`, `ExpiresAt`, `CreatedAt`
- Nicht in PostgreSQL (volatile), in Redis mit TTL
- File: `Common/Models/RefreshTokenModel.cs` (POCO für Redis Serialization)

---

## Phase B: DTOs & Services

### 2.1 DTOs erstellen

- `LoginRequest`: `{ username, password }` → `AuthResponse`
- `RegisterRequest`: `{ username, email, password }` → `AuthResponse`
- `AuthResponse`: `{ token (JWT), refreshToken, user: UserDTO, expiresIn }`
- `UserDTO`: `{ id, username, email, role, createdAt }`
- `RefreshTokenRequest`: `{ refreshToken }` → `AuthResponse`
- File: `Common/DTOs/Auth/`

### 2.2 IAuthService + AuthService

#### Login

- Username + Password → JWT + Refresh-Token
- Validate credentials (case-insensitive username)
- Generate JWT (exp: 15 min)
- Store Refresh-Token in Redis (exp: 7 days, TTL)
- Log audit trail (optional Phase 4)

#### Register

- Username + Email + Password → JWT + Refresh-Token
- Validate: username/email unique (check DB)
- Hash password (Bcrypt, `cost=12`)
- Create User with `EUserRole.Player` default + `ProviderType: Native`
- Auto-login: return JWT + Refresh-Token immediately

#### Refresh

- Refresh-Token → new JWT + new Refresh-Token
- Validate refresh token from Redis (signature + expiration)
- Issue new JWT with same claims
- Rotate refresh token (delete old, store new in Redis) — security best practice
- Return new token pair

#### ValidateToken

- JWT string → ClaimsPrincipal or null
- Used by other services to extract User ID from token
- No DB roundtrip (all claims in JWT)

**Files**:

- `Common/Interfaces/IAuthService.cs`
- `AuthService/Services/AuthService.cs`
- `Common/Utils/PasswordHasher.cs` (Bcrypt wrapper, BCrypt.Net-Next NuGet)
- `Common/Utils/JwtTokenGenerator.cs` (JWT creation/validation, System.IdentityModel.Tokens.Jwt)

### 2.3 DI Registration

- `AddAuthServices()` in `Common/Extensions/ServiceExtensions.cs`
  - `IAuthService` → `AuthService`
  - `IRepository<User>` → `Repository<User>`
  - JWT TokenValidationParameters (from config)
  - Redis IConnectionMultiplexer for refresh tokens

---

## Phase C: API Endpoints

### 3.1 AuthController

```csharp
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // POST /api/auth/register
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    
    // POST /api/auth/login
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    
    // POST /api/auth/refresh
    public async Task<ActionResult<AuthResponse>> Refresh(RefreshTokenRequest request)
    
    // POST /api/auth/logout
    public async Task<IActionResult> Logout()
    
    // GET /api/auth/validate (protected)
    [Authorize]
    public async Task<ActionResult<UserDTO>> Validate()
}
```

**Responses**:

- `/register` → 201 Created
- `/login` → 200 OK
- `/refresh` → 200 OK (or 401 if token invalid/expired)
- `/logout` → 200 OK (or 204 No Content)
- `/validate` → 200 OK (or 401 if not authenticated)

### 3.2 Authorization Middleware

- Extract JWT from `Authorization: Bearer {token}`
- Validate signature + expiration using TokenValidationParameters
- Populate HttpContext.User with claims
- Already in ASP.NET Core, just needs `AddAuthentication("Bearer")` + `.AddJwtBearer()` setup

---

## Phase D: Configuration & Security

### 4.1 appsettings.Base.json (template)

```json
{
  "Jwt": {
    "Secret": "your-secret-key-min-32-chars-set-in-env",
    "Issuer": "bis-auth-service",
    "Audience": "bis-clients",
    "ExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  },
  "Redis": {
    "Host": "redis",
    "Port": 6379
  }
}
```

**Environment variables** (production):

- `JWT_SECRET` → loaded via `Configuration["Jwt:Secret"]` (or IOptions<JwtSettings>)
- Min. 32 characters, random string (openssl rand -base64 32)

### 4.2 Security Best Practices

- **Password**: Never log or echo, hash with Bcrypt immediately upon receive
- **JWT Secret**: Store in environment variable (not git), min 32 chars, rotate annually
- **CORS**: Already set to AllowAll, but ensure auth headers are allowed (`Authorization`)
- **Refresh Token**: Stored in response body (works for mobile/Unity), not HTTP-Only cookie
- **Token Rotation**: Old refresh token deleted when new one issued (prevents replay)
- **Rate Limiting**: TODO Phase 4 (not now, but plan for it: middleware on /auth/login, /auth/register)
- **HTTPS**: Required in production (nginx.conf handles SSL termination)

---

## Phase E: OAuth Preparation (Extensibility)

### 5.1 Database Structure (now)

- `User.ProviderType`: `enum { Native, Google, Discord, ... }`
- `User.ProviderUserId`: nullable string (Google user ID, or null for Native)
- `User.Email`: unique index (shared across all provider types)
- Default on Register: `ProviderType = Native`, `ProviderUserId = null`

### 5.2 Login Flow Adaptation (Phase 3, after OAuth library)

```
OAuth (Google) login request
  → extract email from provider
  → check if User.Email exists
  → if not: create User with ProviderType=Google, ProviderUserId=<google_id>
  → if yes: check ProviderType (if already Google, OK; if Native, error or migrate)
  → issue JWT + Refresh-Token (same as native login)
```

---

## Testing & Verification

### Unit Tests (AuthService.Tests)

- `Register_ValidInput_CreatesUserAndReturnsJwt()`
- `Register_DuplicateEmail_ReturnsBadRequest()`
- `Register_DuplicateUsername_ReturnsBadRequest()`
- `Login_ValidCredentials_ReturnsJwt()`
- `Login_InvalidPassword_ReturnsUnauthorized()`
- `Login_UserNotFound_ReturnsUnauthorized()`
- `RefreshToken_ValidToken_IssuesNewJwt()`
- `RefreshToken_ExpiredToken_ReturnsUnauthorized()`
- `RefreshToken_InvalidSignature_ReturnsUnauthorized()`
- `ValidateToken_ValidJwt_ReturnsClaimsPrincipal()`
- `ValidateToken_ExpiredJwt_ReturnsNull()`

### Integration Tests

- `POST /api/auth/register` → 201 Created, JWT in response body
- `POST /api/auth/login` → 200 OK, Refresh-Token stored in Redis with TTL
- `GET /api/auth/validate` (with Bearer token) → 200 OK, returns UserDTO
- `POST /api/auth/refresh` → 200 OK, new JWT + new refresh token
- `POST /api/auth/logout` → 200 OK, refresh token removed from Redis

### Manual Testing (Postman)

1. POST /api/auth/register → get JWT + refresh token
2. POST /api/auth/login → get JWT + refresh token
3. GET /api/auth/validate with Bearer token → see current user
4. POST /api/auth/refresh → get new JWT
5. JWT decode (jwt.io) → verify `sub` (userId), `role` (Player), `iss` (bis-auth-service)
6. Redis: `redis-cli` → `KEYS *refresh-token*` → should exist with TTL

---

## Files to Create/Modify

### New Files (9 total)

**Enums**:

- `backend/src/Common/Enums/EUserRole.cs`

**DTOs**:

- `backend/src/Common/DTOs/Auth/LoginRequest.cs`
- `backend/src/Common/DTOs/Auth/RegisterRequest.cs`
- `backend/src/Common/DTOs/Auth/AuthResponse.cs`
- `backend/src/Common/DTOs/Auth/UserDTO.cs`
- `backend/src/Common/DTOs/Auth/RefreshTokenRequest.cs`

**Interfaces & Models**:

- `backend/src/Common/Interfaces/IAuthService.cs`
- `backend/src/Common/Models/RefreshTokenModel.cs`

**Utilities**:

- `backend/src/Common/Utils/PasswordHasher.cs`
- `backend/src/Common/Utils/JwtTokenGenerator.cs`

**Service Implementation**:

- `backend/src/AuthService/Services/AuthService.cs`

**API Controller**:

- `backend/src/AuthService/Controllers/AuthController.cs`

**Tests**:

- `backend/tests/AuthService.Tests/AuthServiceTests.cs`

### Modified Files (5 total)

**Domain/Entities**:

- `backend/src/Domain/Entities/User.cs` — add Email, ProviderType, ProviderUserId fields, add navigation if needed

**Data Context**:

- `backend/src/Data/Contexts/BISDbContext.cs` — add unique index on Email, compound index on (ProviderType, ProviderUserId)

**DI Registration**:

- `backend/src/Common/Extensions/ServiceExtensions.cs` — add `AddAuthServices(this IServiceCollection services, IConfiguration config)` method

**Service Setup** (no code changes, just wiring):

- `backend/src/AuthService/Program.cs` — already has `builder.Services.AddAuthServices(config);` call, just verify it's wired

**Configuration**:

- `backend/src/Common/appsettings.Base.json` — add Jwt section template (Secret as placeholder)

---

## Decisions & Rationale

| Decision | Why |
|----------|-----|
| **JWT + Refresh-Token (Redis)** | Stateless JWT for other services; refresh tokens in Redis allow revocation & rotation later without DB roundtrip |
| **Bcrypt (not Argon2)** | Standard in .NET ecosystem, available via BCrypt.Net-Next NuGet, proven, no need for PBKDF2 complexity yet |
| **OAuth fields on User (not separate table)** | Simpler now; nullable fields don't hurt schema; can migrate to separate `OAuth` table later if many providers |
| **Auto-login on register** | Better UX; user doesn't need to click "login" after signup, immediate token issuance |
| **Refresh token rotation** | Security best practice; old token invalidated when new one issued, limits damage if token stolen |
| **JWT in response body (not HttpOnly cookie)** | Works for mobile (Unity) and web clients; cookies would require CORS + SameSite complexity |
| **Roles from start** | Admin/Moderator added later, but role system ready now; simplifies auth checks in other services |
| **No email verification (Phase 1)** | Email-send integration too complex for foundation phase; add in Phase 2 when email service available |

---

## Later Extensions (Post-Phase 1)

1. **Google OAuth** (Phase 3) — new `/auth/google-login` endpoint, reuses Register logic with `ProviderType=Google`
2. **Email Verification** (Phase 2) — add `IsEmailVerified` flag, send verify link (requires email/SMTP service)
3. **2FA** (Phase 4) — optional, add TOTP secret column to User
4. **Rate Limiting** (Phase 4) — middleware on /auth/login, /auth/register (e.g., 5 failed attempts per IP = 15 min lockout)
5. **Audit Logging** (Phase 4) → separate table: login attempts, failed logins, IP logging, device fingerprints
6. **Password Reset** (Phase 2+) → email + secure token flow with expiration
7. **SSO across services** (Phase 3+) → share JWT secret + signing key across all services, each validates independently

---

## Timeline Estimate

| Phase | Task | Days |
|-------|------|------|
| **Prep** | Domain setup (User fields, EUserRole, indices) | 0.5 |
| **B** | DTOs, IAuthService interface | 0.5 |
| **B** | PasswordHasher, JwtTokenGenerator utilities | 1 |
| **B** | AuthService implementation (register, login, refresh, validate) | 1.5 |
| **D** | Config & appsettings setup | 0.5 |
| **C** | AuthController endpoints (all 5 routes) | 0.5 |
| **Testing** | Unit tests (10 test cases) | 1 |
| **Testing** | Integration tests + manual Postman | 1 |
| **Buffer** | Debugging, schema fixes | 1 |
| **TOTAL** | | **~7 days** |

---

## Blockers & Notes

- ✅ Base project structure ready (bootstrap pattern, DI, repositories)
- ✅ PostgreSQL + Redis infrastructure ready (docker-compose)
- ⚠️ NuGet dependencies needed: `BCrypt.Net-Next`, `System.IdentityModel.Tokens.Jwt`, `StackExchange.Redis`
- ⚠️ JWT Secret must be set in environment before running (won't work with placeholder)
- ⚠️ Redis must be healthy (check `docker-compose ps`) before Refresh endpoint is used

---

## Sign-off

**Author**: Phase 1 Foundation Plan  
**Created**: 2026-04-24  
**Status**: Ready for Implementation  
**Next Step**: Start with Phase A (Domain layer modifications)
