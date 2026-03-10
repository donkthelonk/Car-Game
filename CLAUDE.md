# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A Unity 3D endless runner / traffic dodger game. The player drives a car down a road, avoiding oncoming traffic, collecting powerups, and dodging crates. The game tracks score and a countdown timer.

- **Unity version**: 2021 (based on packages)
- **Primary scene**: `Assets/Scenes/My Game.unity`
- **All game scripts**: `Assets/Scripts/`

## Development Workflow

This is a Unity project — there is no CLI build or test command. All development happens inside the Unity Editor:

- **Open project**: Open Unity Hub → select this project folder
- **Play/test**: Press Play in the Unity Editor
- **Build**: File → Build Settings → Build

Scripts are compiled by Unity automatically on save. The `Assembly-CSharp.csproj` is auto-generated.

## Architecture

### Script Organization

```
Assets/Scripts/
├── Vehicles/           # Vehicle class hierarchy
│   ├── Vehicle.cs      # Abstract base class (MonoBehaviour)
│   ├── Car.cs          # Derived: rotates on Y-axis on player collision
│   ├── Truck.cs        # Derived: rotates on Z-axis on player collision
│   └── Bus.cs          # Derived: launches upward on player collision
├── PlayerController.cs # Physics-based player movement (Rigidbody, FixedUpdate)
├── SpawnManager.cs     # InvokeRepeating spawner for traffic, powerups, crates
├── GameManager.cs      # Score, health, game over screen, and restart logic
├── Timer.cs            # Countdown timer; calls GameManager.EndGame() at zero
├── MoveDown.cs         # Moves any object toward negative Z and destroys it off-screen
├── RepeatStreet.cs     # Scrolls the road mesh and resets it to create an infinite loop
├── Powerup.cs          # Rotates the powerup object as it moves down
├── Crate.cs            # Explodes and destroys itself on player collision
├── RotateWheelsX.cs    # Rotates a wheel transform on the X-axis
├── RotateWheelsZ.cs    # Rotates a wheel transform on the Z-axis
└── OLD/                # Archived pre-inheritance scripts (ignore)
```

### Key Design Patterns

**Vehicle inheritance hierarchy**: `Vehicle` (abstract MonoBehaviour) → `Car`, `Truck`, `Bus`. The base class defines `Quirk()` as abstract and `Honk()` / `ChangeLanes()` as virtual. Each derived class sets `isQuirky = true` in `OnCollisionEnter` when tagged "Player", then calls `Quirk()` each `Update()`. On player collision, all vehicles also call `Explode()` (defined in Vehicle) which instantiates an explosion prefab and destroys the GameObject. Honking uses `AudioSource.PlayClipAtPoint()` so the sound plays after the GameObject is destroyed. Each vehicle prefab must have `honkClip` and `explosionPrefab` assigned in the Inspector — no AudioSource component needed.

**GameManager** is the central game state manager. It tracks score (`UpdateScore()`), health (`TakeDamage()` / `RestoreHealth()`), and handles game over (`EndGame()` freezes time via `Time.timeScale = 0` and shows the game over screen) and restart (`RestartGame()` resets `Time.timeScale` and reloads the scene). It holds references to score, health, and final score TextMeshPro objects, plus the game over screen panel.

**Damage deduplication**: `PlayerController` uses an `isInvincible` flag with a 0.5s `Invoke` cooldown to prevent multiple colliders on the player (e.g. wheels) from triggering `TakeDamage()` more than once per hit.

**Powerup**: collected via `OnTriggerEnter` on the player (requires the powerup collider to be set as a trigger). Calls `GameManager.RestoreHealth()` which adds 1 health up to `maxHealth`.

**Tags in use**: `"Player"`, `"Traffic"`, `"Powerup"`, `"Crate"` — these must match GameObject tag assignments in the Unity Editor.

**MoveDown.cs** is a reusable component attached to traffic vehicles, powerups, and crates to push them toward the camera (negative Z) and clean them up when they pass `zDestroy`.

**SpawnManager** calls `gameManager.UpdateScore(5)` every time traffic spawns — score is purely traffic-volume based (not survival time).


### Coordinate convention

The road runs along the Z-axis. "Forward" for incoming traffic is negative Z (toward the camera). Player movement uses `Vector3.forward` (positive Z) for accelerating and `Vector3.right` for lateral movement. The player is constrained between `zMin` and `zMax`.
