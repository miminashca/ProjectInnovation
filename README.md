# 🏥 Red Veil Operations

> **A 2-player asymmetric co-op horror game for mobile, featuring voice-reactive AI.**

![walktrough-final](https://github.com/user-attachments/assets/177917b3-27fe-4309-a215-907386473062)

**Engine:** Unity (Mobile)  
**Tech:** Photon PUN 2, Photon Voice  
**Status:** Released (3-Week Client Project)  

---

## ✎ᝰ About the Game

*Red Veil Operations* is an asymmetric co-op horror game designed for mobile devices. Two players must cooperate to steal valuable organs from an abandoned hospital while evading a sound-sensitive monster.

* **The Thief (Player 1):** Navigates the dark hospital in first-person with limited vision, physically collecting loot.
* **The Operator (Player 2):** Remains safe in a van, utilizing a tactical map and CCTV feeds to guide the Thief and track threats.

**The Innovation:** Communication is the core mechanic. Players utilize in-game voice chat to coordinate, but the enemy AI listens to the real-world microphone input. Players must whisper to communicate without alerting the monster.

---

## ✦ Key Features & Highlights

### ♖ Asymmetric Networking Architecture
Our engineering team built a robust multiplayer framework using **Photon PUN 2** to synchronize two completely distinct gameplay perspectives within a single session.
* **Dual Game Logic:** The game runs separate logic streams for the FPS view (Thief) and the UI-heavy CCTV interface (Operator), syncing player states, inventory, and enemy position in real-time.
* **Voice-Reactive Systems:** Integrated **Photon Voice** to not only facilitate chat but to drive gameplay mechanics. The Monster detects local microphone amplitude, triangulating player positions based on how loud they speak in real life.

### ᝰ.ᐟ Atmospheric Visuals & Environment
The art team delivered a cohesive "Post-Soviet" horror aesthetic optimized for mobile hardware.
* **Optimized Rendering:** Utilized light baking, occlusion culling, and texture atlasing to maintain high performance on mobile devices without sacrificing atmosphere.
* **Custom Shaders:** Implemented custom post-processing stacks and shaders for the night-vision camera feeds.
* **Modular Environment:** Constructed the hospital layout using a modular kit designed for rapid iteration and texture memory efficiency.

### ▶︎ •၊၊||၊|။ Immersive Audio Engineering
The sound design goes beyond simple playback, featuring complex scripting for immersion.
* **Audio Occlusion:** Sound effects are dynamically muffled based on walls and distance, allowing players to track the monster by ear.
* **Layered Music System:** The soundtrack adapts dynamically to the tension level, shifting between ambient exploration and high-stress chase themes.

### ঌ Intuitive Mobile UX
The interface was rigorously tested and iterated upon to handle complex inputs on touch screens.
* **Gyroscope Control:** The Operator can physically rotate their phone to pan security cameras, adding a tactile layer to the surveillance role.
* **Responsive Layouts:** Custom UI systems designed to manage camera map and in-game interactions seamlessly.

---

## Slam Door Interactive - Team 𐦂𖨆𐀪𖠋

This project was built in a 3-week agile sprint by a multidisciplinary team at Saxion University.

**Engineering**
* **[Nichita Cebotari (Framework & Gameplay)](https://linktr.ee/nikkicheb):** Architected the Monster FSM, Audio Detection systems, Voice Chat integration, and managed the Git pipeline.
* **[Svitlana Sosnova (Networking & Mechanics)](https://linktr.ee/miminashca?ltsid=7c9b94a2-6f6e-431a-937d-a78485047df2):** Implemented the core Photon architecture, Player State Machine, movement synchronization, and object interaction logic.

**UI/UX & SFX Design**
* **[Bogdan Pascari (UI/UX Designer)](https://pascaribogdan.journoportfolio.com/):** Designed Figma prototypes, implemented Unity UI layouts, and programmed responsive UX logic.
* **[Simeon Dorne (Sound Designer)](https://simeondorne.com/):** Recorded custom foley, composed adaptive music, and wrote scripts for audio occlusion and optimization.

**Art & Level Design**
* **[Catalin Apostol (3D Art & Animation)](https://cata1029.artstation.com):** Created the Monster concept and model, managed the character render pipeline, and handled animation states.
* **[Mariia Nechepurenko (Technical Artist)](https://www.artstation.com/mariianechepurenko):** Modeled environment props, handled UV/Texturing, light baking, and developed post-processing shaders.
* **[Stefani Badzheva (Level Art & Builder)](https://stefanibadzheva.artstation.com/):** Modeled layout structures, textured rooms, created the Thief character model, and assembled the final scene.

---

## ⌨ Controls

| Action | Thief (Player 1) | Operator (Player 2) |
| :--- | :--- | :--- |
| **Move** | Left Joystick | N/A |
| **Look** | Right Joystick | Gyroscope / Swipe |
| **Interact** | Tap Button | Tap Camera Grid |
| **Communicate** | Microphone (Whisper!) | Microphone (Talk Normally!) |

---

## >_ Installation / How to Run

1.  Clone the repository.
2.  Open the project in **Unity 6**.
3.  Ensure **Photon PUN 2** AppID is configured in the resources file.
4.  Build the APK to two Android devices (recommended for Gyro features) or use Unity Remote for testing.

---

## ֎ AI Usage Declaration

**ChatGPT (OpenAI)** was utilized as a writing partner and debugging assistant throughout the project lifecycle. This included assistance with:
* Designing code architecture patterns.
* Debugging complex logic and refactoring for optimization.
* Formatting documentation and basic script generation.

---

*Developed at Saxion University of Applied Sciences.*
