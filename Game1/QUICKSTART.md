# Quick Start Guide - Bolt & Nut Puzzle

## Getting Started in 5 Minutes

### 1. Open the Project
1. Open Unity Hub
2. Click "Add" and select the `Game1` folder
3. Open the project with Unity 2021.3 or later

### 2. Install DOTween (Required)
DOTween is essential for the game animations.

**Option A: Asset Store**
1. Open Window → Asset Store
2. Search for "DOTween Pro"
3. Download and import

**Option B: Package Manager**
1. Window → Package Manager
2. Click "+" → Add package from git URL
3. Enter: `https://github.com/Demigiant/dotween.git`

### 3. Create a Simple Scene

#### Create the Camera
1. GameObject → Camera (if not present)
2. Position: (0, 5, -10)
3. Rotation: (30, 0, 0)

#### Create the Game Manager
1. GameObject → Create Empty
2. Rename to "GameManager"
3. Add Component → GameManager script
4. Configure in Inspector:
   - Number Of Bolts: 6
   - Nuts Per Bolt: 4
   - Number Of Colors: 4
   - Empty Bolts: 2
   - Bolt Spacing: 2
   - Start Position: (-5, 0, 0)

#### Create Nut Prefab (Simple Version)
1. GameObject → 3D Object → Cylinder
2. Scale: (0.5, 0.2, 0.5)
3. Add Component → Nut script
4. Save as Prefab in Assets/Prefabs/Nut.prefab
5. Delete from scene

#### Create Bolt Prefab (Simple Version)
1. GameObject → 3D Object → Cylinder
2. Scale: (0.3, 2, 0.3)
3. Add Component → Bolt script
4. Configure in Inspector:
   - Max Capacity: 4
   - Nut Spacing: 0.3
5. Add Collider (should have one by default)
6. Save as Prefab in Assets/Prefabs/Bolt.prefab
7. Delete from scene

#### Connect Prefabs to Game Manager
1. Select GameManager in Hierarchy
2. Drag Nut prefab to "Nut Prefab" field
3. Drag Bolt prefab to "Bolt Prefab" field

### 4. Press Play!
Click the Play button and start playing:
- Click on a bolt to pick up a nut
- Click on another bolt to place it
- Click the original bolt to return it

## Common Issues

### "DOTween not found"
- Install DOTween from Asset Store or Package Manager
- Add `using DG.Tweening;` to scripts

### "Nuts don't move"
- Make sure Bolt prefab has a Collider component
- Check Camera is positioned correctly
- Verify BoltSelector component is on GameManager

### "Can't click bolts"
- Ensure Bolt prefab has a Collider
- Check Camera has default MainCamera tag
- Verify raycast distance in BoltSelector (default: 100)

## Next Steps

### Improve Visuals
1. Import better 3D models for nuts and bolts
2. Add materials with metallic shaders
3. Add lighting (Directional Light)
4. Add particle effects

### Add UI
1. Create Canvas for UI
2. Add move counter display
3. Add "Win" screen
4. Add "Reset" button

### Add Levels
1. Create multiple level configurations
2. Add level progression
3. Save player progress

### Polish
1. Add sound effects (screw/unscrew sounds)
2. Add background music
3. Add haptic feedback (mobile)
4. Optimize for mobile devices

## Testing the Game

Try these scenarios:
1. **Easy Move**: Pick a nut and place it on empty bolt
2. **Color Match**: Pick a nut and place on same color
3. **Invalid Move**: Try placing different colors together (should show feedback)
4. **Win Condition**: Sort all nuts by color
5. **Return Move**: Pick a nut and click original bolt

## Performance Tips

- Keep bolt count reasonable (6-10 bolts)
- Use simple meshes for nuts/bolts
- Optimize materials (no reflections if not needed)
- Test on target device (mobile/PC)

## Support

For issues or questions:
- Check README.md for detailed documentation
- Review script comments
- Unity Documentation: docs.unity3d.com
- DOTween Documentation: dotween.demigiant.com

Happy Puzzle Making! 🔩🎮
