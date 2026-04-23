# 🏗 Architektur

## High-Level

Client (Unity) ↔ GameServer ↔ Services ↔ DB

## Client

- Unity Engine
- Verantwortlich für:
  - Rendering
  - Input
  - UI
  - Lokale Effekte
  - Vorhersage (Prediction)
  - Interpolation
  - Clientseitige Logik (z.B. Animationen)
  - Kommunikation mit dem Server (RPCs, WebSockets)
  - Fehlerbehandlung und Reconciliation (z.B. bei Latenz oder Desync)
  - Optimierung für Performance (z.B. LOD, Culling)
  - Sicherheitsmaßnahmen (z.B. Cheat-Schutz, Validierung von Eingaben)
  - Eventuelle Integration von Drittanbieter-Plugins (z.B. für Analytics, Ads, etc.)
  - Android

## GameServer - Backend (.NET)

- Autoritative Simulation
- Tickrate: 20Hz
- Verwaltet:
  - Positionen
  - Combat
  - NPCs

### Services

- AuthService → Login/JWT
- PlayerService → Progression
- WorldService → Generierung
- ChatService → Kommunikation

## Daten

- PostgreSQL (persistente Daten)
- Redis (Sessions, Cache)

## Datenfluss

Client → Input → Server → Simulation → Broadcast → Clients
