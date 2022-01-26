# VRProjekt2122-seliskases

## MVP
Plattform: Oculus Quest

## Ablauf
Das Spiel dreht sich um den Kontrast zwischen Licht und Dunkelheit, Kälte und
Wärme. Das Labyrinth besteht aus Schnee und Eis. Am Ausgang des Labyrinths
steht eine Feuerschale. Um aus dem Labyrinth zu entkommen, muss diese Schale
entzündet werden.

Zu Beginn muss in einer gewissen Zeit eine Fackel gefunden werden, sonst erfriert
der Spieler. (Die ablaufende Zeit wird durch eine Zeitleiste angezeigt.) Wenn der
Spieler die Fackel gefunden hat, muss er nicht mehr gegen die Zeit anspielen.

Mithilfe der Fackel muss der Spieler den Ausgang des Labyrinths finden. Dabei
kommt er allerdings an ein Hindernis: ein Schneemann steht im Weg. Der Spieler
muss den Schneemann mit den Händen zerstören.

Optional: Wenn der Spieler beim Zerstören des Schneemanns nicht geschickt
vorgeht und der Schneemann zum Spieler umkippt, erlischt die Fackel. Dadurch
entsteht wieder der zeitliche Druck, nicht zu erfrieren.

Wenn der Spieler den Ausgang und damit die Feuerschale findet, muss der Spieler
ein zufällig generiertes Schachrätsel lösen.

MVP: Das Schachbrett wird als 2D-Fenster angezeigt
Optional: Der Spieler steht vor einem physischen Schachbrett im 3D-Raum

Auch hier muss der Spieler gegen die Zeit ankämpfen. Auditiv wird dies vom
Geräusch einer tickenden Uhr untermalt. Schafft der Spieler das Rätsel nicht
rechtzeitig und macht einen Fehler, verliert er das Spiel und muss von vorn
beginnen.

MVP: Wenn der Spieler das Schach-Rätsel geschafft hat, öffnet sich der Ausgang
Optional: Schafft der Spieler das Schach-Rätsel, muss er das Holz, in welches sich
das Schachbrett dann verwandelt, zur Feuerschale bringen und mit der Fackel
entflammen. Dadurch schmilzt der Ausgang.

So werden drei Fähigkeiten des Spielers auf die Probe gestellt: Orientierung,
Geschicklichkeit und Strategie.

## Auditive Gestaltung
Auditive Elemente werden vorrangig eingesetzt, um den Spieler bei der Orientierung
zu unterstützen. So leitet beispielsweise zu Beginn das Geräusch des Brennens der
Fackel dem Spieler den Weg.
Die Ambiance-Geräusche sollten im Spieler ein Gefühl von Unbehagen, Kälte und
Einsamkeit auslösen (beispielsweise durch einen dezenten Hall).

Zu Beginn (bevor der Spieler die Fackel gefunden hat) ist der Ton dumpf bzw. leicht
verzerrt, um dem Spieler ein Gefühl von Kälte und Dringlichkeit zu vermitteln.

Sounds:
• Ambiance (Wind, Vögel, etc.)
• Footsteps
• Fackel
• Fallender Schnee (Schneemann)
• Schachfiguren (Ziehen, Schlagen, etc.)
• Benennende Feuerschale

## Visual Style
Das Spiel soll auf einen dunklen und kalten Low-Poly Style setzen.
Optional: Bei Kälte und Erfrieren kommen Post-Processing Shader zum Einsatz,
welche das Bild subtil verzerren, ohne für den Spieler unangenehm zu sein.

## Zeitliche Einteilung
1. Meilenstein (1. Dezember 2021)
• Funktionierender Build
• Teleportation
• Funktionierendes Schach-Puzzle

2. Meilenstein: 22. Dezember 2021
• Alle Puzzles funktionieren

3. Meilenstein (19. Jänner 2021):
• Fertige eingebaute Ambiance-Sounds
• 3D-Modelle & Animationen eingebaut
• Optional: Post-Processing

## Arbeitsschritte
1. Projekt-Setup (GitHub Projekt, etc…)
2. Erster Test-Build auf Oculus Quest
3. Konzeption der Code-Architektur
4. Scene Setup (Einfügung aller Ressourcen)
5. Implementierung des Character Movements (Teleportation, etc.)
6. Schach-Rätsel Programmierung (Whiteboxing)
7. Erste Testung
8. Fackel-Rätsel Programmierung (Whiteboxing)
9. Schneemann Programmierung (Whiteboxing)
10. Ambiance-Sounds
11. Schach-Rätsel Auditiv & Visuell
12. Fackel-Rätsel Auditiv & Visuell
13. Schneemann-Rätsel Auditiv & Visuell
14. Zweite Testung & Feedback
15. 3D-Modellierung
16. Schnee Shader
17. Zusätzliches Sound-Design
18. Post-Processing Shader
19. Dokumentation (Read Me, etc.)
20. Endtestung
21. Abgabe

## Limitations

 - Couldn't test the functionality of the chess puzzle with the VR-Environment. However, it works fine as a 2D application.
 - Couldn't test the gaze pointer within the VR-Environment. This includes moving the chess pieces as well as the UI Input.
 - Couldn't test final performance with Oculus

## Packages

-2D Sprite 1.0.0
-JetBrains Rider Editor 2.0.7
-Oculus XR Plugin 1.10.0
-Post Processing 3.1.1
-Test Framework 1.1.29
-TextMeshPro 3.0.6
-Timeline 1.4.8
-Unity UI 1.0.0
-Version Control 1.9.0
-Visual Studio Code Editor 1.2.4
-Visual Studio Editor 2.0.11
-XR Interaction Toolkit 1.0.0-pre.8
-XR Plugin Management 4.2.0

## Third-party code
https://www.youtube.com/watch?v=U4ogK0MIzqk&t=323s
This project documentation as well the respective GitHub repository were used as a starting point for the chess logic. Code copies are implicitly documented as comments in the individual .cs files.

## Assets
 - https://www.turbosquid.com/3d-models/snowman-christmas-3d-model-1670626
 - https://www.turbosquid.com/3d-models/tree-pixel-low-poly-3d-model-1764347
 - https://assetstore.unity.com/packages/2d/textures-materials/sky/customizable-skybox-174576
 - https://www.turbosquid.com/3d-models/chess-queen-3d-1656436
 - https://www.turbosquid.com/3d-models/free-max-mode-frame/719036#
 - https://www.turbosquid.com/3d-models/medieval-door-3d-model-1207184
 - https://www.turbosquid.com/3d-models/old-key-3ds-free/1099143
 - https://www.cgtrader.com/free-3d-models/furniture/lamp/medieval-torch-game-ready-has-emission
 - https://assetstore.unity.com/packages/3d/environments/historic/modular-medieval-lanterns-85527
 - https://assetstore.unity.com/packages/3d/environments/historic/historic-environment-142116
 - Sounds from https://www.freesfx.co.uk & https://freesound.org
