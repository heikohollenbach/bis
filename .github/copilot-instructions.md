---
name: copilot-instructions
description: "Workspace instructions for the project. Guides AI assistance"
---

## Priority Rules

- **Always apply user-level instructions from `~/.copilot/instructions/*` first.**
- Keep changes minimal and aligned with existing service patterns.
- Prefer links to project docs over re-stating large blocks of documentation.

## General Guidelines

- Use emojis for task status
  - 🔘 unknwon, not started, open
  - 🟣 work in progress, started
  - 🟢 done
  - 🔴 blocked, needs attention
  - 🟡 on hold, waiting for input, planed,
  - 🟤 won't do, rejected, closed
  - 🟠 duplicate, invalid
  - ⚫ other
- Use markdown formatting for readability (headings, lists, code blocks).
- When referencing project documentation, link to the relevant file and section instead of copying content.

## Project Context

`BIS` is a persistent Space-MMORPG backend built with .NET 10.

- Vision and roadmap: [plan/README.md](../plan/README.md)
- Product overview: [plan/00-overview.md](../plan/00-overview.md)
- Architecture decisions: [plan/01-architecture.md](../plan/01-architecture.md)
- Project layout: [plan/03-project-structure.md](../plan/03-project-structure.md)

Tech stack: ASP.NET Core 10, EF Core + PostgreSQL, Redis, SignalR, Docker Compose, Blazor (WebService + Radzen).

## Build, Test, Run

Use these as the default commands:

```powershell
dotnet build backend
dotnet test backend
cd backend; docker-compose up
```

For service-only local runs:

```powershell
cd backend/src/AuthService; dotnet run
cd backend/src/PlayerService; dotnet run
cd backend/src/WorldService; dotnet run
cd backend/src/ChatService; dotnet run
cd backend/src/WebService; dotnet run
```

## Service Boundaries

- `AuthService`: JWT auth, users, sessions (`bis_auth`)
- `PlayerService`: ships, inventory, progression (`bis_player`)
- `WorldService`: zones, combat, NPC/world logic (`bis_world`)
- `ChatService`: messaging (`bis_chat`)
- `WebService`: Blazor server UI (no dedicated DB)

Shared projects:

- `Common`: shared extensions, logging, CORS, constants, cross-service utilities
- `Domain`: entities and domain model
- `Data`: `BISDbContext` and repository implementations

## Program.cs Patterns

API services (`AuthService`, `PlayerService`, `WorldService`, `ChatService`) should keep this shape:

```csharp
builder.Configuration.AddBaseConfiguration();
builder.AddBISLogging();
builder.Services.AddBISApiServices();
builder.Services.Add{Service}Services(builder.Configuration);

app.UseBISApiPipeline();
app.RunWithLogging("ServiceName");
```

`WebService` is intentionally different (Blazor pipeline). Current implementation uses `AddRazorComponents().AddInteractiveServerComponents()` and `AddRadzenComponents()`.

## Code Conventions (Project-Specific)

- Namespace root: `BIS.{ProjectName}`
- Naming: PascalCase for public members, camelCase for locals/private fields, `I` prefix for interfaces, `E` prefix for enums
- Null checks: `is null` / `is not null` only
- Style: file-scoped namespaces, single-line `using`, newline before `{`
- Public controller/service methods require XML doc comments
- Prefer `nameof`, pattern matching, switch expressions, and async I/O end-to-end
- Controllers must not access repositories directly; go through services

Note: Current codebase entities are plain names like `User`, `Ship`, `Resource` (do not force `BO` suffix where not already used).

## Coding Conventions

- **Classes / Methods / Public members**: `PascalCase` (e.g. `MachineService`)
- **Private fields / Local variables**: `camelCase` (e.g. `_machineRepository`)
- **Interfaces**: `I` prefix (e.g. `ICoreMachineService`)
- **Enums**: `E` prefix (e.g. `EUserRole`)
- **Entities**: `BO` suffix (e.g. `MachineBO`)
- **Transfer objects**: `DTO` suffix (e.g. `MachineDTO`)
- **Namespace root**: `BIS.{ProjectName}`
- **File naming**: Match the class name (e.g. `MachineService.cs` for `MachineService` class)
- **Use `var`** when the type is obvious from the right-hand side (e.g. `var machine = new Machine();`), otherwise use explicit types (e.g. `List<Machine> machines = GetMachines();`).
- **Use `nameof`** instead of string literals when referring to member names (e.g. `nameof(MachineService)` instead of `"MachineService"`).
- **Use pattern matching** and switch expressions wherever possible (e.g. `if (obj is Machine machine) { ... }`).
- **Use file-scoped namespaces** and single-line using directives for cleaner code (e.g. `namespace BIS.MachineService;`).

**Nullability**:

- Nullable reference types are enabled everywhere — declare non-nullable by default.
- Use `is null` / `is not null`; never `== null` or `!= null`.
- Add null checks only at system boundaries (controller input, external APIs).

**Style**:

- File-scoped namespaces, single-line `using` directives.
- Newline before every opening `{`.
- XML doc comments on all public controller and public service methods.
- `nameof()` instead of string literals for member references.
- Pattern matching and switch expressions preferred over chains of `if`/`else`.

## Service & Repository Pattern

- Always define an interface; register both in DI via `AddRepositories()` / `AddServices()` in `Program.cs`.
- Use Mapster (`IMapper`) for BO ↔ DTO conversions if possible.
- Async all the way (`async`/`await`) for every I/O-bound operation.

## API Controllers

- Return `ActionResult<T>` (not raw objects) so status codes stay explicit.
- Validate request DTOs in the application/service layer, not in the controller body.
- Do **not** reference repositories directly from controllers.

## Testing

xUnit + Moq; test projects are named `{Project}.Tests`.

**Naming convention** (mandatory): `MethodName_Condition_ExpectedResult()`

- No `// Arrange / Act / Assert` comments.
- Mock dependencies with Moq; never instantiate real databases in unit tests.
- Copy style from the nearest existing test file (attribute choice, async signatures).

---

## Best Practices

### ✅ DO

- Use constructor injection; let `Program.cs` register everything.
- Add XML doc comments to all public service and controller members.
- Fix compiler warnings before building — `TreatWarningsAsErrors` is enabled in all projects.
- Use `is null` / `is not null` for null checks.

### ❌ DON'T

- Do not call repositories from controllers — go through services.
- Do not place business logic in controllers or repositories.
- Do not create circular dependencies across domain layers (validated by architecture tests).
- Do not suppress warnings with `#pragma` unless there is no other option.
- Do not skip `async`/`await` for database or I/O calls.
- Do not use `== null` or `!= null` with nullable reference types.
- Do not use `- [ ]` in markdown, use emojis instead for task status.

## Common Pitfalls

- Keep Windows-compatible commands when suggesting shell usage.
- `backend/README.md` contains outdated version text in places; prefer `.csproj` target frameworks as source of truth.
- `Common/Extensions/ServiceExtensions.cs` currently contains service-registration placeholders; follow established extension registration style when implementing.
