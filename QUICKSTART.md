# Quick Start Guide - Bolt and Nut Puzzle Game

## 5-Minute Setup

### Prerequisites
- Unity 2020.3 or later
- Basic Unity knowledge

### Step 1: Import Scripts (1 minute)
1. Copy all files from `Assets/Scripts/` to your Unity project's `Assets/Scripts/` folder
2. Wait for Unity to compile

### Step 2: Create Basic Prefabs (2 minutes)

**Bolt Prefab:**
```
1. GameObject → 3D Object → Cylinder
2. Name: "BoltPrefab"
3. Scale: (0.2, 1, 0.2)
4. Add Component → Bolt script
5. Set Max Capacity: 4
6. Drag to Assets/Prefabs folder
7. Delete from scene
```

**Nut Prefab:**
```
1. GameObject → 3D Object → Torus
2. Name: "NutPrefab"
3. Scale: (0.3, 0.15, 0.3)
4. Add Component → Nut script
5. Set Move Speed: 2
6. Drag to Assets/Prefabs folder
7. Delete from scene
```

### Step 3: Setup Scene (2 minutes)

**Create GameManager:**
```
1. Create Empty GameObject: "GameManager"
2. Add GameManager script
3. Add AudioManager script
4. In GameManager Inspector:
   - Assign BoltPrefab
   - Assign NutPrefab
```

**Create InputManager:**
```
1. Create Empty GameObject: "InputManager"
2. Add InputManager script
3. Assign Main Camera
```

**Setup Camera:**
```
1. Select Main Camera
2. Position: (0, 8, -5)
3. Rotation: (45, 0, 0)
```

### Step 4: Run! (0 minutes)
Press Play! You should see 9 bolts with colored nuts that you can click and move.

## What You Should See

✓ 9 bolts in 3x3 grid
✓ Colored nuts on 7 bolts
✓ 2 empty bolts
✓ Click bolts to lift nuts
✓ Click other bolts to transfer
✓ Invalid moves shake the nut

## Next Steps

### Add Audio (Optional)
1. Import audio files to Assets/Audio/
2. Assign to AudioManager in Inspector

### Add UI (Optional)
1. Create Canvas
2. Add victory panel, buttons, text
3. Attach UIManager script
4. Assign UI elements

### Improve Visuals (Optional)
1. Import better 3D models for bolts/nuts
2. Add materials with textures
3. Add particle effects
4. Improve lighting

## Troubleshooting

**Nothing appears:**
- Check Console for errors
- Verify prefabs are assigned in GameManager
- Check camera position and rotation

**Can't click bolts:**
- Make sure bolts have colliders
- Verify InputManager has camera reference
- Check you're clicking on the bolt cylinders

**No movement:**
- Check Console for errors
- Verify Nut and Bolt scripts are attached
- Make sure GameManager is in scene

## Full Documentation

For complete setup and features, see:
- `IMPLEMENTATION.md` - Complete technical docs
- `UNITY_SETUP.md` - Detailed Unity setup
- `GAME_DESIGN_UA.md` - Ukrainian design docs
- `README.md` - Project overview
- `Assets/Prefabs/PREFAB_GUIDE.md` - Prefab creation

## Contact

For issues, check the documentation or console errors first.

---

**You're ready to play!** 🎮🔩
