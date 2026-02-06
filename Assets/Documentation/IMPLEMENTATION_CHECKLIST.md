# Implementation Checklist

Use this checklist when setting up the Nut Sorting Puzzle game in Unity.

## ✅ Initial Setup

- [ ] Create new Unity 2021.3 LTS project
- [ ] Import TextMeshPro package
- [ ] Copy all scripts to project
- [ ] Create folder structure (Scenes, Prefabs, Resources, etc.)

## ✅ ScriptableObjects Creation

- [ ] Create DefaultLevelConfig (Right-click → Create → Game → Level Configuration)
  - [ ] Set Min Bolts: 3
  - [ ] Set Max Bolts: 8
  - [ ] Set Nuts Per Bolt: 4
  - [ ] Configure star thresholds
  - [ ] Add 8 colors to array
- [ ] Create DefaultAudioConfig (Right-click → Create → Game → Audio Configuration)
  - [ ] Set volume levels
  - [ ] Assign audio clips (optional)

## ✅ Prefabs Creation

### Bolt Prefab
- [ ] Create 2D Sprite Square
- [ ] Name it "Bolt"
- [ ] Set scale (0.4, 1.2, 1)
- [ ] Add Bolt component
- [ ] Add BoltInputHandler component
- [ ] Add Box Collider 2D
- [ ] Set color to Gray
- [ ] Save as prefab in Assets/Prefabs/

### Nut Prefab
- [ ] Create 2D Sprite Circle
- [ ] Name it "Nut"
- [ ] Set scale (0.25, 0.25, 1)
- [ ] Add Nut component
- [ ] Add Circle Collider 2D
- [ ] Set sorting order to 1
- [ ] Save as prefab in Assets/Prefabs/

## ✅ Menu Scene Setup

- [ ] Create new scene "Menu.unity"
- [ ] Create Canvas with proper scaling
- [ ] Create MenuManager GameObject
- [ ] Add MenuManager component
- [ ] Create UI elements:
  - [ ] Title text
  - [ ] Current Level display
  - [ ] Total Score display
  - [ ] Play button
  - [ ] Continue button
  - [ ] Settings button
  - [ ] Quit button
  - [ ] Settings panel (with toggles)
- [ ] Link all UI elements to MenuManager
- [ ] Assign AudioConfig to MenuManager
- [ ] Test scene in Play mode

## ✅ Game Scene Setup

- [ ] Create new scene "Game.unity"
- [ ] Configure Main Camera (Orthographic, size 5)
- [ ] Create GameManager GameObject
  - [ ] Add GameManager component
  - [ ] Assign LevelConfig
  - [ ] Assign AudioConfig
- [ ] Create LevelGenerator GameObject
  - [ ] Add LevelGenerator component
  - [ ] Assign Bolt prefab
  - [ ] Assign Nut prefab
  - [ ] Set spacing and position
  - [ ] Link to GameManager
- [ ] Create Game Canvas
  - [ ] Add GameUIManager component
  - [ ] Link to GameManager
- [ ] Create HUD elements:
  - [ ] Level text
  - [ ] Moves text
  - [ ] 3 Star images
  - [ ] Pause button
- [ ] Create Level Complete Panel:
  - [ ] Background panel
  - [ ] Complete message
  - [ ] Moves display
  - [ ] 3 Star images
  - [ ] Next Level button
  - [ ] Restart button
  - [ ] Menu button
  - [ ] Set inactive by default
- [ ] Create Pause Panel:
  - [ ] Background panel
  - [ ] Paused text
  - [ ] Resume button
  - [ ] Restart button
  - [ ] Menu button
  - [ ] Audio/effects toggles
  - [ ] Set inactive by default
- [ ] Link all UI elements to GameUIManager
- [ ] Add EventSystem if not present
- [ ] Test scene in Play mode

## ✅ Build Settings

- [ ] Add Menu scene (index 0)
- [ ] Add Game scene (index 1)
- [ ] Configure platform (Android/iOS)
- [ ] Set Player Settings:
  - [ ] Company name
  - [ ] Product name
  - [ ] Package name
  - [ ] Version
  - [ ] Orientation (Portrait)
  - [ ] Minimum API level (Android)

## ✅ Testing

### Menu Scene Tests
- [ ] Menu loads correctly
- [ ] Play button starts game
- [ ] Continue button works (when available)
- [ ] Settings open and close
- [ ] Toggles work and persist
- [ ] Scene transition to Game works

### Game Scene Tests
- [ ] Level generates correctly
- [ ] Bolts and nuts appear
- [ ] Click to pick up nut works
- [ ] Click to place nut works
- [ ] Invalid placement plays error sound
- [ ] Valid placement increments moves
- [ ] Move counter updates
- [ ] Star display updates based on moves
- [ ] Completing bolt plays effect
- [ ] Level completion detected
- [ ] Victory screen shows with correct stars
- [ ] Next level button works
- [ ] Restart works
- [ ] Return to menu works
- [ ] Pause menu works
- [ ] Save system works (close and reopen)

### Audio Tests
- [ ] Menu music plays
- [ ] Game music plays
- [ ] Button click sounds work
- [ ] Nut pickup sound works
- [ ] Nut place sound works
- [ ] Invalid move sound works
- [ ] Bolt complete sound works
- [ ] Level complete sound works
- [ ] Sound toggle works
- [ ] Music toggle works
- [ ] Settings persist

### Effects Tests
- [ ] Nut pickup animation works
- [ ] Nut place animation works
- [ ] Bolt complete animation works
- [ ] Star animations work
- [ ] Effects toggle works
- [ ] Settings persist

## ✅ Optional Enhancements

- [ ] Add custom graphics for bolts and nuts
- [ ] Add custom UI design
- [ ] Add particle effect prefabs
- [ ] Add audio clips
- [ ] Add more color variations
- [ ] Add hint system
- [ ] Add undo functionality
- [ ] Add different game modes
- [ ] Add timer challenge
- [ ] Integrate AdMob
- [ ] Add in-app purchases
- [ ] Add localization

## 🐛 Troubleshooting

If you encounter issues:

1. **UI not visible**: Check Canvas Scaler and camera settings
2. **Clicks not working**: Verify EventSystem exists and colliders are present
3. **Objects not spawning**: Check prefab assignments and camera view
4. **Audio not playing**: Verify AudioConfig assigned and clips present
5. **Save not working**: Check Application.persistentDataPath permissions

## 📝 Notes

- All scripts are singleton-based for easy access
- Save system uses JSON for cross-platform compatibility
- Audio and effects can be toggled independently
- Level difficulty increases automatically
- Star thresholds are configurable in LevelConfig

## ✨ Final Steps

- [ ] Build and test on target device
- [ ] Verify save system on device
- [ ] Test audio on device
- [ ] Check performance
- [ ] Optimize if needed
- [ ] Prepare for release

---

**Remember**: Start with Menu scene in Build Settings for proper flow!
