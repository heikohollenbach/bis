# Backend

## Overview

This is the backend for the BIS Space MMORPG. It's built with ASP.NET Core 8.0 and includes 4 microservices:

- **AuthService** - User authentication and session management
- **PlayerService** - Player progression, inventory, and ship management
- **WorldService** - World generation, movement, and combat simulation
- **ChatService** - In-game chat and messaging
- **WebSerive** - Landingpage

## Quick Start

### Prerequisites

- .NET 108.0 SDK
- Docker & Docker Compose
- PostgreSQL 16 (or use Docker)
- Redis (or use Docker)

### Local Development

1. **Build the solution:**

   ```bash
   dotnet build
   ```

2. **Run services locally (requires PostgreSQL + Redis):**

   ```bash
   # Terminal 1: AuthService
   cd src/AuthService
   dotnet run

   # Terminal 2: PlayerService
   cd src/PlayerService
   dotnet run

   # Terminal 3: WorldService
   cd src/WorldService
   dotnet run

   # Terminal 4: ChatService
   cd src/ChatService
   dotnet run
   ```

3. **Or run with Docker Compose (easiest):**

   ```bash
   docker-compose up
   ```

   Services will be available at:
   - AuthService: <http://localhost:5001>
   - PlayerService: <http://localhost:5002>
   - WorldService: <http://localhost:5003>
   - ChatService: <http://localhost:5004>

### Health Check

```bash
curl http://localhost:5001/health
```

## Project Structure

```text
backend/
├── src/
│   ├── Common/             # Shared utilities, DTOs, exceptions
│   ├── Domain/             # Domain entities
│   ├── Data/               # Data access layer (EF Core)
│   ├── AuthService/        # Authentication service
│   ├── PlayerService/      # Player management service
│   ├── WorldService/       # World & gameplay service
│   └── ChatService/        # Chat service
├── tests/                  # Test projects
│   ├── AuthService.Tests/  # Authentication service tests
├── Dockerfile              # Dockerfile
├── nginx.conf              # Nginx configuration for reverse proxy
├── docker-compose.yml      # Docker Compose configurations
└── BIS.sln                 # Solution file
```

## Database

The backend uses PostgreSQL. Each service has its own database:

- `bis_auth` — AuthService
- `bis_player` — PlayerService
- `bis_world` — WorldService
- `bis_chat` — ChatService

Migrations are created with Entity Framework Core.

## Testing

```bash
dotnet test
```

## Configuration

- `appsettings.json` — Production configuration
- `appsettings.Development.json` — Development configuration
- Environment variables override both files

## Architecture

- **Service-based architecture** — Each service is independent and scalable
- **Clean Architecture** — Domain, Application, and Presentation layers
- **EF Core** — ORM for database access
- **SignalR** — Real-time communication for multiplayer events
- **Serilog** — Structured logging

## Future Phases

- Phase 1 ✓ Foundation (Auth, Movement, Basic Player Data)
- Phase 2: Core Gameplay (Mining, Combat, Inventory)
- Phase 3: Universum (Galaxies, Guilds, Economy)
- Phase 4: Polish (Performance, Monitoring, Beta)
