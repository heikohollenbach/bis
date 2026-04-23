# 🏗 Architektur

## High-Level

Client (Unity) ↔ GameServer ↔ Services ↔ DB

## Client

- Unity Engine
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

- AuthService
  - Login
  - JWT
  - User Management
  - Session Management
  - Sicherheitsmaßnahmen (z.B. Passwort-Hashing, Brute-Force-Schutz)
  - Integration mit externen Authentifizierungsdiensten (z.B. OAuth, SSO)
- PlayerService
  - Progression
  - Inventar
  - Charakterdaten
  - Freundesliste
  - Gilden
  - Matchmaking
  - Ranglisten
  - Sicherheitsmaßnahmen (z.B. Validierung von Spieleraktionen, Cheat-Schutz)
- WorldService
  - Generierung
  - Instanziierung
  - Persistenz von Weltzuständen
  - Verwaltung von NPCs und Mobs
  - Eventuelle Integration von KI-Logik für NPCs
  - Sicherheitsmaßnahmen (z.B. Validierung von Weltinteraktionen, Cheat-Schutz)
  - Optimierung für Performance (z.B. Load Balancing, Sharding)
- ChatService
  - Kommunikation

## Daten

- PostgreSQL (persistente Daten)
- Redis (Sessions, Cache)

## Datenfluss

Client → Input → Server → Simulation → Broadcast → Clients
