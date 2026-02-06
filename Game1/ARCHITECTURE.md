# Technical Architecture - Bolt & Nut Puzzle

## System Overview

The game follows a component-based architecture typical of Unity games, with clear separation between game logic, input handling, and visual presentation.

## Core Components

### 1. Nut (Nut.cs)
**Responsibility**: Visual representation and animation of individual nuts

**Key Properties**:
- `Color`: NutColor enum value
- `ParentBolt`: Reference to current bolt
- `meshRenderer`: Visual rendering
- `currentAnimation`: Active DOTween sequence

**Key Methods**:
```csharp
Initialize(NutColor color)           // Set color and update visuals
AnimateUnscrew(Action onComplete)    // Lift animation
StartHoverAnimation()                // Continuous bobbing
AnimateMoveToBolt(Bolt, Vector3)    // Arc movement
AnimateScrewDown(Vector3)            // Descent animation
AnimateReturnToBolt()                // Return to origin
```

**Animation Sequences**:
1. Unscrew: Rotate 360° + Move up 2 units → Hover loop
2. Move: Arc path (Catmull-Rom) + Rotation → Screw down
3. Screw: Rotate -360° + Move down to position
4. Hover: Infinite bobbing + slow rotation

### 2. Bolt (Bolt.cs)
**Responsibility**: Container logic and nut stack management

**Key Properties**:
- `maxCapacity`: Maximum nuts (default: 4)
- `nutSpacing`: Vertical spacing between nuts
- `nuts`: List of current nuts
- `nutsContainer`: Transform parent for nuts

**Key Methods**:
```csharp
GetTopNut()                    // Returns top nut or null
CanPlaceNut(Nut)               // Validates placement
RemoveTopNut()                 // Removes and returns top
AddNut(Nut)                    // Adds nut to stack
GetNextNutPosition()           // Calculates position
IsSorted()                     // Checks single color
IsComplete()                   // Checks full & sorted
HighlightAsValid/Invalid()     // Visual feedback
```

**Validation Logic**:
```
Can place nut if:
  - Bolt is not full AND
  - (Bolt is empty OR top nut is same color)
```

**State Queries**:
- `IsFull`: nuts.Count >= maxCapacity
- `IsEmpty`: nuts.Count == 0
- `IsSorted`: All nuts same color or empty
- `IsComplete`: IsFull && IsSorted

### 3. GameManager (GameManager.cs)
**Responsibility**: Level generation, game state, win condition

**Key Properties**:
- `nutPrefab`, `boltPrefab`: Templates
- `numberOfBolts`, `nutsPerBolt`: Level config
- `numberOfColors`, `emptyBolts`: Puzzle config
- `bolts`: All bolts in scene
- `moveCount`: Player moves

**Key Methods**:
```csharp
GenerateLevel()               // Creates new level
GenerateNutDistribution()     // Creates color array
CreateBolt(Vector3)           // Instantiates bolt
CreateNut(NutColor, Vector3)  // Instantiates nut
CheckWinCondition()           // Validates all bolts
RecordMove()                  // Counts moves
```

**Level Generation Algorithm**:
```
1. Create N bolts in a row
2. Generate nuts: nutsPerBolt of each color
3. Shuffle the nut array
4. Distribute to first (N - emptyBolts) bolts
5. Leave remaining bolts empty
6. Initialize BoltSelector with bolt list
```

### 4. BoltSelector (BoltSelector.cs)
**Responsibility**: Input handling and move orchestration

**State Machine**:
```
Idle → [Click bolt with nut] → Moving → NutSelected
NutSelected → [Click valid bolt] → Moving → Idle
NutSelected → [Click same bolt] → Moving → Idle
NutSelected → [Click invalid] → (stay in NutSelected)
```

**Key Properties**:
- `selectedBolt`: Source bolt
- `selectedNut`: Currently held nut
- `currentState`: SelectionState enum
- `bolts`: All game bolts

**Key Methods**:
```csharp
Initialize(List<Bolt>)        // Setup with bolts
HandleMouseClick()            // Route input
TrySelectNut(Bolt)           // Pick up nut
TryPlaceNut(Bolt)            // Place nut
ReturnNutToOriginalBolt()    // Cancel move
HighlightValidMoves()        // Show valid targets
GetBoltUnderMouse()          // Raycast detection
```

**Click Handling Flow**:
```
Click → Raycast → Get Bolt → 
  If Idle: Try select nut
  If NutSelected: Try place or return
  If Moving: Ignore
```

## Data Flow

### Move Execution Flow
```
1. User clicks bolt
   └→ BoltSelector.HandleMouseClick()

2. Raycast finds Bolt
   └→ BoltSelector.GetBoltUnderMouse()

3. State: Idle → Select nut
   └→ BoltSelector.TrySelectNut()
      └→ Bolt.RemoveTopNut()
      └→ Nut.AnimateUnscrew()
      └→ State: Moving

4. Animation completes
   └→ State: NutSelected
   └→ BoltSelector.HighlightValidMoves()

5. User clicks target bolt
   └→ BoltSelector.TryPlaceNut()

6. If valid:
   └→ Nut.AnimateMoveToBolt()
   └→ Bolt.AddNut()
   └→ GameManager.RecordMove()
   └→ GameManager.CheckWinCondition()
   └→ State: Idle

7. If invalid:
   └→ Bolt.HighlightAsInvalid()
   └→ Stay in NutSelected
```

### Win Condition Check
```
On each move:
1. GameManager.RecordMove()
2. For each bolt:
   - Check bolt.IsSorted()
3. If all sorted:
   - OnLevelComplete()
```

## Design Patterns

### 1. Component Pattern
Each GameObject has focused components:
- Nut: Visual + Animation
- Bolt: Logic + Container
- GameManager: Level + State
- BoltSelector: Input + Orchestration

### 2. State Machine Pattern
BoltSelector uses explicit state machine:
- Prevents invalid transitions
- Blocks input during animations
- Clear state transitions

### 3. Observer Pattern (Implicit)
- Nut animations use callbacks
- GameManager checks state on moves
- Visual feedback responds to state

### 4. Factory Pattern
GameManager creates bolts and nuts:
- Consistent instantiation
- Prefab or fallback creation
- Centralized configuration

## Animation Architecture

### DOTween Integration
All animations use DOTween sequences:
```csharp
Sequence seq = DOTween.Sequence();
seq.Append(transform.DOMove(...));
seq.Join(transform.DORotate(...));
seq.OnComplete(() => {...});
```

### Animation Lifecycle
1. **Kill**: Stop previous animations
2. **Create**: New sequence
3. **Build**: Chain transformations
4. **Execute**: Play sequence
5. **Callback**: On completion

### Animation Types Used
- `DOMove`: Position changes
- `DOMoveY`: Vertical movement
- `DORotate`: Rotation (screwing)
- `DOPath`: Arc movement
- `DOScale`: Pulsing effects
- `DOShakePosition`: Error feedback

## Performance Considerations

### Optimization Strategies
1. **Object Reuse**: Nuts/bolts can be pooled
2. **Animation Killing**: Prevents memory leaks
3. **Raycast Efficiency**: Single raycast per click
4. **Update Minimization**: Only on input

### Potential Bottlenecks
1. Too many simultaneous animations
2. Complex 3D models for nuts/bolts
3. Excessive raycasting
4. Material instantiation per nut

### Recommended Limits
- Bolts: 10-15 maximum
- Nuts per bolt: 4-6
- Colors: 4-8
- Animations: Optimize sequences

## Extension Points

### Adding New Features

**1. Undo System**
```csharp
class Move {
    Bolt sourceBolt;
    Bolt targetBolt;
    Nut nut;
}
Stack<Move> moveHistory;
```

**2. Hint System**
```csharp
// Find valid move that progresses solution
Move FindHintMove(List<Bolt> bolts) {
    // Search for moves that create same-color groups
}
```

**3. Sound Effects**
```csharp
// In Nut.cs animations:
AudioSource.PlayOneShot(unscrewSound);
AudioSource.PlayOneShot(screwSound);
```

**4. Particle Effects**
```csharp
// On successful placement:
ParticleSystem.Play() at nut position
```

**5. Level Progression**
```csharp
class LevelConfig {
    int bolts, nuts, colors, empty;
}
LevelConfig[] levels;
int currentLevel;
```

## Testing Considerations

### Unit Test Targets
- `Bolt.CanPlaceNut()`: Placement validation
- `Bolt.IsSorted()`: Sort detection
- `GameManager.GenerateNutDistribution()`: Array generation
- Win condition logic

### Integration Tests
- Full move sequence
- Animation completion
- State transitions
- Input handling

### Manual Test Cases
1. Basic move (empty bolt)
2. Color matching
3. Invalid move rejection
4. Win condition
5. Edge cases (full bolts, empty bolts)

## Dependencies

### Unity Modules
- `UnityEngine`: Core
- `UnityEngine.UI`: Optional for UI

### External Packages
- **DOTween Pro** (DG.Tweening): Animation system
  - Version: 1.2.765+
  - License: Asset Store or Free version

### C# Features Used
- LINQ (for color enumeration)
- Generics (List<T>)
- Delegates/Actions (callbacks)
- Enums (NutColor, SelectionState)

## File Structure

```
Game1/
├── Assets/
│   ├── Scripts/
│   │   ├── Nut.cs              (220 lines)
│   │   ├── Bolt.cs             (185 lines)
│   │   ├── GameManager.cs      (220 lines)
│   │   └── BoltSelector.cs     (215 lines)
│   ├── Prefabs/
│   │   ├── Nut.prefab          (to be created)
│   │   └── Bolt.prefab         (to be created)
│   ├── Scenes/
│   │   └── MainScene.unity     (to be created)
│   └── Materials/              (to be created)
├── Packages/
│   └── manifest.json
└── ProjectSettings/
    └── (Unity generated)
```

## Code Metrics

- **Total Lines**: ~840 (excluding Unity config)
- **Classes**: 4 main classes + 2 enums
- **Public Methods**: ~35
- **Animation Sequences**: 7 types
- **State Transitions**: 5 states

## Future Architecture Improvements

1. **Dependency Injection**: Remove Singleton pattern from GameManager
2. **Event System**: Replace direct method calls with events
3. **MVC Pattern**: Separate view from logic more clearly
4. **Object Pooling**: Reuse nut/bolt instances
5. **Scriptable Objects**: Level configurations as assets
6. **Command Pattern**: For undo/redo system

---

Last Updated: February 2026
Version: 1.0
