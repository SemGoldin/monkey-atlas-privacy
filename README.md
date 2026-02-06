# Nut Sorting Puzzle Game

A mobile puzzle game built with Unity where players sort colored nuts onto bolts.

## Features

- 🎮 Procedural level generation
- ⭐ Star rating system based on moves
- 💾 Automatic save system
- 🔊 Audio and effects management
- 📱 Mobile-optimized (Android/iOS)
- 🎨 Easy to customize and configure

## Quick Start

See [SETUP_GUIDE.md](Assets/Documentation/SETUP_GUIDE.md) for detailed setup instructions.

### Prerequisites

- Unity 2021.3 LTS or newer
- TextMeshPro package
- Android SDK (for Android builds)

### Installation

1. Clone or download this repository
2. Open the project in Unity
3. Import TextMeshPro Essentials
4. Follow the setup guide in Assets/Documentation/

## Game Architecture

### Core Systems

- **GameManager**: Main game loop and logic
- **LevelGenerator**: Procedural level creation
- **SaveManager**: Persistent data storage
- **AudioManager**: Sound and music control
- **EffectsManager**: Visual effects and animations

### Gameplay

Players must sort colored nuts onto bolts following these rules:
- Click a bolt to pick up the top nut
- Click another bolt to place the nut
- Nuts can only be placed on empty bolts or bolts with matching color nuts
- Complete bolts by filling them with 4 nuts of the same color
- Win by completing or emptying all bolts

### Star Rating

- ⭐⭐⭐ 3 Stars: ≤ 10 moves
- ⭐⭐ 2 Stars: ≤ 20 moves
- ⭐ 1 Star: ≤ 40 moves

## Documentation

- [Complete Setup Guide](Assets/Documentation/SETUP_GUIDE.md) - Detailed setup instructions
- [README (Ukrainian)](Assets/Documentation/README.md) - Українська документація

## Project Structure

```
Assets/
├── Scenes/          # Menu and Game scenes
├── Scripts/         # All C# scripts
│   ├── Core/        # Game logic
│   ├── UI/          # User interface
│   ├── Managers/    # System managers
│   └── Data/        # Data structures and ScriptableObjects
├── Prefabs/         # Bolt and Nut prefabs
├── Resources/       # Configuration assets
└── Documentation/   # Setup guides and docs
```

## Configuration

The game uses ScriptableObjects for easy configuration:

- **LevelConfig**: Level difficulty, star thresholds, colors
- **AudioConfig**: Sound effects and music settings

## Building

### Android

1. File → Build Settings
2. Select Android platform
3. Configure Player Settings (package name, icons, etc.)
4. Build

### iOS

1. File → Build Settings
2. Select iOS platform
3. Configure Player Settings
4. Build and open in Xcode

## Customization

### Adding Custom Graphics

1. Replace Bolt and Nut prefab sprites
2. Add custom UI elements
3. Update colors in LevelConfig

### Adding Audio

1. Import audio files to Assets/Resources/Audio/
2. Assign clips in AudioConfig ScriptableObject

### Adding Effects

1. Create particle system prefabs
2. Assign to EffectsManager in scene

## License

This project is provided as-is for educational and development purposes.

## Support

For questions or support:
- Email: vaschishin.s@gmail.com

## Credits

Developed for Unity game development learning and demonstration purposes.

---

**Note**: This is a basic implementation suitable for learning and prototyping. For production use, consider adding professional assets, additional features, and thorough testing.
