# Bolt and Nut Puzzle Game

A 3D puzzle game where players sort colored nuts on bolts.

## Game Description

### Objective
Sort all nuts by color onto separate bolts. Each bolt can hold up to 4 nuts, and nuts can only be placed on bolts with matching top nut colors or empty bolts.

### Game Rules
- **9 bolts total**: 7 filled with nuts, 2 empty
- **7 colors**: Red, Blue, Green, Yellow, Purple, Orange, Pink
- **4 nuts per color**: 28 total nuts
- Nuts can only be moved to:
  - Empty bolts
  - Bolts where the top nut matches the color being placed

### Game States

#### 1. Initialization
- Program spawns nuts with random colors on bolts
- Saves initial positions
- Sets level to 1 if first time playing
- Buttons and bolts enter waiting state

#### 2. Player Input
- Wait for player to click on a bolt or button
- Available buttons: Exit to Menu, Restart, Bonus

#### 3. Nut Movement
- Click on bolt with nuts: lifts the top nut with unscrewing animation
- Click on another bolt: attempts to transfer the nut
  - **Valid move**: Nut screws onto target bolt with animation and sound
  - **Invalid move**: Nut shakes side-to-side with error sound
- Click on same bolt: returns nut to original position

#### 4. Success Signaling
- **Success**: 
  - Screwing sound effect plays
  - Visual effect shows nut contact
  - Nut is transferred
- **Failure**:
  - Error sound plays
  - Nut shakes side-to-side ("no" animation)
  - Nut returns to original position

#### 5. Victory
- All nuts are sorted by color (one color per bolt)
- Victory sound plays
- "Winner" panel displays

## Project Structure

```
Assets/
└── Scripts/
    ├── GameState.cs       - Game state enum definition
    ├── NutColor.cs        - Nut color enum definition
    ├── Nut.cs             - Nut behavior and animations
    ├── Bolt.cs            - Bolt behavior and nut management
    ├── GameManager.cs     - Main game logic and state management
    ├── UIManager.cs       - UI elements and button handlers
    └── LevelManager.cs    - Level progression and scoring
```

## Implementation Details

### Classes

#### GameState
Enum representing the five game states:
- `Initialization`
- `PlayerInput`
- `NutMovement`
- `SuccessSignaling`
- `Victory`

#### NutColor
Enum for the 7 available colors:
- Red, Blue, Green, Yellow, Purple, Orange, Pink

#### Nut
Manages individual nut behavior:
- Color assignment and visual representation
- Unscrewing animation (moves up, rotates)
- Screwing animation (moves to target, rotates opposite direction)
- Shake animation for invalid moves
- Return to bolt animation

#### Bolt
Manages bolt behavior:
- Holds up to 4 nuts
- Provides positions for nut placement
- Validates if a nut can be accepted (color matching)
- Tracks sorted state (all nuts same color)
- Handles click events

#### GameManager
Central game controller:
- Singleton pattern for global access
- Spawns bolts in 3x3 grid
- Spawns nuts with random colors
- Handles bolt click logic
- Manages game state transitions
- Checks victory condition
- Plays audio feedback

#### UIManager
Manages UI elements:
- Victory panel display
- Level and moves counter
- Button event handlers (Menu, Restart, Bonus)

#### LevelManager
Handles progression:
- Level tracking with PlayerPrefs
- Score calculation
- Progress persistence

## Setup Requirements

### Unity Version
- Unity 2020.3 or later recommended

### Required Components
- **Prefabs**: BoltPrefab, NutPrefab
- **Audio Clips**: Screwing sound, Error sound, Victory sound
- **UI Elements**: Victory panel, buttons, text displays

### Scene Setup
1. Create empty GameObject named "GameManager"
2. Attach `GameManager` script
3. Assign prefabs and audio clips in inspector
4. Create UI canvas with:
   - Victory panel (hidden by default)
   - Menu button
   - Restart button
   - Bonus button
   - Level text
   - Moves text
5. Attach `UIManager` script to UI GameObject

## How to Play

1. **Start**: Game initializes with nuts randomly distributed on 7 bolts
2. **Select**: Click on a bolt with nuts to lift the top nut
3. **Move**: Click on another bolt to transfer the nut
   - Must match top nut color or be empty
4. **Cancel**: Click on the same bolt to return the nut
5. **Win**: Sort all nuts by color onto separate bolts

## Features

- **Visual Feedback**: Rotating animations for screwing/unscrewing
- **Audio Feedback**: Different sounds for success, error, and victory
- **State Management**: Clear game state flow prevents invalid actions
- **Progress Tracking**: Saves level progression
- **Move Counter**: Tracks number of moves made

## Technical Notes

- Uses Unity's built-in physics and input system
- Coroutines for smooth animations
- Singleton pattern for GameManager access
- PlayerPrefs for data persistence
- Event-driven architecture for button interactions

## Future Enhancements

- Multiple difficulty levels with different bolt/nut configurations
- Time-based challenges
- Hint system for stuck players
- Undo last move feature
- Achievement system
- Particle effects for nut contact
- Background music
- Sound settings menu
