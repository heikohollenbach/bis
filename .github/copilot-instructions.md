---
name: copilot-instructions
description: "Workspace instructions for the project. Guides AI assistance"
---

## User specific guidelines

- **Always use the user instructions** von `~\.copilot\instructions\*`

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
