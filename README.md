# 3D Maze Chase Game for Unity

A complete 3D maze-based chase game implementation for Unity, featuring intelligent AI, procedural maze generation, and classic arcade-style gameplay.

## 🎮 Game Features

- **Dynamic Maze Generation**: Procedurally generated mazes with increasing complexity
- **Intelligent AI**: Four distinct ghost personalities with chase, scatter, and frightened behaviors
- **Power-up System**: Collect power pellets to temporarily turn the tables on your pursuers
- **Score System**: Track your score and compete for high scores
- **Progressive Difficulty**: Levels get more challenging as you advance
- **Polished UI**: Complete menu system with pause, game over, and level complete screens
- **3D Graphics**: Full 3D implementation with customizable visuals

## 📁 Project Structure

```
UnityScripts/
├── Core/
│   ├── GameManager.cs      - Main game controller and state management
│   └── GameState.cs         - Game state enumeration
├── Player/
│   ├── PlayerController.cs  - Player movement and collision handling
│   └── PlayerAnimation.cs   - Player visual effects and animations
├── AI/
│   └── GhostAI.cs          - Ghost AI with multiple behavior states
├── Maze/
│   ├── MazeGenerator.cs    - Procedural maze generation
│   ├── Pellet.cs           - Regular collectible pellets
│   └── PowerPellet.cs      - Special power-up pellets
└── UI/
    ├── UIManager.cs        - UI controller for all menus and HUD
    └── ScoreManager.cs     - Score tracking and high score management
```

## 🚀 Quick Start

### Prerequisites

- Unity 2020.3 LTS or newer
- TextMeshPro package (usually included by default)

### Installation

1. Clone this repository or download the scripts
2. Create a new Unity 3D project
3. Copy the `UnityScripts` folder to your `Assets/Scripts` directory
4. Follow the detailed setup guide in [README_SETUP.md](README_SETUP.md)

## 📖 Documentation

For detailed scene setup instructions, please refer to:
- **[README_SETUP.md](README_SETUP.md)** - Complete step-by-step Unity scene setup guide (English/Ukrainian)

The setup guide includes:
- Scene hierarchy structure
- Component configuration
- Prefab creation
- Material setup
- UI implementation
- Testing and troubleshooting

## 🎯 Game Mechanics

### Player Controls
- **WASD** or **Arrow Keys**: Move player
- **ESC** or **P**: Pause game

### Gameplay
- Collect all pellets to complete the level
- Avoid ghosts or they will catch you
- Collect power pellets to temporarily eat ghosts
- Score points by collecting pellets and eating frightened ghosts
- Complete increasingly difficult levels

### Ghost AI Behaviors

1. **Blinky (Red)**: Direct chaser - follows player directly
2. **Pinky (Pink)**: Ambusher - tries to get ahead of the player
3. **Inky (Cyan)**: Unpredictable - uses complex targeting
4. **Clyde (Orange)**: Random - chases when far, scatters when close

Each ghost alternates between:
- **Scatter Mode**: Return to their corner
- **Chase Mode**: Hunt the player
- **Frightened Mode**: Run away (when player has power-up)

## 🎨 Customization

All scripts are designed to be easily customizable through the Unity Inspector:

- **Movement speeds**: Adjust player and ghost velocities
- **AI timings**: Modify behavior state durations
- **Maze size**: Configure maze dimensions
- **Scoring**: Change point values for collectibles
- **Visual effects**: Customize materials, particles, and lighting

## 🏗️ Architecture

### Design Patterns Used

- **Singleton Pattern**: GameManager and ScoreManager for global access
- **State Pattern**: Ghost AI behavior states
- **Event System**: UI updates and game state changes
- **Component-Based**: Modular scripts for easy maintenance

### Key Systems

1. **Game State Management**: Centralized control of game flow
2. **AI State Machine**: Flexible ghost behavior system
3. **Procedural Generation**: Dynamic maze creation
4. **Score System**: Persistent high score tracking
5. **UI System**: Complete menu and HUD management

## 🔧 Technical Details

### Performance Considerations

- Rigidbody-based physics for smooth movement
- Efficient collision detection using triggers
- Optimized raycast-based pathfinding
- Object pooling ready for performance optimization

### Extensibility

The codebase is designed for easy extension:
- Add new ghost AI behaviors
- Implement different maze generation algorithms
- Create custom power-ups
- Add new game modes
- Integrate multiplayer features

## 🐛 Troubleshooting

Common issues and solutions are documented in the [README_SETUP.md](README_SETUP.md) file under the "Troubleshooting" section.

## 🤝 Contributing

Feel free to:
- Report bugs
- Suggest features
- Submit pull requests
- Share your implementations

## 📝 License

This project is provided as-is for educational and development purposes. 

**Important Note**: This is a generic maze chase game implementation with original game mechanics. It does not use any copyrighted Pac-Man assets, names, or specific character designs. If you plan to publish a game based on this code, ensure you create original artwork and branding.

## 🙏 Credits

- Original game concept inspired by classic arcade maze games
- Developed as a complete Unity implementation example
- All code is original and documented for learning purposes

## 📧 Contact

For questions or support regarding the privacy policy application, contact: vaschishin.s@gmail.com

---

**Happy Game Development! 🎮**

---

## 🌍 Українська версія

### Опис

Повна реалізація 3D гри-лабіринту з переслідуванням для Unity, що включає інтелектуальний ШІ, процедурну генерацію лабіринтів та класичний аркадний геймплей.

### Особливості

- Динамічна генерація лабіринтів
- Чотири унікальні типи ШІ ворогів
- Система бонусів
- Прогресивна складність
- Повний UI з меню та HUD
- 3D графіка

Детальна інструкція з налаштування доступна в файлі [README_SETUP.md](README_SETUP.md).
