# Spaceship

- Module / Modular
- Lego-like
- Komponenten: Antrieb, Rumpf, Bewaffnung, Laderaum, Generator usw.
- Docking Punkte

## ⚙️ 2. Technisches System (Unity-tauglich)

**🔩 Module als Bausteine**

Jedes Modul:

Mesh + Collider
Liste von DockingNodes
Eigenschaften (HP, Masse, Energieverbrauch, etc.)

**🔗 DockingNode**

Position + Rotation
Typ (z.B. “structural”, “weapon”, “engine”)
Größe (S/M/L → verhindert wildes Kombinieren)

**🧲 Snap-System**

Beim Andocken:

Position wird automatisch ausgerichtet
Rotation wird angepasst
Optional: Grid oder magnetisches Snapping

**👉 Wichtig:**

Entweder Grid-basiert (einfacher)
oder freie Verbindung mit Constraints (flexibler, aber schwerer)

## 🎨 3. Wie bleibt es “stimmig”?

Das ist der wichtigste Punkt.

Wenn du einfach Würfel + Kugeln mischst → sieht schnell chaotisch aus.

👉 Lösung: Design-Regeln definieren

🧩 Regel 1: Einheitliche Verbindungspunkte

Alle Module haben:

gleiche Docking-Ringe / Ports
gleiche Farben / Materialien an Verbindungsstellen

➡️ Dadurch wirkt alles zusammengehörig

📐 Regel 2: Shape-Familien

Nicht alles wild mischen, sondern Design-Sets:

Option A – “Hard Sci-Fi” (empfohlen)
Quader / Zylinder
klare Kanten
industrielle Optik

➡️ wirkt wie echte Raumstationen

Option B – “Organic / Alien”
Kugeln
weiche Übergänge
asymmetrisch

➡️ schwieriger umzusetzen

Option C – Hybrid (sehr gut)
Kern = Zylinder / Quader
Erweiterungen = spezielle Formen

## 🧱 4. Welche Module funktionieren gut?

Hier wird’s konkret:

🧱 Struktur-Module (Pflicht)

Das sind deine “Wachstumsbausteine”

Würfel (Standard)
Rechteck-Blöcke (2x1, 3x1)
Zylinder (Rohre / Verbindungen)

👉 Empfehlung:
➡️ KEINE Kugeln als Basis
→ schwer sinnvoll anzudocken

🔌 Verbindungs-Module
Gelenke
T-Stücke
Kreuzstücke

👉 Wichtig für:

komplexe Formen
“echtes Engineering Gefühl”
🚀 Funktions-Module
🔥 Engine
nach hinten ausgerichtet
erzeugt Schub abhängig von Masse
🔫 Waffen
brauchen Sichtlinie
können blockiert werden
🛡️ Schilde
evtl. als Ring oder Dome
🔋 Energie
Reaktoren
verbinden Gameplay mit Größe
🎯 Spezial-Module (macht dein Spiel einzigartig)
Rotationsgelenke
ausfahrbare Teile
Drone Bays
Radar / Scanner

## 🧬 5. Wachstumssystem (Game Design)

Damit es sich gut spielt:

⚖️ Masse vs Leistung
Mehr Module = mehr Masse
→ du brauchst mehr Engine

👉 verhindert “einfach riesig werden”

🔌 Energie-System
jedes Modul verbraucht Energie
Reaktoren limitieren Wachstum
🧠 Schwerpunkt / Balance

(optional, aber geil)

Schiffe können kippen / träge werden
Schwerpunkt beeinflusst Steuerung

## 🎨 6. Wie du es hübsch bekommst (entscheidend)

🔧 Trick 1: “Connection Rings”
Jede Verbindung hat:
Glow
mechanische Details
kaschiert Übergänge
🎨 Trick 2: Farb-System
Spielerfarbe über alles
Module leicht unterschiedlich
🧱 Trick 3: Auto-Smoothing
kleine Übergangsteile automatisch einfügen

## 🧪 7. Was du vermeiden solltest

❌ Kugeln als Hauptmodule
→ schwer zu kombinieren

❌ komplett freie Rotation ohne Regeln
→ Chaos

❌ zu viele verschiedene Größen
→ passt nichts zusammen

🚀 Fazit (konkret für dich)

Wenn du schnell starten willst:

👉 Nimm:

Quader (1x1, 2x1, 3x1)
Zylinder (Verbindungen)
feste Docking-Punkte
Grid-System

👉 Bau darauf:

Engine
Waffen
Energie

👉 Dann später:

Spezialmodule
visuelle Übergänge
