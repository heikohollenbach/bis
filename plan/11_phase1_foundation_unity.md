# Phase 1: Foundation - Unity Projekt

**Ziel**: Ein wartbares Unity-Client-Fundament nach Best Practices und Clean Architecture aufsetzen, das direkt zu den Backend-Services passt und als Basis fuer Movement (Phase 1) sowie Mining/Combat/Inventar (Phase 2) dient.

**Scope in Phase 1**:

- Projektstruktur, Architekturgrenzen, DI-Bootstrap
- Auth + Verbindungsaufbau (HTTP + SignalR)
- Movement Vertical Slice (planetarisches Fliegen als Stub)
- Tests, Build-Rails, klare Done-Kriterien

**Nicht in Scope in Phase 1**:

- Vollstaendiges Mining/Combat/Inventar
- Universumsysteme (Wirtschaft, Gilden, Galaxien)

---

## Leitprinzipien (aus Gesamtplan)

- Server ist autoritativ, der Client entscheidet keine Spiellogik.
- Realtime via SignalR/WebSocket, tick-basiert mit 20Hz.
- Clean Architecture + SRP + Separation of Concerns.
- Mobile first (Android), Windows nur wenn ohne nennenswerten Zusatzaufwand mitlaeuft.
- Sicherheit: JWT Bearer, Token-Rotation, Validierung aller Eingaben.

---

## Zielbild der Unity-Architektur

Empfohlene Schichten:

1. Core

- Entities, Value Objects, Domainenregeln ohne Unity-Abhaengigkeiten.

1. Application

- Use Cases, Ports/Interfaces, Result/Error-Modelle.

1. Infrastructure

- REST-Adapter, SignalR-Adapter, Token-Speicher, Serialization.

1. Presentation

- UI/ViewModels und Szenensteuerung.

1. Gameplay

- Input, Kamera, Movement-Visualisierung (nur Client-UX, keine Autoritaet).

1. Bootstrap

- Composition Root, DI-Wiring, App-Start.

### Geplante Ordnerstruktur (Unity)

```text
client/
  Assets/
    BIS/
      Core/
      Application/
      Infrastructure/
      Presentation/
      Gameplay/
      Bootstrap/
      Tests/
    Prefabs/
    Scenes/
    Resources/
```

### Assembly-Definition-Regeln

- Core kennt niemanden.
- Application darf Core referenzieren.
- Infrastructure darf Application + Core referenzieren.
- Presentation und Gameplay greifen nur ueber Application-Ports auf Logik zu.
- Bootstrap darf alle referenzieren und verdrahtet nur.

---

## Umsetzungsplan

## Schritt 0 - Architekturvertrag fixieren

**Ergebnis**: Verbindlicher Contract fuer Client <-> Backend.

- JWT Bearer + Refresh-Token-Rotation festziehen.
- Datentypen harmonisieren: Guid, UTC DateTime, Enum als int.
- Tick-/Zeitmodell festlegen: 20Hz Netzwerk-Update, Interpolation im Render-Loop.
- Reconnect- und Fehlerverhalten definieren (Timeout, Unauthorized, Retry).

## Schritt 1 - Unity Projekt Bootstrap

**Ergebnis**: Laufendes Unity 6 LTS-Projekt mit Android als Hauptziel.

- Unity 6 LTS Projekt erzeugen.
- Android Build Support aktivieren.
- Windows Build nur mitnehmen, wenn ohne Zusatzaufwand.
- Basispakete installieren:
  - Input System
  - Unity Test Framework
  - optional Cinemachine (Kamera), Addressables (spaeteres Content-Management)

## Schritt 2 - Clean Architecture technisch durchsetzen

**Ergebnis**: Klare Schichten, keine Vermischung.

- Ordnerstruktur unter Assets/BIS anlegen.
- Asmdef-Dateien fuer alle Schichten erstellen.
- Build faellt, wenn Schichtgrenzen verletzt werden.
- Einheitliches Result-/Error-Modell einfuehren (Success, ValidationError, Unauthorized, Timeout, Retryable).

## Schritt 3 - Netzwerk und Auth Basis

**Ergebnis**: Sicherer Login + stabile Realtime-Verbindung.

- REST-Auth-Client fuer:
  - Register
  - Login
  - Refresh
  - Validate
- TokenLifecycleManager:
  - Ablaufzeiten pruefen
  - Token rechtzeitig refreshen
  - Rotation korrekt anwenden
  - Android sichere Speicherung (Keystore)
- SignalR-Client mit Zustandsautomat:
  - Disconnected
  - Connecting
  - Connected
  - Reconnecting
- Exponentielles Backoff bei Reconnect.

## Schritt 4 - Foundation Vertical Slice

**Ergebnis**: End-to-End Pfad fuer "Spieler kann sich verbinden".

- App startet in Bootstrap-Szene.
- Auth-Screen -> Login/Register erfolgreich.
- JWT aktiv, API-Call mit Bearer klappt.
- SignalR-Verbindung steht und reconnectet robust.
- Uebergang in Basisszene nach erfolgreicher Verbindung.

## Schritt 5 - Movement Stub (Phase 1)

**Ergebnis**: Spielbares planetarisches Flug-Grundgeruest.

- Input gemaess Konzept:
  - links: Throttle/Bremsen
  - rechts: Yaw/Pitch
- Sanfte Inertia + stabile Follow-Kamera.
- Trennung der Verantwortungen:
  - Input Capture
  - Movement Intent
  - Network Command
  - Visual Feedback
- Prediction nur fuer UX, Server bleibt autoritativ.

## Schritt 6 - Qualitaetssicherung und Delivery-Rails

**Ergebnis**: Stabilitaet fuer Folgephasen ohne Rework.

- Unit-Tests fuer Core/Application Use Cases.
- Contract-Tests fuer DTO-Mapping (Guid, UTC, Enum-int).
- PlayMode-Smoke-Test: AppStart -> Login -> Connect -> SceneLoad.
- Android Build als Pflichtcheck, Windows optional.
- Dokumentierte Akzeptanzkriterien fuer Phase-1-Abnahme.

---

## Akzeptanzkriterien fuer "Unity Projekt" (Phase 1)

- Unity-Projekt laeuft lokal stabil (Android-targeted).
- Schichtgrenzen sind technisch erzwungen (asmdef).
- Auth-Flow gegen Backend funktioniert inkl. Refresh.
- SignalR-Verbindung inkl. Reconnect ist nachweislich stabil.
- Basisszene laedt nach erfolgreicher Auth/Connect.

## Akzeptanzkriterien fuer "Movement" (Phase 1)

- Mobile Steuerungsschema ist umgesetzt und spielbar.
- Kamera folgt stabil und ohne starkes Jitter.
- Movement-Pfad ist serverautoritativ vorbereitet.
- Kein Architekturbruch (keine Spiellogik in UI/Infrastructure).

---

## Risiken und Gegenmassnahmen

1. Risiko: Fruehe Vermischung von Unity-Monobehaviour-Code und Domain-Logik.

- Gegenmassnahme: Domain nur in Core/Application, Adapter in Infrastructure/Presentation.

1. Risiko: Netzwerkcode ohne robustes Fehler-/Reconnect-Modell.

- Gegenmassnahme: State Machine + Retry/Backoff + klare Fehlerklassen.

1. Risiko: Tick-Desync (Client 60fps vs Server 20Hz).

- Gegenmassnahme: Netzwerk-Ingest auf 20Hz ausrichten, Rendering interpolieren.

1. Risiko: Token-Leaks auf Mobile.

- Gegenmassnahme: Sichere Speicherung, kurze JWT-Laufzeit, Rotation.

---

## Folge in spaeteren Phasen

- Phase 2: Mining, Combat, Inventar als weitere Vertical Slices auf denselben Schichten.
- Phase 3: Wirtschaft, Gilden, Galaxien ohne Architekturwechsel integrierbar.
- Phase 4: Performance/Monitoring/Beta auf bestehendem Fundament statt Rework.
