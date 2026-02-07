# Quick Start Guide / Швидкий старт

## Для розробників / For Developers

Цей репозиторій містить приклад правильно структурованого Unity C# коду з повною документацією.

---

## 📁 Структура проекту

```
monkey-atlas-privacy/
├── Scripts/                    # Unity C# класи
│   ├── Managers/              
│   │   └── GameManager.cs     # Основний менеджер гри (Singleton)
│   ├── Player/                
│   │   └── PlayerController.cs # Управління гравцем
│   ├── Enemy/                 
│   │   └── EnemyAI.cs         # Штучний інтелект ворогів
│   ├── UI/                    
│   │   └── UIManager.cs       # Управління інтерфейсом
│   └── Data/                  
│       └── GameData.cs        # Збереження даних
│
├── README.md                   # Повна документація архітектури
├── CLASS_DIAGRAM.md           # Діаграми класів та зв'язків
├── SUMMARY.md                 # Підсумок проекту
└── QUICK_START.md             # Цей файл
```

---

## 🚀 Швидкий старт (5 хвилин)

### Крок 1: Прочитайте документацію

1. **[README.md](README.md)** - Почніть звідси! Повний опис всіх класів
2. **[CLASS_DIAGRAM.md](CLASS_DIAGRAM.md)** - Візуальні діаграми архітектури
3. **[SUMMARY.md](SUMMARY.md)** - Короткий огляд проекту

### Крок 2: Огляд класів

Кожен клас має чітку відповідальність:

| Клас | Розташування | Призначення |
|------|--------------|-------------|
| `GameManager` | Managers/GameManager.cs | Управління станом гри, рахунком, життями |
| `PlayerController` | Player/PlayerController.cs | Рух, введення, здоров'я гравця |
| `EnemyAI` | Enemy/EnemyAI.cs | AI ворога (патрулювання, атака) |
| `UIManager` | UI/UIManager.cs | Меню, HUD, інтерфейс |
| `GameDataManager` | Data/GameData.cs | Збереження/завантаження даних |

### Крок 3: Використання в Unity

```csharp
// 1. Додати очки до рахунку
GameManager.Instance.AddScore(100);

// 2. Завдати урону гравцю
playerController.TakeDamage(25);

// 3. Показати меню паузи
uiManager.ShowPauseMenu();

// 4. Зберегти гру
GameDataManager.Instance.SaveGame();
```

---

## 📖 Навчальні сценарії

### Сценарій 1: Вивчення Singleton Pattern

Дивіться: `GameManager.cs`, `GameDataManager.cs`

```csharp
// Доступ до singleton екземпляру
GameManager.Instance.StartNewGame();
```

### Сценарій 2: Finite State Machine (FSM)

Дивіться: `EnemyAI.cs`

```csharp
// Стани: Patrol → Chase → Attack → Dead
enum EnemyState { Patrol, Chase, Attack, Dead }
```

### Сценарій 3: Component Architecture

Дивіться: `PlayerController.cs`

```csharp
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour { }
```

### Сценарій 4: Data Persistence

Дивіться: `GameData.cs`

```csharp
// JSON серіалізація + PlayerPrefs
GameDataManager.Instance.SaveGame();
GameDataManager.Instance.LoadGame();
```

---

## 🎓 Що можна навчитися

### Design Patterns (Патерни проектування)
- ✅ **Singleton** - єдиний екземпляр класу
- ✅ **State Machine** - управління станами
- ✅ **Component** - модульна архітектура
- ✅ **Observer** - спостереження за змінами

### SOLID Principles
- ✅ **Single Responsibility** - одна відповідальність
- ✅ **Open/Closed** - відкритий для розширення
- ✅ **Liskov Substitution** - заміщення типів
- ✅ **Interface Segregation** - мінімальні інтерфейси
- ✅ **Dependency Inversion** - залежності через абстракції

### Unity Best Practices
- ✅ SerializeField для Inspector
- ✅ Header/Tooltip для документації
- ✅ RequireComponent для залежностей
- ✅ DontDestroyOnLoad для persistence
- ✅ Gizmos для візуалізації
- ✅ XML коментарі

---

## 🔧 Як використати цей код

### Варіант 1: Вивчення
Читайте код, коментарі та документацію для розуміння архітектури Unity проектів.

### Варіант 2: Шаблон для нового проекту
1. Скопіюйте папку `Scripts/` у ваш Unity проект
2. Адаптуйте під ваші потреби
3. Розширюйте класи через наслідування

### Варіант 3: Референс
Використовуйте як довідник при розробці власних систем.

---

## 💡 Приклади розширення

### Додати новий тип ворога

```csharp
public class FlyingEnemyAI : EnemyAI
{
    // Додаткова логіка польоту
    protected override void Move()
    {
        // Рух у 3D просторі
    }
}
```

### Додати новий менеджер

```csharp
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }
    
    public void PlaySound(string soundName) { }
    public void PlayMusic(string musicName) { }
}
```

### Розширити дані

```csharp
// У класі GameData
public int achievementsUnlocked = 0;

// У класі GameDataManager
public void UnlockAchievement(int id)
{
    currentData.achievementsUnlocked++;
    SaveGame();
}
```

---

## 📚 Рекомендована послідовність вивчення

1. **День 1:** Прочитати README.md (30 хв)
2. **День 2:** Вивчити GameManager.cs (15 хв)
3. **День 3:** Вивчити PlayerController.cs (20 хв)
4. **День 4:** Вивчити EnemyAI.cs (25 хв)
5. **День 5:** Вивчити UIManager.cs (20 хв)
6. **День 6:** Вивчити GameData.cs (20 хв)
7. **День 7:** Прочитати CLASS_DIAGRAM.md (20 хв)

**Загальний час:** ~2.5 години

---

## 🎯 Перевірка розуміння

### Контрольні питання:

1. Що таке Singleton і де він використовується?
2. Які стани має EnemyAI?
3. Як PlayerController взаємодіє з GameManager?
4. Який патерн використовується для UI?
5. Як зберігаються дані гри?

**Відповіді знайдете в коді та документації!**

---

## 🛠️ Налаштування в Unity (Крок за кроком)

### 1. GameManager
```
1. Create → Empty GameObject → "GameManager"
2. Add Component → GameManager
3. Готово! (автоматично персистентний)
```

### 2. Player
```
1. Create → 2D Object → Sprite
2. Tag → "Player"
3. Add Component → Rigidbody2D
4. Add Component → Box Collider 2D
5. Add Component → Player Controller
6. Create child Empty → "GroundCheck"
7. Налаштуйте параметри в Inspector
```

### 3. Enemy
```
1. Create → 2D Object → Sprite
2. Add Component → Rigidbody2D
3. Add Component → Enemy AI
4. Create Empty GameObjects для патрульних точок
5. Призначте точки в масив Patrol Points
```

### 4. UI
```
1. Create → UI → Canvas
2. Створіть панелі для меню
3. Create → Empty GameObject → "UIManager"
4. Add Component → UI Manager
5. Призначте UI елементи в Inspector
```

---

## 📞 Додаткові ресурси

- **Unity Documentation:** https://docs.unity3d.com/
- **C# Documentation:** https://docs.microsoft.com/dotnet/csharp/
- **Game Programming Patterns:** https://gameprogrammingpatterns.com/

---

## ✅ Чеклист вивчення

- [ ] Прочитав README.md
- [ ] Розумію Singleton pattern
- [ ] Розумію State Machine
- [ ] Вивчив всі 5 класів
- [ ] Подивився діаграми
- [ ] Зрозумів SOLID принципи
- [ ] Можу пояснити архітектуру
- [ ] Готовий використати в проекті

---

## 🎉 Підсумок

Цей код демонструє:
- ✅ Професійну архітектуру Unity проектів
- ✅ Індустріальні патерни проектування
- ✅ Чистий, підтримуваний код
- ✅ Повну документацію
- ✅ Найкращі практики Unity

**Успіхів у розробці ігор!** 🎮
