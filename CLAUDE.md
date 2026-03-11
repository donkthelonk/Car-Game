# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A Unity 3D endless runner / traffic dodger game. The player drives a car down a road, avoiding oncoming traffic, collecting powerups, and dodging crates. The game tracks score and a countdown timer.

- **Unity version**: 2021 (based on packages)
- **Primary scene**: `Assets/Scenes/Crappy Car.unity`
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
├── Powerup.cs          # Rotates the powerup object; sets color based on PowerupType enum
├── PowerupText.cs      # Canvas UI text that flashes on powerup pickup
├── FloatingText.cs     # Animates a world-space TMP text that scales up, fades out, and rises
├── CameraShake.cs      # Shakes the camera by randomizing localPosition for a duration
├── BackgroundMusic.cs  # Ramps AudioSource pitch based on score
├── Crate.cs            # Explodes and destroys itself on player collision
├── RotateWheelsX.cs    # Rotates a wheel transform on the X-axis
├── RotateWheelsZ.cs    # Rotates a wheel transform on the Z-axis
└── OLD/                # Archived pre-inheritance scripts (ignore)
```

### Key Design Patterns

**Vehicle inheritance hierarchy**: `Vehicle` (abstract MonoBehaviour) → `Car`, `Truck`, `Bus`. The base class defines `Honk()` as virtual and owns the single `OnCollisionEnter` — derived classes do NOT define their own, as Unity calls both base and derived `private` lifecycle methods via reflection which would cause double-firing. On player collision, `Vehicle.OnCollisionEnter` calls `Honk()` and `Explode()`. `Explode()` instantiates the explosion prefab, spawns a `FloatingText` showing the points awarded, calls `gameManager.UpdateScore(pointValue)`, adds 1 second to the timer, and destroys the GameObject. Honking uses `AudioSource.PlayClipAtPoint()` so the sound plays after the GameObject is destroyed. Each vehicle prefab must have `honkClip`, `explosionPrefab`, and `floatingTextPrefab` assigned in the Inspector. `gameManager` is found in `Awake()` to ensure it's available before any collision fires.

**Per-vehicle damage**: `Vehicle` has a public `damageAmount` field (default 1). `Truck` sets it to 2 and `Bus` sets it to 3 in `Start()`. `PlayerController` reads `damageAmount` from the colliding vehicle and passes it to `GameManager.TakeDamage(int amount)`.

**GameManager** is the central game state manager. It tracks score (`UpdateScore()`), health (`TakeDamage()` / `RestoreHealth()`), and handles game over (`EndGame()` freezes time via `Time.timeScale = 0` and shows the game over screen) and restart (`RestartGame()` resets `Time.timeScale` and reloads the scene). It holds references to score, health, final score, and high score TextMeshPro objects, plus the game over screen panel.

**High score**: persisted across sessions via `PlayerPrefs` (key: `"HighScore"`). Updated in `EndGame()` if the current score exceeds the stored value. Displayed on the game over screen as `"High Score: N"`.

**Damage deduplication**: `PlayerController` uses an `isInvincible` flag with a 0.5s `Invoke` cooldown to prevent multiple colliders on the player (e.g. wheels) from triggering `TakeDamage()` more than once per hit.

**Powerup types**: defined by the `PowerupType` enum (`Health`, `Invincibility`, `ScoreMultiplier`). Set on each prefab via the `type` field on the `Powerup` component — color is applied automatically at runtime (green/blue/red). `SpawnManager` and `Crate` both hold `GameObject[]` arrays and pick randomly. Effects on pickup: Health restores 1 HP + 5s; Invincibility sets `isInvincible` for 3s and blinks the player; ScoreMultiplier doubles score for 10s. `GameManager.ShowPowerupText()` displays a `PowerupText` flash on every pickup. Each prefab has a `pickupSound` AudioClip field — played via `AudioSource.PlayClipAtPoint()` before the GameObject is destroyed.

**Crate**: on player collision, instantiates the explosion prefab and destroys itself. Has a `powerupDropChance` (0–1, default 0.5) — rolls against `powerupPrefabs[]` array, picks a random prefab if successful. Logs `"Crate dropped a powerup!"` to the console when this occurs.

**Damage feedback**: `GameManager.TakeDamage()` triggers three simultaneous effects — `CameraShake.Shake()` (0.3s, magnitude 0.15), a slow-motion dip (`Time.timeScale = 0.2f` for 0.15 real seconds via `WaitForSecondsRealtime`), and a health text flash. `PlayerController` additionally runs `HitFlash()` which turns all player renderers red for 0.15 real seconds. Slow-mo coroutine checks `health > 0` before restoring `timeScale` to avoid un-pausing on game over. `CameraShake` uses `transform.localPosition` offset from `originalPosition` captured in `Awake` and uses `Time.unscaledDeltaTime` so it works during slow-mo.

**Timer**: `AddTime(float amount)` adds time and flashes the timer number yellow using an `isFlashing` flag to prevent `Update` from overwriting the flash color mid-coroutine.

**UI number flashes**: score number flashes green on increase, health number flashes red on damage, timer number flashes yellow when time is added. Each stat uses two separate TMP objects: a static label (e.g. `"Score: "`) and a dynamic value (e.g. `"0"`). Code only updates the value object (`scoreValueText`, `healthValueText`, `timerValueText`). Flash coroutines set `fontSize = originalSize * 1.1f`, apply a `<color>` rich text tag, yield, then restore `fontSize`. Original sizes are captured once in `Start()` so repeated flashes always scale from the true baseline — never use `<size>` rich text tags for resizing.

**Randomized controls**: every `controlsRandomizeInterval` seconds (default 5), horizontal and vertical axes are each randomly inverted for 1 second then reset. All player renderers cycle through rainbow colors and a "Controls Scrambled!" UI text appears and cycles through rainbow colors during this period.

**Player tilt**: `TiltPlayer()` runs each `FixedUpdate` and lerps the player's Y rotation toward `horizontalInput * tiltAngle` to simulate turning. Snaps back to 0 when input is released.

**Tags in use**: `"Player"`, `"Traffic"`, `"Powerup"`, `"Crate"` — these must match GameObject tag assignments in the Unity Editor.

**SpawnManager** has a `spawnTraffic` bool toggle (Inspector checkbox, default true) to disable traffic spawning during testing.

**MoveDown.cs** is a reusable component attached to traffic vehicles, powerups, and crates to push them toward the camera (negative Z) and clean them up when they pass `zDestroy`.

**BackgroundMusic**: attached to the music AudioSource. Each `Update` maps `gameManager.score` to a pitch between `minPitch` (1.0) and `maxPitch` (1.5). Pitch stays at `minPitch` until score reaches `minScore` (500), then ramps linearly up to `maxPitch` at `maxScore` (1000). `GameManager.score` is a public property with a private setter.

**Scoring** comes from two sources: `SpawnManager` calls `gameManager.UpdateScore(5)` each time traffic spawns (every `trafficSpawnTime/2` seconds, default 0.5s); and `Vehicle.Explode()` calls `gameManager.UpdateScore(pointValue)` when the player collides with a vehicle. Point values: Car = 10 (base default), Truck = 20, Bus = 30.

**FloatingText**: spawned by `Vehicle.Explode()` at the collision position. Uses a world-space `TextMeshPro` component. `SetText(string)` sets the label and starts the animation coroutine — scales from `startSize` to `endSize`, fades alpha 1→0, and rises along Y over `duration` seconds, always facing the camera. The prefab must have a `TextMeshPro` component and the `FloatingText` script attached.


### Coordinate convention

The road runs along the Z-axis. "Forward" for incoming traffic is negative Z (toward the camera). Player movement uses `Vector3.forward` (positive Z) for accelerating and `Vector3.right` for lateral movement. The player is constrained between `zMin` and `zMax`.
