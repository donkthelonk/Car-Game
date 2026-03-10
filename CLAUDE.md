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
│   ├── Car.cs          # Derived vehicle type
│   ├── Truck.cs        # Derived vehicle type
│   └── Bus.cs          # Derived vehicle type
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

**Vehicle inheritance hierarchy**: `Vehicle` (abstract MonoBehaviour) → `Car`, `Truck`, `Bus`. The base class defines `Honk()` / `ChangeLanes()` as virtual. On player collision, all vehicles call `Honk()` and `Explode()` (both defined in Vehicle). `Explode()` awards `pointValue` points, instantiates the explosion prefab, and destroys the GameObject. Honking uses `AudioSource.PlayClipAtPoint()` so the sound plays after the GameObject is destroyed. Each vehicle prefab must have `honkClip` and `explosionPrefab` assigned in the Inspector — no AudioSource component needed. `gameManager` is found in `Awake()` to ensure it's available before any collision fires.

**Per-vehicle damage**: `Vehicle` has a public `damageAmount` field (default 1). `Truck` sets it to 2 and `Bus` sets it to 3 in `Start()`. `PlayerController` reads `damageAmount` from the colliding vehicle and passes it to `GameManager.TakeDamage(int amount)`.

**GameManager** is the central game state manager. It tracks score (`UpdateScore()`), health (`TakeDamage()` / `RestoreHealth()`), and handles game over (`EndGame()` freezes time via `Time.timeScale = 0` and shows the game over screen) and restart (`RestartGame()` resets `Time.timeScale` and reloads the scene). It holds references to score, health, and final score TextMeshPro objects, plus the game over screen panel.

**Damage deduplication**: `PlayerController` uses an `isInvincible` flag with a 0.5s `Invoke` cooldown to prevent multiple colliders on the player (e.g. wheels) from triggering `TakeDamage()` more than once per hit.

**Powerup**: collected via `OnTriggerEnter` on the player (requires the powerup collider to be set as a trigger). Calls `GameManager.RestoreHealth()` (adds 1 health up to `maxHealth`) and `GameManager.AddTime(5f)` (adds 5 seconds to the timer). `GameManager.AddTime()` delegates to `Timer.AddTime()`, which increments `timeLeft` and triggers a yellow flash on `timerValueText`.

**Timer**: `AddTime(float amount)` adds time and flashes the timer number yellow using an `isFlashing` flag to prevent `Update` from overwriting the flash color mid-coroutine.

**UI number flashes**: score number flashes green on increase, health number flashes red on damage, timer number flashes yellow when time is added. Each stat uses two separate TMP objects: a static label (e.g. `"Score: "`) and a dynamic value (e.g. `"0"`). Code only updates the value object (`scoreValueText`, `healthValueText`, `timerValueText`). Flash coroutines set `fontSize = originalSize * 1.1f`, apply a `<color>` rich text tag, yield, then restore `fontSize`. Original sizes are captured once in `Start()` so repeated flashes always scale from the true baseline — never use `<size>` rich text tags for resizing.

**Randomized controls**: every `controlsRandomizeInterval` seconds (default 5), horizontal and vertical axes are each randomly inverted for 1 second then reset. All player renderers cycle through rainbow colors and a "Controls Scrambled!" UI text appears and cycles through rainbow colors during this period.

**Player tilt**: `TiltPlayer()` runs each `FixedUpdate` and lerps the player's Y rotation toward `horizontalInput * tiltAngle` to simulate turning. Snaps back to 0 when input is released.

**Tags in use**: `"Player"`, `"Traffic"`, `"Powerup"`, `"Crate"` — these must match GameObject tag assignments in the Unity Editor.

**MoveDown.cs** is a reusable component attached to traffic vehicles, powerups, and crates to push them toward the camera (negative Z) and clean them up when they pass `zDestroy`.

**SpawnManager** calls `gameManager.UpdateScore(5)` every time traffic spawns — score is purely traffic-volume based (not survival time).


### Coordinate convention

The road runs along the Z-axis. "Forward" for incoming traffic is negative Z (toward the camera). Player movement uses `Vector3.forward` (positive Z) for accelerating and `Vector3.right` for lateral movement. The player is constrained between `zMin` and `zMax`.
