# Nut Sorting Puzzle Game - Unity Implementation

## Описание проекта
Це повна реалізація мобільної гри-головоломки з сортування гайок на Unity. Гра включає процедурну генерацію рівнів, систему зірок залежно від кількості ходів, автоматичне збереження прогресу та гнучку систему керування звуком і ефектами.

## Структура проекту

```
Assets/
├── Scenes/
│   ├── Menu.unity          # Головне меню
│   └── Game.unity          # Ігрова сцена
├── Scripts/
│   ├── Core/
│   │   ├── Nut.cs         # Клас гайки
│   │   ├── Bolt.cs        # Клас болта
│   │   ├── LevelGenerator.cs  # Генератор рівнів
│   │   ├── GameManager.cs     # Менеджер гри
│   │   └── BoltInputHandler.cs # Обробка кліків
│   ├── UI/
│   │   ├── MenuManager.cs     # Менеджер меню
│   │   └── GameUIManager.cs   # Менеджер ігрового UI
│   ├── Managers/
│   │   ├── SaveManager.cs     # Система збереження
│   │   ├── AudioManager.cs    # Менеджер аудіо
│   │   └── EffectsManager.cs  # Менеджер ефектів
│   └── Data/
│       ├── GameData.cs        # Дані гри
│       ├── LevelConfig.cs     # Конфігурація рівнів
│       └── AudioConfig.cs     # Конфігурація аудіо
├── Prefabs/
│   ├── Bolt.prefab
│   └── Nut.prefab
└── Resources/
    ├── Audio/
    └── Materials/
```

## Налаштування сцен

### Сцена Menu

1. **Створення сцени Menu:**
   - File → New Scene
   - Зберегти як `Assets/Scenes/Menu.unity`

2. **Налаштування Canvas:**
   - GameObject → UI → Canvas
   - Canvas Scaler:
     - UI Scale Mode: Scale With Screen Size
     - Reference Resolution: 1080x1920 (Portrait)
     - Match: 0.5

3. **Додавання MenuManager:**
   - Створити порожній GameObject "MenuManager"
   - Додати компонент MenuManager
   - Додати AudioConfig ScriptableObject (Assets → Create → Game → Audio Configuration)

4. **UI Елементи:**
   - **Title Panel:**
     - TextMeshPro - Text: "Nut Sorting Puzzle"
     - Font Size: 72, Align: Center
   
   - **Main Buttons:**
     - Button "Play" → OnPlayClicked()
     - Button "Continue" → OnContinueClicked()
     - Button "Settings" → OnSettingsClicked()
     - Button "Quit" → OnQuitClicked()
   
   - **Score Display:**
     - TextMeshPro: Current Level
     - TextMeshPro: Total Score
   
   - **Settings Panel (спочатку неактивна):**
     - Toggle "Sound"
     - Toggle "Music"
     - Toggle "Effects"
     - Button "Close"

5. **Зв'язування в Inspector:**
   - Перетягнути всі UI елементи у відповідні поля MenuManager

### Сцена Game

1. **Створення сцени Game:**
   - File → New Scene
   - Зберегти як `Assets/Scenes/Game.unity`

2. **Налаштування Camera:**
   - Main Camera
   - Position: (0, 0, -10)
   - Projection: Orthographic
   - Size: 5
   - Background: Solid Color (темний колір)

3. **Створення GameManager:**
   - Порожній GameObject "GameManager"
   - Додати компонент GameManager
   - Додати LevelConfig ScriptableObject (Assets → Create → Game → Level Configuration)
   - Додати AudioConfig ScriptableObject

4. **Створення LevelGenerator:**
   - Порожній GameObject "LevelGenerator"
   - Додати компонент LevelGenerator
   - Налаштувати:
     - Bolt Spacing: 1.5
     - Start Position: (-3, -2, 0)

5. **Canvas для Game UI:**
   - GameObject → UI → Canvas
   - Canvas Scaler: Scale With Screen Size (1080x1920)
   
   **HUD Elements:**
   - TextMeshPro "Level": Верх екрану, центр
   - TextMeshPro "Moves": Верх екрану, ліво
   - Images "Stars" (3 зірки): Верх екрану, право
   - Button "Pause": Верх екрану
   
   **Level Complete Panel (спочатку неактивна):**
   - Background Panel
   - TextMeshPro "Level Complete!"
   - TextMeshPro "Moves: X"
   - Images "Stars" (3 зірки для відображення результату)
   - Button "Next Level"
   - Button "Restart"
   - Button "Menu"
   
   **Pause Panel (спочатку неактивна):**
   - Background Panel
   - TextMeshPro "Paused"
   - Button "Resume"
   - Button "Restart"
   - Button "Menu"
   - Toggle "Sound"
   - Toggle "Music"
   - Toggle "Effects"

6. **Створення GameUIManager:**
   - Додати компонент GameUIManager до Canvas
   - Зв'язати всі UI елементи у Inspector

## Створення Prefabs

### Bolt Prefab

1. **Створення:**
   - GameObject → 2D Object → Sprite → Square
   - Назвати "Bolt"
   - Transform Position: (0, 0, 0)
   - Transform Scale: (0.4, 1.2, 1)

2. **Компоненти:**
   - Sprite Renderer:
     - Color: Gray (128, 128, 128)
     - Sorting Layer: Default
   - Box Collider 2D:
     - Size: (0.4, 1.2)
   - Bolt Script
   - BoltInputHandler Script

3. **Налаштування Bolt Script:**
   - Capacity: 4
   - Nut Spacing: 0.3

4. Зберегти як Prefab: `Assets/Prefabs/Bolt.prefab`

### Nut Prefab

1. **Створення:**
   - GameObject → 2D Object → Sprite → Circle
   - Назвати "Nut"
   - Transform Scale: (0.25, 0.25, 1)

2. **Компоненти:**
   - Sprite Renderer:
     - Color: White (буде змінюватись в коді)
     - Sorting Layer: Default
     - Order in Layer: 1
   - Circle Collider 2D:
     - Radius: 0.5
   - Nut Script

3. Зберегти як Prefab: `Assets/Prefabs/Nut.prefab`

## Створення ScriptableObjects

### Level Configuration

1. **Створення:**
   - Assets → Create → Game → Level Configuration
   - Назвати "DefaultLevelConfig"

2. **Налаштування:**
   - Min Bolts: 3
   - Max Bolts: 8
   - Nuts Per Bolt: 4
   - Bolts Increase Every N Levels: 5
   - Three Star Max Moves: 10
   - Two Star Max Moves: 20
   - One Star Max Moves: 40
   - Nut Colors: Масив з 8 кольорів (Red, Blue, Green, Yellow, Cyan, Magenta, Orange, Purple)

### Audio Configuration

1. **Створення:**
   - Assets → Create → Game → Audio Configuration
   - Назвати "DefaultAudioConfig"

2. **Налаштування:**
   - Додати AudioClips для кожного звуку (якщо доступні)
   - Master Volume: 1.0
   - SFX Volume: 1.0
   - Music Volume: 0.5

## Build Settings

1. **Додавання сцен:**
   - File → Build Settings
   - Add Open Scenes:
     - Menu (index 0)
     - Game (index 1)

2. **Platform Settings (Android):**
   - Platform: Android
   - Texture Compression: ASTC
   - API Level: Minimum API Level 24
   - Scripting Backend: IL2CPP
   - Target Architectures: ARM64

3. **Player Settings:**
   - Company Name: [Your Company]
   - Product Name: Nut Sorting Puzzle
   - Package Name: com.yourcompany.nutpuzzle
   - Version: 1.0.0
   - Orientation: Portrait
   - Status Bar: Hidden

## Основні функції

### Система рівнів
- Процедурна генерація рівнів
- Складність збільшується з кожним рівнем
- Рандомне перемішування гайок на початку

### Система зірок
- 3 зірки: ≤ 10 ходів
- 2 зірки: ≤ 20 ходів
- 1 зірка: ≤ 40 ходів
- 0 зірок: > 40 ходів

### Система збереження
- Автоматичне збереження після кожного ходу
- Збереження прогресу рівня
- Збереження кращих результатів
- Можливість продовжити незавершений рівень

### Аудіо система
- Фонова музика для меню та гри
- Звукові ефекти для дій
- Окремі регулятори гучності
- Можливість вимкнути звук/музику

### Система ефектів
- Партикли для різних подій
- Анімації масштабування
- Анімації обертання
- Можливість вимкнути ефекти

## Геймплей

1. **Правила:**
   - Клік на болт підбирає верхню гайку
   - Повторний клік на той самий болт повертає гайку назад
   - Клік на інший болт намагається помістити гайку
   - Гайку можна помістити тільки на порожній болт або болт з гайками того ж кольору
   - Болт вважається завершеним, коли всі гайки одного кольору

2. **Перемога:**
   - Рівень завершується, коли всі болти або порожні, або повністю заповнені гайками одного кольору
   - Нараховуються зірки залежно від кількості ходів
   - Відкривається наступний рівень

## Тестування

1. **Menu Scene:**
   - Перевірити завантаження збережених даних
   - Перевірити кнопки Play, Continue, Settings
   - Перевірити роботу toggles в налаштуваннях

2. **Game Scene:**
   - Перевірити генерацію рівня
   - Перевірити механіку переміщення гайок
   - Перевірити підрахунок ходів
   - Перевірити систему зірок
   - Перевірити збереження прогресу
   - Перевірити звуки та ефекти

## Подальше вдосконалення

1. **Візуальні покращення:**
   - Додати кастомні спрайти для болтів та гайок
   - Додати анімації
   - Покращити UI дизайн

2. **Геймплей:**
   - Додати систему підказок
   - Додати можливість скасування ходу
   - Додати різні типи рівнів

3. **Монетизація:**
   - Інтегрувати рекламу (AdMob)
   - Додати in-app purchases
   - Додати систему нагород

## Вимоги

- Unity 2021.3 LTS або новіша
- TextMeshPro (включено в Unity)
- Android SDK (для Android builds)

## Контакти

Email: vaschishin.s@gmail.com

---

**Примітка:** Це базова реалізація гри. Для повноцінної гри рекомендується додати професійну графіку, звуки, додаткові рівні складності та монетизацію.
