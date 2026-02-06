# 3D Maze Chase Game - Unity Setup Guide / Посібник з налаштування 3D гри-лабіринту в Unity

## English Version

### Prerequisites
- Unity 2020.3 LTS or newer
- Basic knowledge of Unity Editor
- TextMeshPro package (usually included by default)

---

## Project Setup

### 1. Create New Unity Project
1. Open Unity Hub
2. Click "New Project"
3. Select "3D" template
4. Name your project (e.g., "MazeChaseGame3D")
5. Click "Create"

### 2. Import Scripts
1. Copy all C# scripts from the `UnityScripts` folder to your Unity project's `Assets/Scripts` folder
2. Create the following folder structure in Unity:
   ```
   Assets/
   ├── Scripts/
   │   ├── Core/
   │   │   ├── GameManager.cs
   │   │   └── GameState.cs
   │   ├── Player/
   │   │   ├── PlayerController.cs
   │   │   └── PlayerAnimation.cs
   │   ├── AI/
   │   │   └── GhostAI.cs
   │   ├── Maze/
   │   │   ├── MazeGenerator.cs
   │   │   ├── Pellet.cs
   │   │   └── PowerPellet.cs
   │   └── UI/
   │       ├── UIManager.cs
   │       └── ScoreManager.cs
   ```

---

## Scene Hierarchy Setup

### 1. Main Scene Structure
Create the following GameObject hierarchy in your scene:

```
Scene: MainScene
├── GameManager (Empty GameObject)
├── ScoreManager (Empty GameObject)
├── Player (Sphere)
│   └── Model (Sphere - child)
├── Ghosts (Empty GameObject - Container)
│   ├── Ghost_Blinky (Sphere with different material)
│   ├── Ghost_Pinky (Sphere with different material)
│   ├── Ghost_Inky (Sphere with different material)
│   └── Ghost_Clyde (Sphere with different material)
├── Maze (Empty GameObject - Container)
├── Pellets (Empty GameObject - Container)
├── Lighting
│   ├── Directional Light
│   └── Environment Lighting
├── Main Camera
└── Canvas (UI)
    ├── HUD
    │   ├── ScoreText
    │   ├── HighScoreText
    │   ├── LevelText
    │   └── LivesPanel
    ├── MainMenuPanel
    ├── PauseMenuPanel
    ├── GameOverPanel
    └── LevelCompletePanel
```

---

## Detailed Component Setup

### 2. GameManager Setup

**GameObject:** GameManager (Empty GameObject)

**Components:**
1. Add `GameManager` script
2. Configure in Inspector:
   - Starting Lives: 3
   - Level Start Delay: 2
   - Game Over Delay: 3
   - Player: Drag Player GameObject here
   - Ghost Container: Drag Ghosts GameObject here
   - UI Manager: Drag Canvas GameObject here (after adding UIManager)
   - Maze Generator: Drag Maze GameObject here (after adding MazeGenerator)

---

### 3. ScoreManager Setup

**GameObject:** ScoreManager (Empty GameObject)

**Components:**
1. Add `ScoreManager` script
2. Configure in Inspector:
   - Pellet Score: 10
   - Power Pellet Score: 50
   - Ghost Base Score: 200
   - Ghost Score Multiplier: 2

---

### 4. Player Setup

**GameObject:** Player

**Steps:**
1. Create a Sphere GameObject (GameObject → 3D Object → Sphere)
2. Name it "Player"
3. Set Position: (0, 0.5, 0)
4. Set Scale: (0.5, 0.5, 0.5)
5. Add Tag: Create new tag "Player" and assign it

**Components:**
1. **Rigidbody:**
   - Use Gravity: ✓
   - Is Kinematic: ✗
   - Constraints: Freeze Rotation X, Y, Z

2. **Capsule Collider** (or Sphere Collider):
   - Is Trigger: ✗
   - Radius: 0.5

3. **PlayerController Script:**
   - Move Speed: 5
   - Rotation Speed: 720
   - Acceleration: 10

4. **AudioSource:**
   - Play On Awake: ✗
   - Loop: ✓

**Player Model (Child Object):**
1. Create child Sphere: Right-click Player → 3D Object → Sphere
2. Name it "Model"
3. Remove Collider component
4. This will be the visual representation

5. **PlayerAnimation Script** (on Model):
   - Model: Drag Model GameObject here
   - Bob Speed: 2
   - Bob Amount: 0.1

---

### 5. Ghost Setup

**For Each Ghost (Blinky, Pinky, Inky, Clyde):**

1. Create a Sphere GameObject
2. Name accordingly (e.g., "Ghost_Blinky")
3. Set Position: Different starting positions
   - Blinky: (0, 0.5, 5)
   - Pinky: (-5, 0.5, 0)
   - Inky: (5, 0.5, 0)
   - Clyde: (0, 0.5, -5)
4. Set Scale: (0.6, 0.6, 0.6)
5. Add Tag: Create new tag "Ghost" and assign it

**Components:**
1. **Rigidbody:**
   - Use Gravity: ✓
   - Is Kinematic: ✗
   - Constraints: Freeze Rotation X, Y, Z

2. **Sphere Collider:**
   - Is Trigger: ✓
   - Radius: 0.5

3. **GhostAI Script:**
   - Normal Speed: 4
   - Frightened Speed: 2
   - Frightened Duration: 10
   - Scatter Duration: 7
   - Chase Duration: 20
   - Ghost Type: Select appropriate type (Blinky/Pinky/Inky/Clyde)
   - Ghost Renderer: Drag the Sphere's Mesh Renderer here

4. **AudioSource:**
   - Play On Awake: ✗
   - Loop: ✗

---

### 6. Maze Setup

**GameObject:** Maze (Empty GameObject - Container)

**Components:**
1. Add `MazeGenerator` script
2. Configure in Inspector:
   - Width: 28
   - Height: 31
   - Cell Size: 1
   - Maze Container: Drag Maze GameObject here (self-reference)
   - Pellet Container: Drag Pellets GameObject here

**Create Prefabs:**

**Wall Prefab:**
1. Create Cube (GameObject → 3D Object → Cube)
2. Scale: (1, 1, 1)
3. Create Material: Assets → Create → Material
   - Name: "WallMaterial"
   - Albedo: Dark blue (#1B1B65)
4. Assign material to cube
5. Add Tag "Wall"
6. Drag to Assets/Prefabs folder to create prefab
7. Delete from scene
8. Assign to MazeGenerator's "Wall Prefab" field

**Floor Prefab:**
1. Create Plane (GameObject → 3D Object → Plane)
2. Scale: (0.1, 1, 0.1)
3. Create Material: "FloorMaterial"
   - Albedo: Black (#000000)
4. Assign material
5. Drag to Assets/Prefabs folder
6. Delete from scene
7. Assign to MazeGenerator's "Floor Prefab" field

**Pellet Prefab:**
1. Create Sphere (GameObject → 3D Object → Sphere)
2. Name: "Pellet"
3. Scale: (0.1, 0.1, 0.1)
4. Create Material: "PelletMaterial"
   - Albedo: White (#FFFFFF)
   - Emission: Enabled, White
5. Add Tag "Pellet"

**Components:**
- **Sphere Collider:**
  - Is Trigger: ✓
  - Radius: 0.5

- **Pellet Script:**
  - Point Value: 10
  - Rotation Speed: 90

- **Point Light (Optional):**
  - Range: 2
  - Intensity: 1
  - Color: White

6. Drag to Assets/Prefabs folder
7. Delete from scene
8. Assign to MazeGenerator's "Pellet Prefab" field

**Power Pellet Prefab:**
1. Duplicate Pellet prefab
2. Name: "PowerPellet"
3. Scale: (0.2, 0.2, 0.2)
4. Update Material: "PowerPelletMaterial"
   - Albedo: Yellow (#FFFF00)
   - Emission: Enabled, Yellow
5. Add Tag "PowerPellet"

**Components:**
- **PowerPellet Script:**
  - Point Value: 50
  - Rotation Speed: 120
  - Pulse Speed: 3
  - Pulse Scale: 0.3

- **Point Light (Optional):**
  - Range: 3
  - Intensity: 2
  - Color: Yellow

6. Save as new prefab
7. Assign to MazeGenerator's "Power Pellet Prefab" field

---

### 7. Camera Setup

**GameObject:** Main Camera

**Transform:**
- Position: (0, 20, -10)
- Rotation: (60, 0, 0)
- This creates a top-down isometric view

**Camera Component:**
- Field of View: 60
- Clipping Planes:
  - Near: 0.3
  - Far: 100

**Optional - Camera Follow Script:**
Create a simple script to follow the player:

```csharp
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 20, -10);
    [SerializeField] private float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (target == null) return;
        
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
```

---

### 8. Lighting Setup

**Directional Light:**
- Rotation: (50, -30, 0)
- Intensity: 1
- Color: Slightly blue-white

**Environment Lighting:**
1. Window → Rendering → Lighting
2. Environment Tab:
   - Skybox Material: Default or create a dark skybox
   - Sun Source: Directional Light
   - Environment Lighting: Ambient Color
   - Ambient Color: Dark blue (#0A0A2E)
   - Ambient Intensity: 0.5

**Add Fog (Optional):**
1. Edit → Project Settings → Graphics
2. Enable Fog: ✓
3. Fog Color: Dark blue
4. Fog Mode: Exponential Squared
5. Fog Density: 0.01

---

### 9. UI Setup

**Canvas Setup:**
1. Create Canvas (GameObject → UI → Canvas)
2. Canvas Scaler:
   - UI Scale Mode: Scale With Screen Size
   - Reference Resolution: 1920 x 1080
   - Match: 0.5

**HUD Elements:**

**ScoreText:**
1. Create TextMeshPro (Right-click Canvas → UI → Text - TextMeshPro)
2. If prompted, import TMP Essentials
3. Name: "ScoreText"
4. Rect Transform:
   - Anchor: Top Left
   - Position: (100, -50)
5. Text: "Score: 0"
6. Font Size: 36
7. Color: White

**HighScoreText:**
1. Duplicate ScoreText
2. Name: "HighScoreText"
3. Position: (100, -100)
4. Text: "High Score: 0"

**LevelText:**
1. Duplicate ScoreText
2. Name: "LevelText"
3. Anchor: Top Center
4. Position: (0, -50)
5. Text: "Level 1"
6. Alignment: Center

**LivesPanel:**
1. Create Empty GameObject (Right-click Canvas → Create Empty)
2. Name: "LivesPanel"
3. Anchor: Top Right
4. Position: (-100, -50)
5. Add Horizontal Layout Group component
6. Create 3 child Images (Right-click LivesPanel → UI → Image)
7. Name them: "Life1", "Life2", "Life3"
8. Set sprite to a heart or player icon
9. Size: 40 x 40 each

**MainMenuPanel:**
1. Create Panel (Right-click Canvas → UI → Panel)
2. Name: "MainMenuPanel"
3. Color: Semi-transparent black (0, 0, 0, 200)

**Content:**
- Title Text: "MAZE CHASE 3D"
  - Font Size: 72
  - Alignment: Center
  - Position: Center-top

- Play Button:
  - Create Button (Right-click Panel → UI → Button)
  - Text: "PLAY"
  - Size: 200 x 60
  - Position: Center

- Quit Button:
  - Duplicate Play Button
  - Text: "QUIT"
  - Position: Below Play button

**PauseMenuPanel:**
1. Duplicate MainMenuPanel
2. Name: "PauseMenuPanel"
3. Title Text: "PAUSED"

**Content:**
- Resume Button: "RESUME"
- Restart Button: "RESTART"
- Quit Button: "QUIT"

**GameOverPanel:**
1. Duplicate MainMenuPanel
2. Name: "GameOverPanel"
3. Title Text: "GAME OVER"

**Content:**
- Score Display
- Restart Button: "TRY AGAIN"
- Main Menu Button: "MAIN MENU"

**LevelCompletePanel:**
1. Duplicate MainMenuPanel
2. Name: "LevelCompletePanel"
3. Title Text: "LEVEL COMPLETE!"

**Content:**
- Score Display
- Continue Button: "CONTINUE"

**Add UIManager Script to Canvas:**
1. Select Canvas GameObject
2. Add UIManager script
3. Drag all UI elements to their respective fields in the Inspector:
   - Score Text
   - High Score Text
   - Level Text
   - Life Icons (array of 3)
   - Main Menu Panel
   - Pause Menu Panel
   - Game Over Panel
   - Level Complete Panel
   - Message Text (create a centered TextMeshPro for temporary messages)
   - Buttons: Play, Resume, Restart, Quit

---

## Materials Setup

### Create These Materials:

1. **PlayerMaterial:**
   - Albedo: Yellow (#FFFF00)
   - Emission: Enabled, Yellow

2. **PlayerPoweredUpMaterial:**
   - Albedo: Cyan (#00FFFF)
   - Emission: Enabled, Cyan, Intensity: 2

3. **Ghost Materials (create 4 different ones):**
   - BlinkyMaterial: Red (#FF0000)
   - PinkyMaterial: Pink (#FFB8FF)
   - InkyMaterial: Cyan (#00FFFF)
   - ClydeMaterial: Orange (#FFB852)

4. **GhostFrightenedMaterial:**
   - Albedo: Blue (#0000FF)
   - Emission: Enabled, White (flashing)

5. **GhostEatenMaterial:**
   - Albedo: Transparent
   - Rendering Mode: Transparent
   - Albedo Alpha: 0.3

**Assign Materials:**
- Player Model → PlayerMaterial
- Each Ghost → Its respective color material
- In each GhostAI script, assign:
  - Normal Material: Ghost's color material
  - Frightened Material: GhostFrightenedMaterial
  - Eaten Material: GhostEatenMaterial

---

## Input Setup

Unity's new Input System is optional, but the default Input Manager works with these settings:

**Default Input Axes (Project Settings → Input Manager):**
- Horizontal: A/D or Left/Right arrows
- Vertical: W/S or Up/Down arrows

These are usually set up by default in Unity.

---

## Audio Setup (Optional but Recommended)

Create/import audio files and assign them:

**Player:**
- Movement Sound: Continuous "wakka wakka" sound
- Death Sound: Descending tone
- Eat Pellet Sound: Short beep
- Eat Ghost Sound: Higher pitch sound

**Ghosts:**
- Frightened Sound: Siren-like sound

You can use free sound effect libraries or create simple beeps using audio tools.

---

## Tags Setup

**Create These Tags:**
(Edit → Project Settings → Tags and Layers)

1. Player
2. Ghost
3. Wall
4. Pellet
5. PowerPellet

---

## Layer Setup (Optional)

For better collision detection:
1. Create layers: Player, Ghost, Wall, Collectible
2. Assign GameObjects to respective layers
3. Configure collision matrix (Edit → Project Settings → Physics):
   - Player can collide with: Ghost, Wall
   - Ghost can collide with: Wall (not each other)
   - Collectibles: Trigger only

---

## Build and Run

### Testing in Editor:
1. Save your scene (File → Save As → "MainScene")
2. Press Play button
3. Test all functionality:
   - Player movement
   - Ghost AI behavior
   - Pellet collection
   - Score system
   - UI menus
   - Level completion

### Building the Game:
1. File → Build Settings
2. Add "MainScene" to Scenes in Build
3. Select target platform (PC, Mac, Linux, etc.)
4. Click "Build"
5. Choose output folder
6. Test the built game

---

## Troubleshooting

### Common Issues:

1. **Player/Ghosts fall through floor:**
   - Check Rigidbody settings
   - Ensure colliders are properly set up
   - Floor should have colliders

2. **Ghosts don't move:**
   - Check GhostAI script is attached
   - Verify Player tag is set correctly
   - Check GhostAI script is enabled

3. **Pellets don't collect:**
   - Verify tags are set correctly
   - Check colliders are triggers
   - Ensure scripts are attached

4. **UI doesn't show:**
   - Check Canvas is set to Screen Space - Overlay
   - Verify UIManager references are set
   - Check panels are children of Canvas

5. **Game doesn't start:**
   - Check GameManager references
   - Verify Start Game is called
   - Check for errors in Console

---

## Performance Optimization Tips

1. **Use Object Pooling** for pellets if generating many
2. **Optimize collision detection** by using layers
3. **Use LOD (Level of Detail)** for complex models
4. **Bake lighting** for static objects
5. **Reduce draw calls** by batching materials
6. **Profile your game** using Unity Profiler (Window → Analysis → Profiler)

---

## Extending the Game

### Ideas for Enhancement:

1. **More Ghost AI Behaviors:**
   - Add different personality types
   - Implement better pathfinding (A* algorithm)

2. **Power-ups:**
   - Speed boost
   - Freeze ghosts
   - Extra lives

3. **Level Design:**
   - Hand-crafted levels instead of procedural
   - Different maze layouts per level
   - Boss levels

4. **Audio:**
   - Background music
   - Better sound effects
   - Voice announcements

5. **Visual Effects:**
   - Particle effects for collection
   - Ghost spawn/respawn effects
   - Screen shake on death

6. **Multiplayer:**
   - Local co-op
   - Competitive mode
   - Online leaderboards

---

# Українська версія

## Передумови
- Unity 2020.3 LTS або новіша версія
- Базові знання Unity Editor
- Пакет TextMeshPro (зазвичай включений за замовчуванням)

---

## Налаштування проєкту

### 1. Створення нового проєкту Unity
1. Відкрийте Unity Hub
2. Натисніть "New Project"
3. Виберіть шаблон "3D"
4. Назвіть проєкт (наприклад, "MazeChaseGame3D")
5. Натисніть "Create"

### 2. Імпорт скриптів
1. Скопіюйте всі C# скрипти з папки `UnityScripts` до папки `Assets/Scripts` вашого Unity проєкту
2. Створіть наступну структуру папок в Unity:
   ```
   Assets/
   ├── Scripts/
   │   ├── Core/
   │   │   ├── GameManager.cs
   │   │   └── GameState.cs
   │   ├── Player/
   │   │   ├── PlayerController.cs
   │   │   └── PlayerAnimation.cs
   │   ├── AI/
   │   │   └── GhostAI.cs
   │   ├── Maze/
   │   │   ├── MazeGenerator.cs
   │   │   ├── Pellet.cs
   │   │   └── PowerPellet.cs
   │   └── UI/
   │       ├── UIManager.cs
   │       └── ScoreManager.cs
   ```

---

## Налаштування ієрархії сцени

### 1. Основна структура сцени
Створіть наступну ієрархію GameObject у вашій сцені:

```
Сцена: MainScene
├── GameManager (Порожній GameObject)
├── ScoreManager (Порожній GameObject)
├── Player (Сфера)
│   └── Model (Сфера - дочірній об'єкт)
├── Ghosts (Порожній GameObject - Контейнер)
│   ├── Ghost_Blinky (Сфера з іншим матеріалом)
│   ├── Ghost_Pinky (Сфера з іншим матеріалом)
│   ├── Ghost_Inky (Сфера з іншим матеріалом)
│   └── Ghost_Clyde (Сфера з іншим матеріалом)
├── Maze (Порожній GameObject - Контейнер)
├── Pellets (Порожній GameObject - Контейнер)
├── Lighting (Освітлення)
│   ├── Directional Light
│   └── Environment Lighting
├── Main Camera
└── Canvas (UI)
    ├── HUD
    │   ├── ScoreText
    │   ├── HighScoreText
    │   ├── LevelText
    │   └── LivesPanel
    ├── MainMenuPanel
    ├── PauseMenuPanel
    ├── GameOverPanel
    └── LevelCompletePanel
```

---

## Детальне налаштування компонентів

### 2. Налаштування GameManager

**GameObject:** GameManager (Порожній GameObject)

**Компоненти:**
1. Додайте скрипт `GameManager`
2. Налаштуйте в Inspector:
   - Starting Lives: 3
   - Level Start Delay: 2
   - Game Over Delay: 3
   - Player: Перетягніть GameObject Player сюди
   - Ghost Container: Перетягніть GameObject Ghosts сюди
   - UI Manager: Перетягніть GameObject Canvas сюди (після додавання UIManager)
   - Maze Generator: Перетягніть GameObject Maze сюди (після додавання MazeGenerator)

### 3. Налаштування ScoreManager

**GameObject:** ScoreManager (Порожній GameObject)

**Компоненти:**
1. Додайте скрипт `ScoreManager`
2. Налаштуйте параметри очок у Inspector

### 4. Налаштування гравця

**GameObject:** Player

**Кроки:**
1. Створіть Sphere GameObject (GameObject → 3D Object → Sphere)
2. Назвіть "Player"
3. Позиція: (0, 0.5, 0)
4. Масштаб: (0.5, 0.5, 0.5)
5. Додайте тег "Player"

**Компоненти:**
1. **Rigidbody:**
   - Use Gravity: ✓
   - Constraints: Freeze Rotation X, Y, Z

2. **Collider:**
   - Is Trigger: ✗
   - Radius: 0.5

3. **PlayerController Script:**
   - Move Speed: 5
   - Rotation Speed: 720

4. **AudioSource:** Для звукових ефектів

### 5. Налаштування привидів

**Для кожного привида (Blinky, Pinky, Inky, Clyde):**

1. Створіть Sphere GameObject
2. Назвіть відповідно (наприклад, "Ghost_Blinky")
3. Встановіть різні стартові позиції
4. Масштаб: (0.6, 0.6, 0.6)
5. Додайте тег "Ghost"

**Компоненти:**
1. **Rigidbody** з заморожуванням обертання
2. **Sphere Collider** як тригер
3. **GhostAI Script** з налаштуваннями швидкості та типу привида

### 6. Налаштування лабіринту

**GameObject:** Maze (Порожній GameObject)

**Компоненти:**
1. Додайте скрипт `MazeGenerator`
2. Налаштуйте розміри лабіринту
3. Створіть префаби для стін, підлоги та точок

**Створення префабів:**

**Префаб стіни:**
1. Створіть Cube
2. Додайте темно-синій матеріал
3. Додайте тег "Wall"
4. Збережіть як префаб

**Префаб точки (Pellet):**
1. Створіть малу Sphere
2. Додайте білий матеріал з емісією
3. Додайте тег "Pellet"
4. Додайте скрипт Pellet
5. Збережіть як префаб

**Префаб силової точки (Power Pellet):**
1. Створіть більшу Sphere
2. Додайте жовтий матеріал з емісією
3. Додайте тег "PowerPellet"
4. Додайте скрипт PowerPellet
5. Збережіть як префаб

### 7. Налаштування камери

**GameObject:** Main Camera

**Transform:**
- Позиція: (0, 20, -10)
- Обертання: (60, 0, 0)
- Це створює вигляд зверху

### 8. Налаштування освітлення

**Directional Light:**
- Обертання: (50, -30, 0)
- Інтенсивність: 1

**Environment Lighting:**
- Темно-синє навколишнє освітлення
- Увімкніть туман для атмосфери (опціонально)

### 9. Налаштування UI

**Canvas:**
1. Створіть Canvas
2. Додайте Canvas Scaler

**Елементи HUD:**
- ScoreText (TextMeshPro) - відображення очок
- HighScoreText - рекорд
- LevelText - поточний рівень
- LivesPanel - іконки життів (3 зображення)

**Меню:**
- MainMenuPanel - головне меню з кнопкою "ГРАТИ"
- PauseMenuPanel - меню паузи
- GameOverPanel - екран завершення гри
- LevelCompletePanel - екран завершення рівня

**Додайте UIManager:**
1. Виберіть Canvas
2. Додайте скрипт UIManager
3. Пов'яжіть всі UI елементи з відповідними полями

---

## Налаштування матеріалів

Створіть ці матеріали:

1. **PlayerMaterial:** Жовтий з емісією
2. **PlayerPoweredUpMaterial:** Блакитний з яскравою емісією
3. **Матеріали привидів:** Червоний, рожевий, блакитний, помаранчевий
4. **GhostFrightenedMaterial:** Синій (коли гравець має силу)
5. **GhostEatenMaterial:** Напівпрозорий

---

## Налаштування тегів

Створіть ці теги (Edit → Project Settings → Tags):
1. Player
2. Ghost
3. Wall
4. Pellet
5. PowerPellet

---

## Тестування

1. Збережіть сцену
2. Натисніть Play
3. Перевірте:
   - Рух гравця
   - Поведінку привидів
   - Збір точок
   - Систему очок
   - Меню

---

## Вирішення проблем

### Типові проблеми:

1. **Гравець падає крізь підлогу:**
   - Перевірте налаштування Rigidbody
   - Переконайтеся, що колайдери правильно налаштовані

2. **Привиди не рухаються:**
   - Перевірте, чи додано скрипт GhostAI
   - Переконайтеся, що тег Player встановлено правильно

3. **Точки не збираються:**
   - Перевірте теги
   - Переконайтеся, що колайдери є тригерами

4. **UI не відображається:**
   - Перевірте Canvas налаштування
   - Переконайтеся, що посилання UIManager встановлені

---

## Розширення гри

### Ідеї для покращення:

1. **Більше поведінки AI привидів**
2. **Додаткові бонуси:**
   - Прискорення
   - Заморожування привидів
   - Додаткові життя
3. **Ручний дизайн рівнів**
4. **Фонова музика та звукові ефекти**
5. **Візуальні ефекти:**
   - Частинки для збору
   - Ефекти появи привидів
6. **Мультиплеєр та таблиці лідерів**

---

## Підсумок

Тепер у вас є повністю функціональна 3D гра-лабіринт! Всі класи та інструкції надані для створення повноцінного ігрового досвіду з:
- Рухом гравця
- ШІ привидів з різними стратегіями
- Процедурною генерацією лабіринту
- Системою очок
- Повноцінним UI
- Системою рівнів

Експериментуйте з налаштуваннями та додавайте власні фічі!
