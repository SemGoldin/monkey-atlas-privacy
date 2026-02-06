# Quick Start Checklist / Швидкий старт

## English Version - Quick Setup Checklist

Follow this checklist to get your game running quickly:

### Phase 1: Unity Project Setup (5 minutes)
- [ ] Install Unity 2020.3 LTS or newer
- [ ] Create new Unity 3D project
- [ ] Import TextMeshPro (Window → TextMeshPro → Import TMP Essential Resources)

### Phase 2: Copy Scripts (2 minutes)
- [ ] Create folder: `Assets/Scripts/`
- [ ] Copy entire `UnityScripts/` folder contents to `Assets/Scripts/`
- [ ] Wait for Unity to compile (check bottom-right of Unity Editor)

### Phase 3: Create Game Objects (30 minutes)

#### Managers (5 minutes)
- [ ] Create Empty GameObject: "GameManager"
  - [ ] Add `GameManager.cs` script
- [ ] Create Empty GameObject: "ScoreManager"
  - [ ] Add `ScoreManager.cs` script

#### Player (10 minutes)
- [ ] Create Sphere: "Player"
  - [ ] Position: (0, 0.5, 0), Scale: (0.5, 0.5, 0.5)
  - [ ] Add Tag: "Player"
  - [ ] Add `Rigidbody` component (freeze all rotations)
  - [ ] Add `Sphere Collider`
  - [ ] Add `PlayerController.cs` script
  - [ ] Create child Sphere: "Model"
    - [ ] Remove its collider
    - [ ] Add `PlayerAnimation.cs` script

#### Ghosts Container (5 minutes)
- [ ] Create Empty GameObject: "Ghosts"
  - [ ] Create 4 child Spheres:
    - [ ] "Ghost_Blinky" - Position: (0, 0.5, 5)
    - [ ] "Ghost_Pinky" - Position: (-5, 0.5, 0)
    - [ ] "Ghost_Inky" - Position: (5, 0.5, 0)
    - [ ] "Ghost_Clyde" - Position: (0, 0.5, -5)
  - [ ] For each ghost:
    - [ ] Scale: (0.6, 0.6, 0.6)
    - [ ] Add Tag: "Ghost"
    - [ ] Add `Rigidbody` (freeze rotations)
    - [ ] Add `Sphere Collider` (Is Trigger: ✓)
    - [ ] Add `GhostAI.cs` script
    - [ ] Set Ghost Type (Blinky/Pinky/Inky/Clyde)

#### Maze (5 minutes)
- [ ] Create Empty GameObject: "Maze"
  - [ ] Add `MazeGenerator.cs` script
- [ ] Create Empty GameObject: "Pellets"

#### Camera (2 minutes)
- [ ] Select Main Camera
  - [ ] Position: (0, 20, -10)
  - [ ] Rotation: (60, 0, 0)

#### Lighting (3 minutes)
- [ ] Select Directional Light
  - [ ] Rotation: (50, -30, 0)
  - [ ] Intensity: 1

### Phase 4: Create Prefabs (45 minutes)

#### Wall Prefab (10 minutes)
- [ ] Create Cube
  - [ ] Add dark blue material
  - [ ] Add Tag: "Wall"
  - [ ] Save as prefab: `Assets/Prefabs/Wall.prefab`
  - [ ] Delete from scene

#### Floor Prefab (10 minutes)
- [ ] Create Plane
  - [ ] Scale: (0.1, 1, 0.1)
  - [ ] Add black material
  - [ ] Save as prefab: `Assets/Prefabs/Floor.prefab`
  - [ ] Delete from scene

#### Pellet Prefab (15 minutes)
- [ ] Create Sphere
  - [ ] Scale: (0.1, 0.1, 0.1)
  - [ ] Add white emissive material
  - [ ] Add Tag: "Pellet"
  - [ ] Add `Sphere Collider` (Is Trigger: ✓)
  - [ ] Add `Pellet.cs` script
  - [ ] Optional: Add Point Light
  - [ ] Save as prefab: `Assets/Prefabs/Pellet.prefab`
  - [ ] Delete from scene

#### Power Pellet Prefab (10 minutes)
- [ ] Duplicate Pellet prefab
  - [ ] Scale: (0.2, 0.2, 0.2)
  - [ ] Change material to yellow emissive
  - [ ] Change Tag: "PowerPellet"
  - [ ] Replace script with `PowerPellet.cs`
  - [ ] Save as prefab: `Assets/Prefabs/PowerPellet.prefab`

### Phase 5: Configure References (15 minutes)

#### GameManager
- [ ] Drag Player to "Player" field
- [ ] Drag Ghosts to "Ghost Container" field
- [ ] Drag Canvas to "UI Manager" field (will do next)
- [ ] Drag Maze to "Maze Generator" field

#### MazeGenerator
- [ ] Drag Maze to "Maze Container" field
- [ ] Drag Pellets to "Pellet Container" field
- [ ] Drag Wall prefab to "Wall Prefab" field
- [ ] Drag Floor prefab to "Floor Prefab" field
- [ ] Drag Pellet prefab to "Pellet Prefab" field
- [ ] Drag PowerPellet prefab to "Power Pellet Prefab" field

### Phase 6: Create UI (60 minutes)

#### Canvas Setup (5 minutes)
- [ ] Create Canvas (GameObject → UI → Canvas)
- [ ] Canvas Scaler: Scale With Screen Size, 1920x1080

#### HUD (15 minutes)
- [ ] Create TextMeshPro: "ScoreText"
  - [ ] Anchor: Top Left, Position: (100, -50)
  - [ ] Text: "Score: 0"
- [ ] Create TextMeshPro: "HighScoreText"
  - [ ] Position: (100, -100)
- [ ] Create TextMeshPro: "LevelText"
  - [ ] Anchor: Top Center
- [ ] Create Panel: "LivesPanel"
  - [ ] Anchor: Top Right
  - [ ] Add 3 child Images for life icons

#### Main Menu (10 minutes)
- [ ] Create Panel: "MainMenuPanel"
  - [ ] Add title TextMeshPro: "MAZE CHASE 3D"
  - [ ] Add Button: "Play"
  - [ ] Add Button: "Quit"

#### Other Menus (30 minutes)
- [ ] Create Panel: "PauseMenuPanel"
  - [ ] Title: "PAUSED"
  - [ ] Buttons: Resume, Restart, Quit
- [ ] Create Panel: "GameOverPanel"
  - [ ] Title: "GAME OVER"
  - [ ] Buttons: Try Again, Main Menu
- [ ] Create Panel: "LevelCompletePanel"
  - [ ] Title: "LEVEL COMPLETE"
  - [ ] Button: Continue

#### UIManager Setup (5 minutes)
- [ ] Add `UIManager.cs` to Canvas
- [ ] Drag all UI elements to their respective fields
- [ ] Link all buttons

### Phase 7: Create Materials (30 minutes)

#### Create these materials in Assets/Materials/:
- [ ] PlayerMaterial (Yellow, emissive)
- [ ] PlayerPoweredUpMaterial (Cyan, emissive)
- [ ] BlinkyMaterial (Red)
- [ ] PinkyMaterial (Pink)
- [ ] InkyMaterial (Cyan)
- [ ] ClydeMaterial (Orange)
- [ ] GhostFrightenedMaterial (Blue)
- [ ] GhostEatenMaterial (Transparent)

#### Assign Materials:
- [ ] Player Model → PlayerMaterial
- [ ] Each Ghost → Its color material
- [ ] Each GhostAI script → Normal, Frightened, Eaten materials

### Phase 8: Create Tags (5 minutes)
- [ ] Edit → Project Settings → Tags and Layers
- [ ] Create tags: Player, Ghost, Wall, Pellet, PowerPellet
- [ ] Assign tags to respective objects

### Phase 9: Test (15 minutes)
- [ ] Save Scene as "MainScene"
- [ ] Press Play
- [ ] Test player movement (WASD/Arrows)
- [ ] Test ghost behavior
- [ ] Test pellet collection
- [ ] Test power pellets
- [ ] Test UI menus
- [ ] Check for errors in Console

### Phase 10: Polish (Optional)
- [ ] Add audio clips to player and ghosts
- [ ] Add particle effects
- [ ] Adjust speeds and timings
- [ ] Customize colors and materials
- [ ] Add more visual effects

## Total Time Estimate
- **Minimum Setup**: 2-3 hours (basic functionality)
- **Complete Setup**: 4-6 hours (with polish)

---

## Українська версія - Чеклист швидкого налаштування

### Фаза 1: Налаштування проєкту Unity (5 хвилин)
- [ ] Встановити Unity 2020.3 LTS або новішу
- [ ] Створити новий Unity 3D проєкт
- [ ] Імпортувати TextMeshPro

### Фаза 2: Копіювання скриптів (2 хвилини)
- [ ] Створити папку: `Assets/Scripts/`
- [ ] Скопіювати вміст папки `UnityScripts/` до `Assets/Scripts/`
- [ ] Дочекатися компіляції Unity

### Фаза 3: Створення ігрових об'єктів (30 хвилин)

#### Менеджери
- [ ] Створити GameManager та додати скрипт
- [ ] Створити ScoreManager та додати скрипт

#### Гравець
- [ ] Створити Player (Sphere)
- [ ] Налаштувати компоненти
- [ ] Створити дочірній Model

#### Привиди
- [ ] Створити контейнер Ghosts
- [ ] Створити 4 привиди з різними позиціями
- [ ] Налаштувати кожного привида

#### Лабіринт та камера
- [ ] Створити Maze з MazeGenerator
- [ ] Створити Pellets контейнер
- [ ] Налаштувати камеру

### Фаза 4: Створення префабів (45 хвилин)
- [ ] Wall Prefab (стіна)
- [ ] Floor Prefab (підлога)
- [ ] Pellet Prefab (точка)
- [ ] PowerPellet Prefab (силова точка)

### Фаза 5: Налаштування посилань (15 хвилин)
- [ ] Налаштувати GameManager
- [ ] Налаштувати MazeGenerator

### Фаза 6: Створення UI (60 хвилин)
- [ ] Canvas та HUD елементи
- [ ] Головне меню
- [ ] Меню паузи
- [ ] Екрани завершення
- [ ] UIManager налаштування

### Фаза 7: Створення матеріалів (30 хвилин)
- [ ] Створити всі необхідні матеріали
- [ ] Призначити матеріали об'єктам

### Фаза 8: Створення тегів (5 хвилин)
- [ ] Створити всі необхідні теги

### Фаза 9: Тестування (15 хвилин)
- [ ] Зберегти сцену
- [ ] Протестувати всю функціональність

### Фаза 10: Поліровка (Опціонально)
- [ ] Додати аудіо
- [ ] Додати ефекти
- [ ] Налаштувати параметри

## Загальний час
- **Мінімальне налаштування**: 2-3 години
- **Повне налаштування**: 4-6 годин

---

## Tips / Поради

### Save Often / Зберігайте часто
- Save scene: Ctrl+S / Cmd+S
- Save project: Ctrl+Shift+S / Cmd+Shift+S

### Check Console / Перевіряйте консоль
- Open console: Ctrl+Shift+C / Cmd+Shift+C
- Fix all errors before testing

### Use Documentation / Використовуйте документацію
- Refer to README_SETUP.md for detailed instructions
- All scripts have XML documentation comments

### Test Incrementally / Тестуйте поетапно
- Test after each major phase
- Don't wait until everything is done

### Backup Your Work / Робіть резервні копії
- Use version control (Git)
- Save scene versions

---

**Good Luck! / Удачі!** 🎮
