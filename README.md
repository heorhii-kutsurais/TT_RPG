# Ability System Tech Test

The project is located on the **`develop` branch**.

Project version is **6000.0.40f1**

## How to Run
1. Open the scene located at `Assets/Scenes/AbilitySystemTest.unity`.
2. Press Play — the scene includes a controllable character with 5 abilities:
   - QuickSlash
   - CircularSlash
   - SlashAndStomp
   - Embers (Burn AoE)
   - HealingArea

## Controls
- Move: **WASD**
- Abilities: **1–5**

## Notes
- Uses **UniTask**, **Odin Inspector**, and **SerializedDictionary**, **DoTween**
- Code is separated into modules (`Abilities`, `Entity`, `Health`, etc.).
- Status effect system is implemented via `StatusController`.
- `AbilityController` handles cooldowns, input, and execution.
- **Visuals and UI** use basic placeholder assets and effects. (See comments in code.)

This is a modular and testable implementation aimed at clarity and expandability.
