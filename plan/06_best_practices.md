# ✅ Best Practices

## Architektur

- Server autoritativ (Server entscheidet alles)
- Keine Spiellogik im Client
- Services trennen (z.B. Auth, Player, World, Chat)
- Single Responsibility Principle (SRP)
- Separation of Concerns (SoC)

## Performance

- Object Pooling (z.B. für Projektile, NPCs)
- Culling ( z.B. Frustum Culling, Occlusion Culling)
- Spatial Partitioning (z.B. Quadtrees, Octrees)
- Asynchrone Verarbeitung (z.B. für KI, Pathfinding)
- Optimierung von Datenstrukturen (z.B. ECS, Structs statt Classes)
- Kompression von Netzwerkdaten (z.B. Protobuf, MessagePack)
- LOD (Level of Detail) für Modelle und Effekte
- Multithreading (z.B. für Hintergrundberechnungen, Ladezeiten)
- Delta Updates (nur Änderungen senden, nicht den gesamten Zustand)

## Code

- Clean Architecture
- Repository Pattern
- Tests

## Sicherheit

- Rate Limiting
- Validate alles
