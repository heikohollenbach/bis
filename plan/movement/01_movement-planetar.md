# Steuerungskonzept

## 🎮 Ziel des Systems

Dieses Steuerungskonzept ist für ein **3rd-Person-3D-Raumschiff-Gameplay auf Android** optimiert.

Der Fokus liegt auf:
- einfacher Bedienbarkeit
- gutem Raumgefühl
- flüssigem Flugverhalten
- geringer Einstiegshürde

Das Schiff soll sich **wie ein echtes Raumschiff im Raum bewegen**, nicht wie ein Charakter.

---

## 📷 Kamera-System

### Grundidee

- 3rd-Person Kamera leicht hinter und über dem Schiff
- weiches Follow-System (keine harte Kamera)
- leichte Verzögerung für „Space Feeling“

### Verhalten

- Kamera folgt der Schiffsausrichtung
- sanfte Rotation mit dem Schiff
- optionales „Look Ahead“ in Flugrichtung

### Vorteile

- bessere Orientierung im Raum
- cineastisches Gefühl
- weniger Motion Sickness als freie Kamera

---

## 🕹️ Steuerung Layout (Mobile)

### 📍 Linke Bildschirmhälfte – Throttle / Schub

Der linke Bereich steuert die Bewegung entlang der Flugrichtung.

#### Eingaben:

- ⬆️ Finger nach oben: mehr Vorwärts-Schub
- ⬇️ Finger nach unten: Bremsen / Rückwärts
- optional:
  - leichte Links/Rechts-Neigung für Strafe-Bewegung

#### Verhalten:

- kontinuierlicher Schub (kein „Step Movement“)
- Geschwindigkeit baut sich dynamisch auf
- Trägheit wirkt wie im Weltraum

#### Ziel:

Der Spieler kontrolliert **nur die Energie / Geschwindigkeit**, nicht die exakte Position.

---

### 📍 Rechte Bildschirmhälfte – Steuerung / Ausrichtung

Der rechte Bereich steuert die Richtung des Raumschiffs.

#### Eingaben:

- ➡️ nach rechts ziehen: Schiff dreht sich nach rechts (Yaw)
- ⬅️ nach links ziehen: Schiff dreht sich nach links (Yaw)
- ⬆️ nach oben ziehen: Nase hebt sich (Pitch up)
- ⬇️ nach unten ziehen: Nase senkt sich (Pitch down)

#### Verhalten:

- sanfte Rotation (kein sofortiges Snapping)
- „inertia based turning“ (Trägheit beim Drehen)
- Begrenzung der Pitch-Winkel gegen Überdrehen

#### Ziel:

Der Spieler bestimmt **wohin das Schiff fliegt**, nicht die exakte Position.

---

## 🚀 Flugmodell

### Physik-Ansatz

- Rigidbody-basiert oder kinematisch mit interpolierter Bewegung
- leichte Trägheit (Drag + Angular Drag)
- kein realistischer Orbit-Simulator → eher Arcade-Space

### Eigenschaften:

- sanftes Beschleunigen
- langsames Abbremsen
- kein abruptes Stoppen
- Gefühl von Masse im Raum

---

## ⚡ Boost-System (optional)

### Funktion

- temporärer Geschwindigkeits-Boost

### Input

- separater Button (rechts oben oder Daumenbereich)

### Verhalten

- starke Beschleunigung nach vorne
- visuelles Feedback (Speed Lines / Partikel)
- kurze Cooldown-Phase optional

---

## 🧠 Assist-System (wichtig für Spielbarkeit)

Damit das Steuerungssystem „leicht fliegbar“ bleibt:

### Auto-Stabilisierung

- Schiff richtet sich langsam wieder horizontal aus (optional togglebar)
- verhindert unkontrolliertes Rollen

### Ziel-Hilfe

- leichte „Magnetisierung“ zur Blickrichtung
- hilft beim Treffen von Zielen im Raum

### Camera Assist

- Kamera bleibt stabil hinter dem Schiff
- verhindert extremes Rotieren der Ansicht

---

## 🎯 Gameplay-Erlebnis

Der Spieler soll folgendes Gefühl bekommen:

- „Ich fliege ein Raumschiff“
- nicht „Ich steuere eine Kamera im Raum“
- sondern „Ich lenke ein Fahrzeug durch den Weltraum“

---

## ✅ Vorteile dieses Systems

- sehr gut für Mobile geeignet
- intuitive Zweihandbedienung
- geringe Lernkurve
- gut skalierbar für Combat & Exploration
- unterstützt RPG-Gameplay im Space Setting

---

## 🧩 Erweiterungen (später möglich)

- Auto-Pilot zu Missionspunkten
- Ziel-Lock-On für Kampf
- Drift-Modus (für Turns)
- Gravity-Zonen mit angepasster Physik
- Third-Person Cinematic Camera Mode

---

## 📌 Fazit

Dieses Steuerungssystem ist ein **Arcade-optimierter 3rd-Person Space Flight Controller**, der bewusst auf Einfachheit und gutes Feeling statt Realismus setzt.
