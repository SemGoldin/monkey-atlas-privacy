# Unity Scene Setup Guide

## Required Setup for Bolt and Nut Puzzle Game

### 1. Create Main Scene

Create a new scene called "GameScene" with the following structure:

### 2. Scene Hierarchy

```
GameScene
├── Main Camera
├── Directional Light
├── GameManager (Empty GameObject)
│   ├── GameManager.cs
│   └── AudioManager.cs
├── InputManager (Empty GameObject)
│   └── InputManager.cs
├── UI Canvas
│   ├── UIManager.cs
│   ├── Victory Panel
│   │   ├── Background Image
│   │   └── Winner Text
│   ├── HUD
│   │   ├── Level Text
│   │   └── Moves Text
│   └── Buttons
│       ├── Menu Button
│       ├── Restart Button
│       └── Bonus Button
└── SettingsManager (Empty GameObject)
    └── SettingsManager.cs
```

### 3. GameManager Setup

1. Create an empty GameObject named "GameManager"
2. Attach the `GameManager.cs` script
3. In the Inspector, configure:
   - **Game Configuration**:
     - Total Bolts: 9
     - Empty Bolts: 2
     - Nuts Per Color: 4
   - **Prefabs**:
     - Bolt Prefab: Assign your bolt prefab
     - Nut Prefab: Assign your nut prefab

### 4. Create Prefabs

#### Bolt Prefab
1. Create a 3D Cylinder GameObject
2. Scale it to look like a bolt (e.g., 0.2, 1, 0.2)
3. Add a Collider component (Box Collider or Mesh Collider)
4. Attach the `Bolt.cs` script
5. Configure in Inspector:
   - Max Capacity: 4
   - Nut Spacing: 0.3
   - Unscrew Height: 1.0
6. Save as prefab in `Assets/Prefabs/BoltPrefab`

#### Nut Prefab
1. Create a 3D Torus or custom nut mesh
2. Scale appropriately (e.g., 0.3, 0.15, 0.3)
3. Add a Renderer component with a material
4. Attach the `Nut.cs` script
5. Configure in Inspector:
   - Move Speed: 2.0
   - Rotation Speed: 360
   - Shake Amount: 0.1
6. Save as prefab in `Assets/Prefabs/NutPrefab`

### 5. AudioManager Setup

1. The AudioManager will be created automatically by GameManager
2. Create audio clips for:
   - Screwing sound (mechanical screwing noise)
   - Unscrewing sound (mechanical unscrewing noise)
   - Error sound (buzz or negative beep)
   - Victory sound (celebratory music/sound)
   - Click sound (button click)
   - Background music (optional)
3. Assign these in the AudioManager Inspector

### 6. UI Setup

#### Canvas Settings
- Render Mode: Screen Space - Overlay
- Canvas Scaler: Scale With Screen Size
- Reference Resolution: 1920 x 1080

#### Victory Panel
1. Create Panel named "Victory Panel"
2. Set anchors to stretch (full screen)
3. Set background color with semi-transparent black
4. Add Text child named "Winner Text"
5. Configure text:
   - Font Size: 72
   - Alignment: Center
   - Text: "Winner!"
   - Color: Gold/Yellow
6. Initially set Victory Panel active: false

#### HUD Elements
1. Level Text (top-left)
   - Text: "Level 1"
   - Font Size: 36
2. Moves Text (top-right)
   - Text: "Moves: 0"
   - Font Size: 36

#### Buttons
1. Menu Button (top-left, below level)
   - Text: "Menu"
   - On Click: UIManager.OnMenuButtonClicked
2. Restart Button (top-center)
   - Text: "Restart"
   - On Click: UIManager.OnRestartButtonClicked
3. Bonus Button (top-right, below moves)
   - Text: "Bonus"
   - On Click: UIManager.OnBonusButtonClicked

### 7. UIManager Setup

1. Attach `UIManager.cs` to the Canvas
2. In Inspector, assign references:
   - Victory Panel: Drag Victory Panel GameObject
   - Victory Text: Drag Winner Text
   - Menu Button: Drag Menu Button
   - Restart Button: Drag Restart Button
   - Bonus Button: Drag Bonus Button
   - Level Text: Drag Level Text
   - Moves Text: Drag Moves Text

### 8. InputManager Setup

1. Create empty GameObject named "InputManager"
2. Attach `InputManager.cs` script
3. In Inspector:
   - Main Camera: Drag Main Camera from hierarchy
   - Bolt Layer: Create and assign a layer for bolts

### 9. Camera Setup

1. Position camera to see the 3x3 grid of bolts
2. Suggested position: (0, 8, -5)
3. Rotation: (45, 0, 0)
4. Clear Flags: Skybox
5. Projection: Perspective

### 10. Lighting

1. Use default Directional Light
2. Adjust intensity and shadows as needed
3. Consider adding ambient lighting

### 11. Materials

Create materials for each nut color:
- RedMaterial (Color: Red)
- BlueMaterial (Color: Blue)
- GreenMaterial (Color: Green)
- YellowMaterial (Color: Yellow)
- PurpleMaterial (Color: Purple)
- OrangeMaterial (Color: Orange)
- PinkMaterial (Color: Pink)

Note: The `Nut.cs` script will automatically set colors programmatically, but you can create materials for better visual quality.

### 12. Build Settings

1. Add GameScene to build settings
2. Configure for target platform (PC, Android, iOS)
3. Set company name and product name
4. Configure player settings as needed

### 13. Testing

1. Enter Play mode
2. Verify:
   - 9 bolts appear in 3x3 grid
   - 7 bolts have 4 colored nuts each
   - 2 bolts are empty
   - Clicking bolts lifts nuts
   - Nuts can be transferred between bolts
   - Invalid moves show shake animation
   - Victory condition triggers when sorted
   - All sounds play correctly
   - UI buttons work

## Troubleshooting

### Nuts not appearing
- Check if NutPrefab is assigned in GameManager
- Verify Nut.cs script is attached to prefab
- Check console for errors during initialization

### Click detection not working
- Verify bolts have Collider components
- Check InputManager has correct camera reference
- Ensure bolts are on correct layer

### Animations not smooth
- Increase Move Speed in Nut script
- Adjust Rotation Speed for faster/slower rotation
- Check Time.deltaTime is being used correctly

### Audio not playing
- Verify AudioClips are assigned in AudioManager
- Check audio source volume settings
- Ensure AudioManager is not destroyed

### Victory not triggering
- Debug CheckVictoryCondition method
- Verify all 7 colors are sorted correctly
- Check that exactly 7 bolts are full and sorted
