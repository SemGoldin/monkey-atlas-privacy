# 3D Bolt and Nut Puzzle Game - Complete Implementation

## Project Overview

This is a complete Unity3D puzzle game implementation where players sort colored nuts on bolts. The game features state-based gameplay, animations, sound effects, and a victory condition system.

## Game Mechanics Summary

### Core Rules
- **9 bolts** arranged in 3x3 grid
- **7 colors** of nuts (Red, Blue, Green, Yellow, Purple, Orange, Pink)
- **4 nuts per color** = 28 total nuts
- **2 empty bolts** for maneuvering
- **Goal**: Sort all nuts by color, one color per bolt

### Movement Rules
1. Only the top nut can be moved from a bolt
2. Nuts can only be placed on:
   - Empty bolts
   - Bolts where the top nut matches the color
3. Click the same bolt again to cancel and return the nut

## Architecture

### State Machine
The game uses a 5-state state machine:

```
Initialization → PlayerInput ⇄ NutMovement → SuccessSignaling → Victory
                     ↑              ↓
                     └──────────────┘
```

### Class Structure

#### Core Classes
1. **GameState.cs** - Enum defining game states
2. **NutColor.cs** - Enum defining 7 nut colors
3. **Nut.cs** - Individual nut behavior and animations
4. **Bolt.cs** - Bolt behavior and nut management
5. **GameManager.cs** - Central game controller (Singleton)

#### Support Classes
6. **UIManager.cs** - UI elements and button handlers
7. **AudioManager.cs** - Centralized audio control (Singleton)
8. **InputManager.cs** - Mouse and touch input handling
9. **LevelManager.cs** - Level progression and scoring
10. **SettingsManager.cs** - Game settings persistence (Singleton)
11. **EffectsManager.cs** - Particle effects and visual feedback (Singleton)
12. **GameConstants.cs** - Game-wide constants and utilities

#### Test Classes
13. **GameLogicTests.cs** - Automated tests for game logic

## File Structure

```
monkey-atlas-privacy/
├── Assets/
│   └── Scripts/
│       ├── GameState.cs
│       ├── NutColor.cs
│       ├── Nut.cs
│       ├── Bolt.cs
│       ├── GameManager.cs
│       ├── UIManager.cs
│       ├── AudioManager.cs
│       ├── InputManager.cs
│       ├── LevelManager.cs
│       ├── SettingsManager.cs
│       ├── EffectsManager.cs
│       ├── GameConstants.cs
│       └── Tests/
│           └── GameLogicTests.cs
├── .gitignore
├── README.md
├── GAME_DESIGN_UA.md (Ukrainian documentation)
├── UNITY_SETUP.md (Unity scene setup guide)
└── privacy-policy
```

## Key Features Implemented

### 1. Game Initialization
- Spawns 9 bolts in 3x3 grid
- Creates 28 nuts with random color distribution
- Fills 7 bolts with 4 nuts each
- Leaves 2 bolts empty
- Saves initial game state

### 2. Player Interaction
- Mouse and touch input support
- Click to lift top nut from bolt
- Click another bolt to transfer
- Click same bolt to cancel
- Visual feedback for selected bolt

### 3. Nut Movement Validation
- Checks if target bolt has space
- Validates color matching with top nut
- Prevents invalid moves
- Enforces game rules before animation

### 4. Animations
- **Unscrewing**: Nut lifts up while rotating
- **Screwing**: Nut moves to target and rotates
- **Shake**: Side-to-side motion for invalid moves
- Smooth interpolation using coroutines

### 5. Audio System
- Screwing sound effect
- Unscrewing sound effect
- Error sound for invalid moves
- Victory sound on win
- Click sounds for buttons
- Background music support
- Volume controls for SFX and music

### 6. Visual Effects
- Particle effects for nut contact
- Victory celebration effects
- Sparkle effects for highlights
- Customizable effect prefabs

### 7. Victory Condition
- Checks if all nuts are sorted
- Validates 7 full bolts with matching colors
- Triggers victory state
- Shows winner panel
- Plays victory sound and effects

### 8. UI System
- Victory panel with "Winner" text
- Level and moves display
- Menu, Restart, and Bonus buttons
- Clean and responsive interface

### 9. Progress Tracking
- Level progression system
- High score tracking
- PlayerPrefs persistence
- Settings save/load

### 10. Settings Management
- SFX volume control
- Music volume control
- Vibration toggle
- Tutorial completion tracking
- Persistent settings

## Code Quality Features

### Design Patterns Used
- **Singleton Pattern**: GameManager, AudioManager, SettingsManager, EffectsManager
- **State Machine Pattern**: GameState enum with state transitions
- **Observer Pattern**: Event-driven button clicks
- **Factory Pattern**: Prefab instantiation for bolts and nuts

### Best Practices
- Clear separation of concerns
- Comprehensive XML documentation
- Consistent naming conventions
- Null safety checks
- Resource cleanup
- DontDestroyOnLoad for manager singletons

### Performance Considerations
- Object pooling ready (can be added for nuts)
- Coroutines for smooth animations
- Efficient collision detection
- Minimal garbage collection

## Testing

### Test Coverage
- Nut color enum validation (7 colors)
- Game state enum validation (5 states)
- Bolt capacity math verification
- Color matching logic
- Victory condition logic

### How to Run Tests
1. Attach `GameLogicTests.cs` to a GameObject in scene
2. Enter Play mode
3. Check Console for test results

## Setup Instructions

### Quick Start
1. Open Unity 2020.3 or later
2. Create new 3D project
3. Copy all files from `Assets/Scripts/` to your project
4. Follow detailed setup in `UNITY_SETUP.md`

### Required Assets
- Bolt 3D model or primitive cylinder
- Nut 3D model or primitive torus
- Audio clips (screwing, unscrewing, error, victory sounds)
- UI sprites (buttons, panels)
- Optional: Particle system prefabs

## Localization

The game includes Ukrainian documentation in `GAME_DESIGN_UA.md` matching the original problem statement requirements.

## Future Enhancements

### Potential Additions
- Multiple difficulty levels
- Time-based challenges
- Hint system
- Undo/redo functionality
- Achievement system
- Leaderboards
- Daily challenges
- Multiple themes
- Tutorial system
- More levels with variations

### Technical Improvements
- Object pooling for better performance
- Save/load game state
- Replay system
- Analytics integration
- Cloud save support
- Multiplayer mode

## Performance Metrics

### Memory Usage
- Minimal memory footprint
- ~28 nut objects + 9 bolt objects
- Efficient resource management

### Frame Rate
- Smooth 60 FPS target
- Optimized animations
- No heavy computations in Update()

## Troubleshooting

### Common Issues

**Nuts not appearing**
- Check prefab assignments in GameManager
- Verify Nut.cs script is attached
- Check console for initialization errors

**Click detection fails**
- Ensure bolts have colliders
- Verify camera reference in InputManager
- Check correct layers assigned

**Animations jerky**
- Adjust animation speeds in Nut.cs
- Check Time.deltaTime usage
- Verify coroutines are running

**No audio**
- Assign audio clips in AudioManager
- Check AudioSource components
- Verify volume settings

## Credits

Implementation follows the Ukrainian game design specification provided in the problem statement.

## License

This implementation is part of the Monkey Atlas project.

## Support

For issues or questions, refer to:
- UNITY_SETUP.md for scene setup
- GAME_DESIGN_UA.md for design details
- README.md for overview
