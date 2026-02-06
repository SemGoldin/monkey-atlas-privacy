# Bolt & Nut Sorting Puzzle Game

A Unity-based sorting puzzle game where players organize colored nuts on bolts. This is a classic sorting puzzle mechanic with a tactile "screwing and unscrewing" theme.

## 🎮 Game Overview

Players see multiple bolts with colored nuts screwed onto them. The goal is to sort all nuts by color so that each bolt contains only nuts of a single color or is empty.

### Key Features

- **Physical Screwing Mechanics**: Realistic animations for unscrewing and screwing nuts using DOTween
- **Color Sorting Puzzle**: Sort colored nuts across multiple bolts
- **Strategic Gameplay**: Empty bolts serve as buffers for complex moves
- **Smooth Animations**: Polished movements with arc trajectories and rotation effects
- **Visual Feedback**: Clear indication of valid and invalid moves

## 🔩 Game Mechanics

### Rules

1. **Click a bolt** to pick up the top nut (with unscrewing animation)
2. **Click another bolt** to place the nut:
   - ✅ Can place on empty bolt
   - ✅ Can place on bolt if top nut is same color
   - ❌ Cannot place on bolt with different color on top
   - ❌ Cannot exceed bolt capacity
3. **Click original bolt** to return the nut (if you change your mind)

### Win Condition

Level is complete when all bolts are either:
- Completely filled with nuts of a single color, OR
- Empty

## 🎨 Technical Implementation

### Core Scripts

#### `Nut.cs`
- Represents individual nuts with colors
- Handles all nut animations using DOTween:
  - `AnimateUnscrew()` - Rotating lift animation when picked up
  - `StartHoverAnimation()` - Continuous bobbing and rotation while selected
  - `AnimateMoveToBolt()` - Arc movement to target bolt
  - `AnimateScrewDown()` - Rotating descent when placed
  - `AnimateReturnToBolt()` - Return to original position

#### `Bolt.cs`
- Manages bolt state and nut stacks
- Validates nut placement rules
- Provides visual feedback (highlighting for valid/invalid moves)
- Tracks if bolt is sorted/complete

#### `GameManager.cs`
- Generates and manages levels
- Creates bolts and distributes nuts
- Checks win conditions
- Tracks player moves

#### `BoltSelector.cs`
- Handles user input (mouse clicks)
- Manages selection state machine
- Coordinates nut movement between bolts
- Provides visual feedback for valid moves

### Animation States

The game uses a state machine for nut selection:

1. **Idle** - No nut selected, waiting for input
2. **NutSelected** - Nut is picked up and hovering
3. **Moving** - Nut is animating (prevents input)

### DOTween Animations

All animations use DOTween Pro for smooth, professional movements:

- **Unscrewing**: 360° rotation + upward movement (0.3s)
- **Hovering**: Infinite loop of bobbing + slow rotation
- **Arc Movement**: Catmull-Rom path with rotation (0.5s)
- **Screwing Down**: Downward movement with -360° rotation (0.4s)
- **Valid Highlight**: Pulsing scale animation
- **Invalid Feedback**: Shake animation

## 📁 Project Structure

```
Game1/
├── Assets/
│   ├── Scripts/
│   │   ├── Nut.cs
│   │   ├── Bolt.cs
│   │   ├── GameManager.cs
│   │   └── BoltSelector.cs
│   ├── Prefabs/
│   ├── Scenes/
│   └── Materials/
├── Packages/
│   └── manifest.json
└── ProjectSettings/
    └── ProjectSettings.asset
```

## 🎯 Level Configuration

Configure levels via `GameManager` inspector:

- `numberOfBolts` - Total number of bolts (default: 6)
- `nutsPerBolt` - Capacity of each bolt (default: 4)
- `numberOfColors` - Number of different colors (default: 4)
- `emptyBolts` - Number of empty bolts as buffers (default: 2)
- `boltSpacing` - Distance between bolts (default: 2.0)

## 🎨 Available Nut Colors

- Red
- Blue
- Green
- Yellow
- Purple
- Orange
- Cyan
- Magenta

## 🔧 Dependencies

- **Unity 2021.3+** (or compatible version)
- **DOTween Pro** (DG.Tweening) - For animations
  - Note: Package reference included in manifest.json

## 🚀 Setup Instructions

1. Open Unity Hub
2. Add the `Game1` folder as a Unity project
3. Install DOTween from Asset Store or Package Manager
4. Create prefabs for Nuts and Bolts (or use primitives)
5. Create a main scene and add GameManager
6. Press Play to start the game

## 🎮 Controls

- **Left Mouse Button** - Select/Place nuts
- Click on bolt with nuts to pick up the top nut
- Click on another bolt to place the nut
- Click on the original bolt to return the nut

## 📝 Future Enhancements

- Level progression system
- Move counter and scoring
- Undo/hint system
- Sound effects and music
- Particle effects for successful moves
- Multiple difficulty levels
- Save/load progress
- UI for level selection and settings

## 👨‍💻 Developer Notes

### Key Design Decisions

1. **State Machine for Selection**: Prevents input during animations
2. **Modular Animation System**: Each animation is self-contained
3. **Clear Separation of Concerns**: Each script has a single responsibility
4. **Visual Feedback First**: Player always knows what moves are valid
5. **Think Before Action**: Hovering state encourages strategic thinking

### Performance Considerations

- Uses object pooling for nuts (can be added)
- Animations are killed properly to prevent memory leaks
- Efficient raycasting for bolt selection

## 📄 License

Part of the Monkey Atlas project by DevZone Studios.

## 🤝 Credits

Game Design: Classic sorting puzzle mechanics
Implementation: Unity + DOTween animations
Publisher: DevZone Studios
