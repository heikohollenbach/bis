# Freies Warp-System im Sonnensystem

## 🎯 Ziel des Systems

Das Warp-System ermöglicht dem Spieler, sich **frei innerhalb des Sonnensystems zu bewegen**, ohne lange Strecken manuell fliegen zu müssen.

Der Spieler kann:
- jeden Punkt im Sonnensystem anwählen
- direkt dorthin springen (Warp)
- anschließend wieder in den normalen Flugmodus wechseln

---

## 🧠 Grundprinzip

Das Spiel trennt zwei Bewegungsarten:

### 🚀 Lokale Bewegung

- normales 3rd-Person-Fliegen
- wird für kurze Distanzen genutzt
- basiert auf dem bestehenden Flug-Controller

### 🌌 Warp-Bewegung

- wird für große Distanzen verwendet
- ist kein physisches Fliegen
- sondern ein schneller Positionswechsel mit visueller Darstellung

---

## 🧭 Navigations-Flow

### 1. Karte öffnen

- Spieler öffnet eine Sonnensystem-Karte
- Darstellung als 2D oder 3D möglich
- Karte ist zoombar und rotierbar

### 2. Ziel auswählen

Der Spieler kann:
- Planeten auswählen
- Stationen auswählen
- freie Punkte im Raum anklicken

Wichtig:
- Es muss möglich sein, **auch ins „Leere“ zu klicken**
- Das erzeugt eine freie Zielposition im Raum

---

### 3. Warp auslösen

- Button „Jump“ oder „Warp“
- optional Anzeige, ob Ziel erreichbar ist

---

### 4. Warp-Sequenz

Während des Warps:
- Eingaben werden deaktiviert
- Schiff richtet sich automatisch zum Ziel aus
- Kamera fokussiert das Schiff
- visueller Warp-Effekt wird abgespielt

Dauer:
- kurz halten (ca. 1–3 Sekunden)

---

### 5. Ankunft

- Spieler wird an Zielposition versetzt
- kleine zufällige Abweichung für natürliches Gefühl
- Kamera richtet sich neu aus
- Steuerung wird wieder aktiviert

---

## 📏 Distanz-Regelung

Warp soll nur für größere Distanzen genutzt werden.

Regel:
- kurze Distanz → normales Fliegen
- große Distanz → Warp verfügbar

Ziel:
- Spieler nutzt beide Systeme sinnvoll kombiniert

---

## 🎮 UX-Anforderungen

### Ziel-Feedback

- gewähltes Ziel wird visuell markiert
- klar erkennbar im Raum oder auf der Karte

### Richtungsanzeige

- optional Linie oder Pfeil zum Ziel

### Reichweitenanzeige

- zeigt, wie weit ein Warp möglich ist
- z. B. als Kreis oder Kugel um den Spieler

---

## 🧲 Snap-System (optional)

Wenn der Spieler nahe an Objekten klickt:
- Planeten
- Stationen
- Missionsziele

Dann wird das Ziel automatisch auf diese Objekte „gesnappt“.

---

## 🌍 Design-Vorteile

- freie Bewegung im gesamten Sonnensystem
- keine langen, langweiligen Flugzeiten
- ideal für Mobile-Steuerung
- gut kombinierbar mit Exploration und RPG-Mechaniken

---

## ⚠️ Zu vermeiden

- zu lange Warpzeiten
- fehlendes visuelles Feedback
- ungenaue Zielauswahl
- exaktes Teleportieren ohne natürliche Variation

---

## 🚀 Erweiterungsmöglichkeiten

Für spätere Versionen:

### Ressourcen-System

- Warp kostet Energie oder Treibstoff

### Exploration

- Bereiche müssen erst entdeckt werden
- „Fog of War“ im Sonnensystem

### Events

- zufällige Ereignisse während des Warps

### Points of Interest

- spezielle Orte im Raum
- automatisches Einrasten beim Ziel

---

## 📌 Fazit

Das Warp-System bildet die Grundlage für die Navigation im Spiel:

- normales Fliegen für kurze Strecken
- Warp für große Distanzen
