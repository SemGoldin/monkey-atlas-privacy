# Структура класів Unity C# проекту

## Огляд архітектури

Цей проект демонструє правильну організацію коду для Unity гри з використанням патернів проектування та принципів SOLID. Код структурований за функціональними категоріями з чітким розділенням відповідальностей.

## Структура директорій

```
Scripts/
├── Managers/          # Менеджери та контролери
│   └── GameManager.cs
├── Player/            # Компоненти гравця
│   └── PlayerController.cs
├── Enemy/             # Компоненти ворогів
│   └── EnemyAI.cs
├── UI/                # Користувацький інтерфейс
│   └── UIManager.cs
└── Data/              # Управління даними
    └── GameData.cs
```

---

## Опис класів

### 1. GameManager (Managers/GameManager.cs)

**Тип:** Singleton Manager

**Призначення:** Центральний контролер гри, який керує станом гри та координує різні системи.

**Основні відповідальності:**
- Управління станом гри (меню, гра, пауза, завершення)
- Відстеження рахунку та життів гравця
- Координація між різними системами
- Управління рівнями

**Використані патерни:**
- Singleton - забезпечує єдиний екземпляр в грі
- DontDestroyOnLoad - зберігає об'єкт між сценами

**Ключові методи:**
```csharp
StartNewGame()          // Запуск нової гри
AddScore(int points)    // Додавання очків
LoseLife()              // Втрата життя
SetPause(bool pause)    // Управління паузою
LoadNextLevel()         // Завантаження наступного рівня
```

**Приклад використання:**
```csharp
// Додавання очків
GameManager.Instance.AddScore(100);

// Встановлення паузи
GameManager.Instance.SetPause(true);
```

---

### 2. PlayerController (Player/PlayerController.cs)

**Тип:** MonoBehaviour Component

**Призначення:** Управління персонажем гравця, обробка введення та фізики руху.

**Основні відповідальності:**
- Обробка клавіатурного та геймпадного введення
- Управління рухом та стрибками
- Система здоров'я гравця
- Перевірка контакту з землею
- Респавн після смерті

**Залежності:**
- Rigidbody2D (обов'язковий компонент)
- GameManager (для повідомлення про втрату життя)

**Налаштовувані параметри:**
```csharp
moveSpeed          // Швидкість руху
jumpForce          // Сила стрибка
health             // Поточне здоров'я
maxHealth          // Максимальне здоров'я
groundLayer        // Шар для перевірки землі
```

**Ключові методи:**
```csharp
Move(float direction)   // Горизонтальний рух
Jump()                  // Виконання стрибка
TakeDamage(int damage)  // Отримання урону
Heal(int amount)        // Відновлення здоров'я
```

**Приклад використання:**
```csharp
// Нанесення урону гравцю
playerController.TakeDamage(10);

// Лікування гравця
playerController.Heal(25);
```

---

### 3. EnemyAI (Enemy/EnemyAI.cs)

**Тип:** MonoBehaviour Component

**Призначення:** Управління штучним інтелектом ворожих персонажів.

**Основні відповідальності:**
- Патрулювання між точками
- Виявлення гравця
- Переслідування гравця
- Атака гравця
- Управління здоров'ям ворога

**Стани поведінки (FSM - Finite State Machine):**
- **Patrol** - Патрулювання між заданими точками
- **Chase** - Переслідування гравця
- **Attack** - Атака гравця
- **Dead** - Стан смерті

**Налаштовувані параметри:**
```csharp
health             // Здоров'я ворога
damage             // Урон ворога
scoreValue         // Очки за знищення
patrolSpeed        // Швидкість патрулювання
chaseSpeed         // Швидкість переслідування
detectionRange     // Радіус виявлення гравця
attackRange        // Радіус атаки
attackCooldown     // Затримка між атаками
patrolPoints[]     // Точки патрулювання
```

**Ключові методи:**
```csharp
Patrol()           // Патрулювання
ChasePlayer()      // Переслідування гравця
AttackPlayer()     // Атака гравця
TakeDamage(int)    // Отримання урону
```

**Приклад використання:**
```csharp
// Нанесення урону ворогу
enemy.TakeDamage(20);
```

---

### 4. UIManager (UI/UIManager.cs)

**Тип:** MonoBehaviour Manager

**Призначення:** Управління всіма елементами користувацького інтерфейсу.

**Основні відповідальності:**
- Відображення ігрової статистики (рахунок, життя, здоров'я)
- Управління меню (головне, пауза, Game Over)
- Обробка натискань кнопок
- Оновлення UI в реальному часі

**Елементи UI:**
- **HUD** - Постійні ігрові елементи (рахунок, життя, здоров'я)
- **Main Menu** - Головне меню
- **Pause Menu** - Меню паузи
- **Game Over** - Екран завершення гри

**Налаштовувані елементи:**
```csharp
scoreText          // Текст рахунку
livesText          // Текст життів
healthSlider       // Слайдер здоров'я
mainMenuPanel      // Панель головного меню
pauseMenuPanel     // Панель паузи
gameOverPanel      // Панель Game Over
```

**Ключові методи:**
```csharp
ShowMainMenu()     // Показати головне меню
ShowHUD()          // Показати HUD
ShowPauseMenu()    // Показати меню паузи
ShowGameOver()     // Показати екран завершення
OnNewGameButton()  // Обробник кнопки "Нова гра"
```

**Приклад використання:**
```csharp
// Показати екран Game Over
uiManager.ShowGameOver();

// Показати повідомлення
uiManager.ShowMessage("Рівень пройдено!", 3f);
```

---

### 5. GameData & GameDataManager (Data/GameData.cs)

**Тип:** Data Model + Singleton Manager

**Призначення:** Збереження та управління даними гри, включаючи прогрес та налаштування.

#### GameData (клас даних)

**Дані, що зберігаються:**
```csharp
highScore              // Найкращий рахунок
lastLevel              // Останній рівень
totalCoinsCollected    // Загальна кількість монет
totalEnemiesDefeated   // Загальна кількість знищених ворогів
totalPlayTime          // Загальний час гри
musicVolume            // Гучність музики
sfxVolume              // Гучність ефектів
completedLevels        // Список пройдених рівнів
lastSaveDate           // Дата останнього збереження
```

#### GameDataManager (менеджер)

**Основні відповідальності:**
- Збереження даних через PlayerPrefs
- Завантаження даних при старті
- Серіалізація/десеріалізація JSON
- Оновлення статистики
- Управління налаштуваннями

**Ключові методи:**
```csharp
SaveGame()                      // Збереження гри
LoadGame()                      // Завантаження гри
ResetGame()                     // Скидання даних
UpdateHighScore(int score)      // Оновлення рекорду
AddCoin()                       // Додати монету
AddEnemyDefeated()              // Додати знищеного ворога
CompleteLevel(int level)        // Позначити рівень пройденим
SetMusicVolume(float volume)    // Встановити гучність музики
GetStatistics()                 // Отримати статистику
```

**Приклад використання:**
```csharp
// Збереження гри
GameDataManager.Instance.SaveGame();

// Оновлення рекорду
GameDataManager.Instance.UpdateHighScore(5000);

// Отримання статистики
string stats = GameDataManager.Instance.GetStatistics();
Debug.Log(stats);
```

---

## Взаємодія між класами

### Діаграма залежностей

```
GameManager (Singleton)
    ↑
    ├── PlayerController → TakeDamage → LoseLife()
    ├── EnemyAI → Die() → AddScore()
    ├── UIManager → GetScore(), GetLives(), GetCurrentState()
    └── GameDataManager → UpdateHighScore(), SaveGame()

PlayerController
    ↓
    └── UIManager → GetHealth() (для відображення)

EnemyAI
    ↓
    └── PlayerController → TakeDamage()

UIManager
    ↓
    ├── GameManager (читання стану)
    ├── PlayerController (читання здоров'я)
    └── Buttons → GameManager методи
```

### Потік даних

1. **Початок гри:**
   ```
   UIManager.OnNewGameButton() 
   → GameManager.StartNewGame() 
   → GameDataManager.LoadGame()
   ```

2. **Гравець отримує урон:**
   ```
   EnemyAI.PerformAttack() 
   → PlayerController.TakeDamage() 
   → [якщо health ≤ 0] PlayerController.Die() 
   → GameManager.LoseLife()
   → [якщо lives ≤ 0] GameManager.GameOver()
   ```

3. **Знищення ворога:**
   ```
   PlayerController (зброя) 
   → EnemyAI.TakeDamage() 
   → [якщо health ≤ 0] EnemyAI.Die() 
   → GameManager.AddScore() 
   → GameDataManager.AddEnemyDefeated()
   ```

4. **Оновлення UI:**
   ```
   UIManager.Update() 
   → UpdateGameUI() 
   → GameManager.GetScore(), GetLives() 
   → PlayerController.GetHealth()
   → Оновлення текстів та слайдерів
   ```

---

## Використані патерни проектування

### 1. Singleton Pattern
**Де використовується:** GameManager, GameDataManager

**Чому:** Забезпечує єдиний екземпляр та глобальний доступ до критичних систем гри.

```csharp
public static GameManager Instance { get; private set; }
```

### 2. State Machine (FSM)
**Де використовується:** EnemyAI, GameManager

**Чому:** Керування різними станами поведінки з чіткими переходами.

```csharp
public enum EnemyState { Patrol, Chase, Attack, Dead }
```

### 3. Component Pattern
**Де використовується:** Всі MonoBehaviour класи

**Чому:** Unity архітектура на основі компонентів для модульності.

### 4. Observer Pattern (неявний)
**Де використовується:** UI оновлення

**Чому:** UI спостерігає за змінами в GameManager та PlayerController.

---

## Принципи SOLID

### Single Responsibility Principle (SRP)
Кожен клас має одну чітку відповідальність:
- GameManager - управління станом гри
- PlayerController - управління гравцем
- EnemyAI - поведінка ворогів
- UIManager - користувацький інтерфейс
- GameDataManager - збереження даних

### Open/Closed Principle (OCP)
Класи відкриті для розширення через наслідування:
```csharp
// Можна створити різні типи ворогів
public class FlyingEnemyAI : EnemyAI { }
public class BossEnemyAI : EnemyAI { }
```

### Dependency Inversion Principle (DIP)
Залежності через абстракції (інтерфейси Unity):
```csharp
[RequireComponent(typeof(Rigidbody2D))]
```

---

## Інструкції з налаштування

### Налаштування GameManager
1. Створіть порожній GameObject в сцені
2. Додайте компонент GameManager
3. Об'єкт автоматично стане персистентним (DontDestroyOnLoad)

### Налаштування PlayerController
1. Створіть GameObject для гравця з тегом "Player"
2. Додайте Rigidbody2D (Gravity Scale = 3-5)
3. Додайте Collider2D (Box або Capsule)
4. Додайте компонент PlayerController
5. Створіть дочірній порожній GameObject для groundCheck
6. Призначте параметри в Inspector:
   - Move Speed: 5-7
   - Jump Force: 10-15
   - Ground Check: посилання на дочірній об'єкт
   - Ground Layer: встановіть шар "Ground"

### Налаштування EnemyAI
1. Створіть GameObject для ворога
2. Додайте Rigidbody2D та Collider2D
3. Додайте компонент EnemyAI
4. Створіть порожні GameObjects для точок патрулювання
5. Призначте точки в масив Patrol Points
6. Налаштуйте інші параметри в Inspector

### Налаштування UIManager
1. Створіть Canvas в сцені
2. Створіть UI елементи (панелі, текст, кнопки)
3. Створіть GameObject для UIManager
4. Призначте всі UI елементи в Inspector
5. Підключіть обробники до кнопок

### Налаштування GameDataManager
1. Створіть порожній GameObject
2. Додайте компонент GameDataManager
3. Дані будуть автоматично зберігатись через PlayerPrefs

---

## Найкращі практики

### 1. Коментарі та документація
Всі класи містять XML документацію:
```csharp
/// <summary>
/// Опис методу
/// </summary>
/// <param name="paramName">Опис параметру</param>
/// <returns>Опис повернутого значення</returns>
```

### 2. Серіалізовані поля
Використання атрибутів для кращої організації в Inspector:
```csharp
[Header("Налаштування руху")]
[SerializeField]
[Tooltip("Швидкість руху гравця")]
private float moveSpeed = 5f;
```

### 3. Перевірка null
Завжди перевіряємо посилання перед використанням:
```csharp
if (GameManager.Instance != null)
{
    GameManager.Instance.AddScore(100);
}
```

### 4. Debug логування
Інформативні повідомлення для відлагодження:
```csharp
Debug.Log($"Рахунок: {score}");
Debug.LogWarning("Попередження!");
Debug.LogError("Помилка!");
```

### 5. Візуалізація в редакторі
Gizmos для візуалізації параметрів:
```csharp
private void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, detectionRange);
}
```

---

## Розширення системи

### Додавання нового типу ворога
```csharp
public class BossEnemy : EnemyAI
{
    // Специфічна логіка боса
    protected override void AttackPlayer()
    {
        // Особлива атака боса
    }
}
```

### Додавання нової системи
```csharp
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }
    
    // Логіка управління звуком
}
```

### Додавання нових даних
```csharp
// В класі GameData
public int newStatistic = 0;

// В класі GameDataManager
public void UpdateNewStatistic(int value)
{
    currentData.newStatistic = value;
    SaveGame();
}
```

---

## Висновок

Ця архітектура забезпечує:
- ✅ Чіткий розділ відповідальностей
- ✅ Легку підтримку та розширення
- ✅ Модульність коду
- ✅ Повторне використання компонентів
- ✅ Зрозумілу структуру для команди
- ✅ Відповідність принципам SOLID
- ✅ Використання патернів проектування Unity

Кожен клас має чітко визначену роль, добре задокументований і може бути легко модифікований або розширений без впливу на інші частини системи.
