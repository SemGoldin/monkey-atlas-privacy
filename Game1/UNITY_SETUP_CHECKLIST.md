# Unity Setup Checklist

Use this checklist when opening the project in Unity Editor for the first time.

## ✅ Pre-Setup (Before Opening Unity)

- [x] All scripts created (7 C# files)
- [x] Project structure ready
- [x] Configuration files in place
- [x] Documentation complete

## 📋 Unity Editor Setup

### 1. Open Project
- [ ] Open Unity Hub
- [ ] Click "Add" → Select `Game1` folder
- [ ] Choose Unity version 2021.3 or later
- [ ] Wait for project to open and import

### 2. Install DOTween (REQUIRED)
- [ ] Window → Package Manager
- [ ] Search for "DOTween" in Asset Store
- [ ] Download and Import DOTween (Free or Pro)
- [ ] Accept all files in import dialog

**Alternative**: Add via git URL: `https://github.com/Demigiant/dotween.git`

### 3. Create Scene Setup

#### Camera
- [ ] Create Main Camera (if not exists)
- [ ] Position: (0, 5, -10)
- [ ] Rotation: (30, 0, 0)
- [ ] Tag: MainCamera

#### Lighting
- [ ] Create Directional Light
- [ ] Rotation: (50, -30, 0)
- [ ] Intensity: 1

#### Game Manager
- [ ] Create Empty GameObject
- [ ] Name: "GameManager"
- [ ] Add Component: GameManager
- [ ] Add Component: BoltSelector (auto-added)

### 4. Create Prefabs

#### Nut Prefab (Simple)
- [ ] GameObject → 3D Object → Cylinder
- [ ] Name: "Nut"
- [ ] Scale: (0.5, 0.2, 0.5)
- [ ] Add Component: Nut script
- [ ] Add Component: Rigidbody (optional, set IsKinematic = true)
- [ ] Create Material with color (optional)
- [ ] Drag to Assets/Prefabs/Nut.prefab
- [ ] Delete from Hierarchy

#### Bolt Prefab (Simple)
- [ ] GameObject → 3D Object → Cylinder
- [ ] Name: "Bolt"
- [ ] Scale: (0.3, 2, 0.3)
- [ ] Position: (0, 0, 0)
- [ ] Add Component: Bolt script
- [ ] Add Component: Box Collider (or keep default)
- [ ] Configure Bolt:
  - Max Capacity: 4
  - Nut Spacing: 0.3
- [ ] Create Materials (normal/valid/invalid)
- [ ] Drag to Assets/Prefabs/Bolt.prefab
- [ ] Delete from Hierarchy

### 5. Configure Game Manager
- [ ] Select GameManager in Hierarchy
- [ ] Assign Nut Prefab field
- [ ] Assign Bolt Prefab field
- [ ] Set parameters:
  - Number Of Bolts: 6
  - Nuts Per Bolt: 4
  - Number Of Colors: 4
  - Empty Bolts: 2
  - Bolt Spacing: 2
  - Start Position: (-5, 0, 0)

### 6. Optional: Create UI

#### Canvas
- [ ] UI → Canvas
- [ ] Canvas Scaler → Scale With Screen Size

#### Move Counter
- [ ] UI → Text - TextMeshPro
- [ ] Name: "MoveCountText"
- [ ] Position: Top left
- [ ] Text: "Moves: 0"

#### Win Panel
- [ ] UI → Panel
- [ ] Name: "WinPanel"
- [ ] Set inactive by default
- [ ] Add child: Text for "Level Complete!"
- [ ] Add child: Button for "Next Level"
- [ ] Add child: Button for "Reset"

#### UI Controller
- [ ] Create Empty GameObject
- [ ] Name: "UIController"
- [ ] Add Component: UIController
- [ ] Assign UI elements

### 7. Optional: Audio Manager
- [ ] Create Empty GameObject
- [ ] Name: "AudioManager"
- [ ] Add Component: AudioManager
- [ ] Add Component: Audio Source (auto-added)
- [ ] Assign sound clips when available

### 8. Save Scene
- [ ] File → Save Scene As
- [ ] Name: "MainScene" or "GameScene"
- [ ] Save in Assets/Scenes/

## 🎮 Testing

### Basic Functionality Test
- [ ] Press Play
- [ ] Verify bolts appear in scene
- [ ] Verify nuts are on bolts
- [ ] Click a bolt to pick up nut
- [ ] Verify unscrew animation plays
- [ ] Verify nut hovers
- [ ] Click another bolt
- [ ] Verify nut moves with arc
- [ ] Verify screw down animation
- [ ] Check console for any errors

### Move Validation Test
- [ ] Try placing nut on same color → should work
- [ ] Try placing nut on different color → should show error
- [ ] Try placing nut on full bolt → should show error
- [ ] Click original bolt → nut should return

### Win Condition Test
- [ ] Sort all nuts by color
- [ ] Verify win message appears
- [ ] Check move counter

## 🎨 Polish (Optional)

### Visual Improvements
- [ ] Import better 3D models for nuts/bolts
- [ ] Create metallic materials
- [ ] Add environment (floor, background)
- [ ] Add particle effects for moves
- [ ] Add post-processing effects

### Audio
- [ ] Find/create sound effects
  - Unscrew sound (metal threading)
  - Screw sound (metal threading)
  - Hover sound (gentle metallic)
  - Valid move sound (click/success)
  - Invalid move sound (error/buzz)
  - Win sound (celebration)
- [ ] Assign to AudioManager

### UI Polish
- [ ] Design better UI layout
- [ ] Add icons
- [ ] Add animations
- [ ] Add settings menu

### Levels
- [ ] Create LevelConfiguration assets
  - Right-click → Create → Bolt Puzzle → Level Configuration
- [ ] Design different difficulty levels
- [ ] Test each level

## 🐛 Troubleshooting

### "DOTween not found"
- Install DOTween from Asset Store or Package Manager
- Reimport all scripts after installation

### "Bolts don't respond to clicks"
- Check Bolt prefab has Collider
- Verify Camera has MainCamera tag
- Check BoltSelector raycast settings

### "Nuts don't animate"
- Verify DOTween is installed
- Check Console for errors
- Ensure animations aren't killed prematurely

### "Build errors"
- Check all scripts compile
- Verify DOTween is in build
- Check target platform settings

## 📝 Next Steps

After basic setup works:
1. [ ] Design multiple levels
2. [ ] Add level progression
3. [ ] Implement save/load
4. [ ] Add undo system
5. [ ] Create tutorial
6. [ ] Add achievements
7. [ ] Optimize for target platform
8. [ ] Build and test on device

## 📚 Documentation Reference

- **README.md** - Overview and features
- **QUICKSTART.md** - Detailed setup guide
- **ARCHITECTURE.md** - Technical details
- **IMPLEMENTATION_SUMMARY.md** - What's implemented

## ✨ You're Ready!

Once all checklist items are complete:
- Save the scene
- Save the project
- Press Play and enjoy!

The game should work with simple cylinder primitives, but looks much better with proper 3D models and materials.

**Have fun creating your Bolt & Nut Puzzle! 🔩🎮**
