# Prefab Configuration Guide

## Bolt Prefab

### 3D Model Setup
1. Create a new 3D object (Cylinder or import custom bolt model)
2. Name it "BoltPrefab"
3. Set scale: (0.2, 1.0, 0.2) for cylinder
4. Position: (0, 0, 0)

### Components Required
- **Transform**: Default position
- **Mesh Filter**: Cylinder or custom mesh
- **Mesh Renderer**: Material with metallic gray color
- **Collider**: Box Collider or Mesh Collider
  - Is Trigger: false
  - Adjust bounds to match bolt size
- **Bolt Script** (Bolt.cs)
  - Max Capacity: 4
  - Nut Spacing: 0.3
  - Unscrew Height: 1.0

### Visual Setup
Create a material "BoltMaterial":
- Shader: Standard
- Albedo: Gray (RGB: 0.5, 0.5, 0.5)
- Metallic: 0.7
- Smoothness: 0.6

## Nut Prefab

### 3D Model Setup
1. Create a new 3D object (Torus or import custom nut model)
2. Name it "NutPrefab"
3. Set scale: (0.3, 0.15, 0.3) for torus
4. Position: (0, 0, 0)

### Components Required
- **Transform**: Default position
- **Mesh Filter**: Torus or custom mesh
- **Mesh Renderer**: Material with default white (will be colored by script)
- **Nut Script** (Nut.cs)
  - Color: Red (will be set by initialization)
  - Move Speed: 2.0
  - Rotation Speed: 360
  - Shake Amount: 0.1

### Visual Setup
Create a material "NutMaterial":
- Shader: Standard
- Albedo: White (will be changed programmatically)
- Metallic: 0.3
- Smoothness: 0.5

## Particle Effect Prefabs

### Nut Contact Effect
1. Create new Particle System
2. Name: "NutContactEffect"
3. Main Module:
   - Duration: 0.5
   - Looping: false
   - Start Lifetime: 0.3
   - Start Speed: 2
   - Start Size: 0.1
   - Start Color: Yellow/White
4. Emission:
   - Rate over Time: 0
   - Bursts: 1 burst with 10 particles
5. Shape:
   - Shape: Sphere
   - Radius: 0.2

### Victory Effect
1. Create new Particle System
2. Name: "VictoryEffect"
3. Main Module:
   - Duration: 2.0
   - Looping: false
   - Start Lifetime: 1.5
   - Start Speed: 3
   - Start Size: 0.2
   - Start Color: Rainbow gradient
4. Emission:
   - Rate over Time: 20
5. Shape:
   - Shape: Cone
   - Angle: 45
   - Radius: 1.0

### Sparkle Effect
1. Create new Particle System
2. Name: "SparkleEffect"
3. Main Module:
   - Duration: 1.0
   - Looping: true
   - Start Lifetime: 0.5
   - Start Speed: 0.5
   - Start Size: 0.05
   - Start Color: White with transparency
4. Emission:
   - Rate over Time: 10

## Audio Clips

### Required Sound Effects

**Screwing Sound** (screwing.wav)
- Duration: 0.3-0.5 seconds
- Type: Mechanical screwing/threading sound
- Format: WAV or MP3
- Suggested: Ratchet or drill sound, pitched down

**Unscrewing Sound** (unscrewing.wav)
- Duration: 0.3-0.5 seconds
- Type: Mechanical unscrewing sound
- Format: WAV or MP3
- Suggested: Reverse of screwing sound

**Error Sound** (error.wav)
- Duration: 0.2-0.3 seconds
- Type: Negative feedback buzz
- Format: WAV or MP3
- Suggested: Buzzer or "wrong" beep

**Victory Sound** (victory.wav)
- Duration: 1-2 seconds
- Type: Celebratory fanfare
- Format: WAV or MP3
- Suggested: Ascending chimes or short victory jingle

**Click Sound** (click.wav)
- Duration: 0.1 seconds
- Type: Button click
- Format: WAV or MP3
- Suggested: Soft click or tap sound

**Background Music** (bgm.wav) - Optional
- Duration: 60-120 seconds (looping)
- Type: Calm, puzzle-appropriate music
- Format: WAV or MP3
- Volume: Lower than SFX

## UI Sprites

### Button Sprites
- Normal state: Clean button with slight shadow
- Highlighted state: Brighter version
- Pressed state: Darker/indented look
- Disabled state: Grayed out

Suggested sizes:
- Width: 200 pixels
- Height: 80 pixels
- Format: PNG with transparency

### Panel Sprites
- Victory panel background: Semi-transparent dark overlay
- Size: 800x600 pixels
- Format: PNG with alpha channel

## Material Colors

### Nut Colors (Programmatically Set)
The following colors are set by the GameConstants class:
- Red: RGB(255, 0, 0)
- Blue: RGB(0, 0, 255)
- Green: RGB(0, 255, 0)
- Yellow: RGB(255, 255, 0)
- Purple: RGB(127, 0, 127)
- Orange: RGB(255, 127, 0)
- Pink: RGB(255, 191, 204)

### UI Colors
- Background: RGB(20, 20, 20) - Dark gray
- Button normal: RGB(70, 70, 70)
- Button hover: RGB(100, 100, 100)
- Text: RGB(255, 255, 255) - White
- Victory text: RGB(255, 215, 0) - Gold

## Prefab Creation Checklist

### Before Creating Prefabs
- [ ] Create all required folders (Prefabs, Materials, Audio, Sprites)
- [ ] Import or create 3D models
- [ ] Create materials with appropriate shaders
- [ ] Import audio files
- [ ] Create or import UI sprites

### Bolt Prefab Checklist
- [ ] 3D model configured and scaled
- [ ] Bolt material applied
- [ ] Collider component added and configured
- [ ] Bolt.cs script attached
- [ ] Script parameters set correctly
- [ ] Saved as prefab in Assets/Prefabs/

### Nut Prefab Checklist
- [ ] 3D model configured and scaled
- [ ] Nut material applied (white base)
- [ ] Nut.cs script attached
- [ ] Script parameters set correctly
- [ ] Saved as prefab in Assets/Prefabs/

### Effect Prefabs Checklist
- [ ] Particle systems created
- [ ] Main module configured
- [ ] Emission settings configured
- [ ] Shape settings configured
- [ ] Colors and gradients set
- [ ] Saved as prefabs in Assets/Prefabs/Effects/

### Audio Clips Checklist
- [ ] All required audio files imported
- [ ] Import settings checked (Force to Mono, Load in Background)
- [ ] Compression format appropriate
- [ ] Audio files organized in Assets/Audio/

## Testing Prefabs

### Bolt Prefab Test
1. Drag prefab into scene
2. Verify visual appearance
3. Check collider bounds
4. Test script in Inspector
5. Remove from scene if working

### Nut Prefab Test
1. Drag prefab into scene
2. Verify visual appearance
3. Call Initialize() with a test color
4. Verify color changes correctly
5. Remove from scene if working

### Effect Prefabs Test
1. Drag effect prefab into scene
2. Play particle system
3. Verify timing and appearance
4. Check auto-destruction works
5. Remove from scene if working

## Assembly Instructions

Once all prefabs are created:
1. Open GameScene
2. Find GameManager GameObject
3. Assign BoltPrefab to Bolt Prefab field
4. Assign NutPrefab to Nut Prefab field
5. Find EffectsManager GameObject
6. Assign effect prefabs to respective fields
7. Find AudioManager GameObject
8. Assign audio clips to respective fields
9. Save scene
10. Test in Play mode
