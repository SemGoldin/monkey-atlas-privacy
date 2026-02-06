# Nut Sorting Puzzle Game - Complete Setup Guide

## Project Overview

This is a complete implementation of a nut sorting puzzle mobile game in Unity featuring:
- Procedural level generation
- Star rating system based on moves
- Automatic save system
- Audio and effects management
- Two scenes (Menu and Game)

## Quick Start Guide

### Step 1: Create Unity Project
1. Create new Unity 2021.3 LTS or newer project
2. Copy all files from this repository into your Unity project

### Step 2: Import Required Packages
1. Window → Package Manager
2. Install "TextMeshPro" (if not already installed)
3. Import TMP Essential Resources when prompted

### Step 3: Create ScriptableObject Configurations

#### Level Configuration
1. Right-click in Assets/Resources folder
2. Create → Game → Level Configuration
3. Name it "DefaultLevelConfig"
4. Configure settings:
   ```
   Min Bolts: 3
   Max Bolts: 8
   Nuts Per Bolt: 4
   Bolts Increase Every N Levels: 5
   Three Star Max Moves: 10
   Two Star Max Moves: 20
   One Star Max Moves: 40
   ```
5. Add 8 colors to Nut Colors array (Red, Blue, Green, Yellow, Cyan, Magenta, Orange, Purple)

#### Audio Configuration
1. Right-click in Assets/Resources folder
2. Create → Game → Audio Configuration
3. Name it "DefaultAudioConfig"
4. Configure volumes:
   ```
   Master Volume: 1.0
   SFX Volume: 1.0
   Music Volume: 0.5
   ```

### Step 4: Create Prefabs

#### Bolt Prefab
1. GameObject → 2D Object → Sprite → Square
2. Name: "Bolt"
3. Set Transform Scale: (0.4, 1.2, 1)
4. Add Component: Bolt Script
5. Add Component: BoltInputHandler Script
6. Add Component: Box Collider 2D
7. Configure Bolt Script:
   - Capacity: 4
   - Nut Spacing: 0.3
8. Set Sprite Renderer Color: Gray (128, 128, 128, 255)
9. Drag to Assets/Prefabs folder to create prefab
10. Delete from scene

#### Nut Prefab
1. GameObject → 2D Object → Sprite → Circle
2. Name: "Nut"
3. Set Transform Scale: (0.25, 0.25, 1)
4. Add Component: Nut Script
5. Add Component: Circle Collider 2D
6. Set Sprite Renderer Order in Layer: 1
7. Drag to Assets/Prefabs folder to create prefab
8. Delete from scene

### Step 5: Setup Menu Scene

1. **Create Scene:**
   - File → New Scene
   - Save as "Assets/Scenes/Menu.unity"

2. **Create Canvas:**
   - GameObject → UI → Canvas
   - Canvas Scaler Settings:
     - UI Scale Mode: Scale With Screen Size
     - Reference Resolution: 1080 x 1920
     - Screen Match Mode: Match Width Or Height
     - Match: 0.5

3. **Create MenuManager:**
   - Create Empty GameObject named "MenuManager"
   - Add MenuManager component
   - Assign DefaultAudioConfig in Inspector

4. **Create UI Elements:**

   **Title (TextMeshPro):**
   - Right-click Canvas → UI → Text - TextMeshPro
   - Name: "Title"
   - Text: "Nut Sorting Puzzle"
   - Font Size: 72
   - Alignment: Center
   - Position: Top of screen

   **Current Level Display:**
   - Create TextMeshPro
   - Name: "CurrentLevelText"
   - Position: Below title

   **Total Score Display:**
   - Create TextMeshPro
   - Name: "TotalScoreText"
   - Position: Below current level

   **Play Button:**
   - GameObject → UI → Button - TextMeshPro
   - Name: "PlayButton"
   - Text: "Play"
   - Position: Center

   **Continue Button:**
   - Create Button
   - Name: "ContinueButton"
   - Text: "Continue"
   - Position: Below Play button

   **Settings Button:**
   - Create Button
   - Name: "SettingsButton"
   - Text: "Settings"
   - Position: Below Continue button

   **Quit Button:**
   - Create Button
   - Name: "QuitButton"
   - Text: "Quit"
   - Position: Below Settings button

   **Settings Panel:**
   - Create UI → Panel
   - Name: "SettingsPanel"
   - Set initially inactive (uncheck in Inspector)
   - Add UI elements inside:
     - Toggle "SoundToggle" with label "Sound"
     - Toggle "MusicToggle" with label "Music"
     - Toggle "EffectsToggle" with label "Effects"
     - Button "CloseButton" with text "Close"

5. **Link Components:**
   - Select MenuManager GameObject
   - Drag all UI elements to corresponding fields in Inspector:
     - Current Level Text → currentLevelText
     - Total Score Text → totalScoreText
     - Play Button → playButton
     - Continue Button → continueButton
     - Settings Button → settingsButton
     - Quit Button → quitButton
     - Settings Panel → settingsPanel
     - Sound Toggle → soundToggle
     - Music Toggle → musicToggle
     - Effects Toggle → effectsToggle
     - Close Button → closeSettingsButton
   - Assign DefaultAudioConfig → audioConfig

### Step 6: Setup Game Scene

1. **Create Scene:**
   - File → New Scene
   - Save as "Assets/Scenes/Game.unity"

2. **Configure Main Camera:**
   - Select Main Camera
   - Set Position: (0, 0, -10)
   - Set Projection: Orthographic
   - Set Size: 5
   - Set Background: Dark color (your choice)

3. **Create GameManager:**
   - Create Empty GameObject named "GameManager"
   - Add GameManager component
   - Assign DefaultLevelConfig in Inspector
   - Assign DefaultAudioConfig in Inspector

4. **Create LevelGenerator:**
   - Create Empty GameObject named "LevelGenerator"
   - Add LevelGenerator component
   - Assign Bolt Prefab from Assets/Prefabs
   - Assign Nut Prefab from Assets/Prefabs
   - Set Bolt Spacing: 1.5
   - Set Start Position: (-3, -2, 0)
   - Drag LevelGenerator to GameManager's levelGenerator field

5. **Create Game Canvas:**
   - GameObject → UI → Canvas
   - Name: "GameCanvas"
   - Canvas Scaler: Scale With Screen Size (1080 x 1920)

6. **Create GameUIManager:**
   - Add GameUIManager component to Canvas
   - Drag Canvas reference to GameManager's gameUIManager field

7. **Create HUD Elements:**

   **Level Text:**
   - Create TextMeshPro under Canvas
   - Name: "LevelText"
   - Position: Top center
   - Text: "Level 1"
   - Font Size: 48

   **Moves Text:**
   - Create TextMeshPro
   - Name: "MovesText"
   - Position: Top left
   - Text: "Moves: 0"
   - Font Size: 36

   **Star Display:**
   - Create 3 UI Images
   - Name: "Star1", "Star2", "Star3"
   - Position: Top right (horizontal row)
   - Set sprite to a star shape or circle
   - Color: Yellow/Gray

   **Pause Button:**
   - Create Button
   - Name: "PauseButton"
   - Position: Top right corner
   - Text: "||"

8. **Create Level Complete Panel:**
   - Create UI → Panel under Canvas
   - Name: "LevelCompletePanel"
   - Set initially inactive
   - Add background (semi-transparent)
   - Add child elements:
     - TextMeshPro "CompleteMessage" → "Level Complete!"
     - TextMeshPro "CompleteMoves" → "Moves: 0"
     - 3 Images for stars (CompleteStar1, CompleteStar2, CompleteStar3)
     - Button "NextLevelButton" → "Next Level"
     - Button "RestartButton" → "Restart"
     - Button "MenuButton" → "Menu"

9. **Create Pause Panel:**
   - Create UI → Panel under Canvas
   - Name: "PausePanel"
   - Set initially inactive
   - Add child elements:
     - TextMeshPro "PausedText" → "Paused"
     - Button "ResumeButton" → "Resume"
     - Button "PauseRestartButton" → "Restart"
     - Button "PauseMenuButton" → "Menu"
     - Toggle "SoundToggle" with label
     - Toggle "MusicToggle" with label
     - Toggle "EffectsToggle" with label

10. **Link GameUIManager Components:**
    - Select Canvas (with GameUIManager)
    - Link all UI elements in Inspector:
      - Level Text → levelText
      - Moves Text → movesText
      - Star Images array (3 elements) → starImages
      - Level Complete Panel → levelCompletePanel
      - Complete Message → completeMessageText
      - Complete Moves → completeMoves
      - Complete Stars array → completeStarImages
      - Next Level Button → nextLevelButton
      - Restart Button → restartButton
      - Menu Button → menuButton
      - Pause Panel → pausePanel
      - Resume Button → resumeButton
      - Pause Restart Button → pauseRestartButton
      - Pause Menu Button → pauseMenuButton
      - Toggles → soundToggle, musicToggle, effectsToggle

### Step 7: Configure Build Settings

1. **Add Scenes:**
   - File → Build Settings
   - Click "Add Open Scenes" with Menu scene open
   - Open Game scene and add it
   - Ensure Menu is index 0, Game is index 1

2. **Platform Settings (for Android):**
   - Select Android platform
   - Click "Switch Platform"
   - Player Settings:
     - Company Name: Your Company
     - Product Name: Nut Sorting Puzzle
     - Package Name: com.yourcompany.nutpuzzle
     - Version: 1.0.0
     - Default Orientation: Portrait
     - Minimum API Level: 24
     - Target API Level: Automatic (highest installed)
     - Scripting Backend: IL2CPP
     - Target Architectures: ARM64 ✓

### Step 8: Testing

1. **Test Menu Scene:**
   - Open Menu scene
   - Press Play
   - Verify buttons work
   - Check settings toggles
   - Verify scene transition to Game

2. **Test Game Scene:**
   - Open Game scene
   - Press Play
   - Verify level generates
   - Test nut picking and placing
   - Verify move counter
   - Test invalid moves (should play error sound)
   - Complete a level to test victory condition
   - Test pause functionality

## Gameplay Mechanics

### Rules:
1. Click a bolt to pick up the top nut
2. Click same bolt again to return the nut
3. Click different bolt to place the nut
4. Nuts can only be placed on empty bolts or bolts with same color nuts
5. Bolt is complete when all 4 nuts are same color

### Win Condition:
- All bolts are either empty or completely filled with same color nuts

### Star Rating:
- 3 stars: ≤ 10 moves
- 2 stars: ≤ 20 moves
- 1 star: ≤ 40 moves
- 0 stars: > 40 moves

## Features

### Auto-Save System
- Saves after every move
- Saves current level progress
- Can resume unfinished level
- Saves best scores and stars

### Audio System
- Separate controls for SFX and Music
- Background music for Menu and Game
- Sound effects for all actions
- Persistent settings

### Effects System
- Particle effects for events (when prefabs assigned)
- Scale animations
- Rotation animations
- Can be toggled on/off

## Troubleshooting

### Issue: UI elements not visible
- Check Canvas Scaler settings
- Verify all UI elements are children of Canvas
- Check Camera culling mask includes UI layer

### Issue: Clicks not registering
- Add EventSystem to scene (GameObject → UI → Event System)
- Verify BoltInputHandler is attached to Bolt prefabs
- Check that bolts have Collider2D components

### Issue: Bolts/Nuts not appearing
- Verify prefabs are assigned in LevelGenerator
- Check prefabs have required components
- Verify Camera can see the bolts (check z-positions)

### Issue: Audio not playing
- Assign AudioConfig in managers
- Check AudioClips are assigned in AudioConfig
- Verify audio settings toggles are ON

## Optional Enhancements

1. **Visual Improvements:**
   - Replace square/circle sprites with custom artwork
   - Add animations to nuts and bolts
   - Improve UI design with custom graphics
   - Add particle effects (assign prefabs in EffectsManager)

2. **Gameplay:**
   - Add hint system
   - Add undo move functionality
   - Add different level types
   - Add timer mode

3. **Monetization:**
   - Integrate AdMob
   - Add In-App Purchases
   - Add reward system

## File Structure Summary

```
Assets/
├── Scenes/
│   ├── Menu.unity
│   └── Game.unity
├── Scripts/
│   ├── Core/
│   │   ├── Nut.cs
│   │   ├── Bolt.cs
│   │   ├── BoltInputHandler.cs
│   │   ├── LevelGenerator.cs
│   │   └── GameManager.cs
│   ├── UI/
│   │   ├── MenuManager.cs
│   │   └── GameUIManager.cs
│   ├── Managers/
│   │   ├── SaveManager.cs
│   │   ├── AudioManager.cs
│   │   └── EffectsManager.cs
│   └── Data/
│       ├── GameData.cs
│       ├── LevelConfig.cs
│       └── AudioConfig.cs
├── Prefabs/
│   ├── Bolt.prefab
│   └── Nut.prefab
├── Resources/
│   ├── DefaultLevelConfig.asset
│   └── DefaultAudioConfig.asset
└── Documentation/
    ├── README.md
    └── SETUP_GUIDE.md
```

## Support

For questions or issues:
- Email: vaschishin.s@gmail.com

---

**Note:** This is a basic implementation. For a production-ready game, consider adding professional graphics, sounds, additional difficulty levels, and monetization.
