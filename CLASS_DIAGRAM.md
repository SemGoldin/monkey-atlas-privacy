# Діаграма класів Unity проекту

## ASCII Діаграма класів та їх взаємозв'язків

```
┌─────────────────────────────────────────────────────────────────────────┐
│                          ARCHITECTURE OVERVIEW                          │
└─────────────────────────────────────────────────────────────────────────┘

┌───────────────────────────────────────────────────────────────────────────┐
│                                                                           │
│                           CORE MANAGERS                                   │
│                                                                           │
│  ┌────────────────────────┐              ┌──────────────────────────┐   │
│  │    GameManager         │              │   GameDataManager        │   │
│  │    (Singleton)         │              │   (Singleton)            │   │
│  ├────────────────────────┤              ├──────────────────────────┤   │
│  │ - instance             │              │ - instance               │   │
│  │ - currentState         │◄─────────────│ - currentData            │   │
│  │ - score                │   взаємодія  │ - SAVE_KEY               │   │
│  │ - lives                │              │                          │   │
│  │ - currentLevel         │              │ + SaveGame()             │   │
│  ├────────────────────────┤              │ + LoadGame()             │   │
│  │ + StartNewGame()       │              │ + ResetGame()            │   │
│  │ + AddScore()           │              │ + UpdateHighScore()      │   │
│  │ + LoseLife()           │              │ + AddCoin()              │   │
│  │ + SetPause()           │              │ + CompleteLevel()        │   │
│  │ + LoadNextLevel()      │              │ + GetStatistics()        │   │
│  │ + GetCurrentState()    │              └──────────────────────────┘   │
│  └────────────────────────┘                        ▲                     │
│           ▲                                        │                     │
│           │                                        │                     │
│           │                                        │ зберігає            │
│           │                                        │                     │
│           │                         ┌──────────────┴─────────────┐      │
│           │                         │      GameData              │      │
│           │                         │      (Data Model)          │      │
│           │                         ├────────────────────────────┤      │
│           │                         │ + highScore                │      │
│           │                         │ + lastLevel                │      │
│           │                         │ + totalCoinsCollected      │      │
│           │                         │ + totalEnemiesDefeated     │      │
│           │                         │ + totalPlayTime            │      │
│           │                         │ + musicVolume              │      │
│           │                         │ + sfxVolume                │      │
│           │                         │ + completedLevels          │      │
│           │                         │ + lastSaveDate             │      │
│           │                         └────────────────────────────┘      │
│           │                                                              │
└───────────┼──────────────────────────────────────────────────────────────┘
            │
            │ координує
            │
┌───────────┼──────────────────────────────────────────────────────────────┐
│           │                  GAME ENTITIES                               │
│           │                                                              │
│  ┌────────┴───────────────┐                  ┌──────────────────────┐   │
│  │   PlayerController     │                  │     EnemyAI          │   │
│  │   (MonoBehaviour)      │                  │  (MonoBehaviour)     │   │
│  ├────────────────────────┤                  ├──────────────────────┤   │
│  │ - rb: Rigidbody2D      │                  │ - currentState       │   │
│  │ - moveSpeed            │                  │ - health             │   │
│  │ - jumpForce            │                  │ - damage             │   │
│  │ - health               │                  │ - patrolSpeed        │   │
│  │ - maxHealth            │                  │ - chaseSpeed         │   │
│  │ - isGrounded           │                  │ - detectionRange     │   │
│  │ - groundLayer          │                  │ - attackRange        │   │
│  ├────────────────────────┤                  │ - patrolPoints[]     │   │
│  │ + Move()               │◄─────атакує──────│ - playerTransform    │   │
│  │ + Jump()               │                  ├──────────────────────┤   │
│  │ + TakeDamage()         │                  │ + Patrol()           │   │
│  │ + Heal()               │                  │ + ChasePlayer()      │   │
│  │ + Die()                │                  │ + AttackPlayer()     │   │
│  │ + Respawn()            │                  │ + TakeDamage()       │   │
│  │ + GetHealth()          │                  │ + Die()              │   │
│  └────────────────────────┘                  └──────────────────────┘   │
│           │                                            │                 │
│           │ повідомляє про смерть                     │ додає очки      │
│           │                                            │                 │
│           └────────────────┬───────────────────────────┘                 │
│                            │                                             │
└────────────────────────────┼─────────────────────────────────────────────┘
                             │
                             │ надає дані
                             │
┌────────────────────────────┼─────────────────────────────────────────────┐
│                            ▼              USER INTERFACE                 │
│                 ┌──────────────────────┐                                 │
│                 │     UIManager        │                                 │
│                 │  (MonoBehaviour)     │                                 │
│                 ├──────────────────────┤                                 │
│                 │ - scoreText          │                                 │
│                 │ - livesText          │                                 │
│                 │ - healthSlider       │                                 │
│                 │ - mainMenuPanel      │                                 │
│                 │ - pauseMenuPanel     │                                 │
│                 │ - gameOverPanel      │                                 │
│                 │ - hudPanel           │                                 │
│                 │ - playerController   │                                 │
│                 ├──────────────────────┤                                 │
│                 │ + UpdateGameUI()     │                                 │
│                 │ + ShowMainMenu()     │                                 │
│                 │ + ShowHUD()          │                                 │
│                 │ + ShowPauseMenu()    │                                 │
│                 │ + ShowGameOver()     │                                 │
│                 │ + OnNewGameButton()  │                                 │
│                 │ + OnResumeButton()   │                                 │
│                 │ + OnQuitButton()     │                                 │
│                 └──────────────────────┘                                 │
│                                                                           │
└───────────────────────────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────────────────────────┐
│                        STATE DIAGRAMS                                   │
└─────────────────────────────────────────────────────────────────────────┘

┌───────────────────────────────────────────────────────────────┐
│  GameManager States (Game State Machine)                     │
│                                                               │
│      ┌─────────────┐                                         │
│      │  MainMenu   │                                         │
│      └──────┬──────┘                                         │
│             │ Start Game                                     │
│             ▼                                                 │
│      ┌─────────────┐     Pause      ┌────────────┐          │
│      │   Playing   │◄──────────────►│   Paused   │          │
│      └──────┬──────┘     Resume     └────────────┘          │
│             │                                                 │
│             │ Lives = 0                                      │
│             ▼                                                 │
│      ┌─────────────┐                                         │
│      │  GameOver   │                                         │
│      └─────────────┘                                         │
│                                                               │
└───────────────────────────────────────────────────────────────┘

┌───────────────────────────────────────────────────────────────┐
│  EnemyAI States (Finite State Machine)                       │
│                                                               │
│      ┌─────────────┐                                         │
│      │   Patrol    │                                         │
│      └──────┬──────┘                                         │
│             │ Player detected                                │
│             ▼                                                 │
│      ┌─────────────┐                                         │
│      │    Chase    │                                         │
│      └──────┬──────┘                                         │
│             │ In attack range                                │
│             ▼                                                 │
│      ┌─────────────┐                                         │
│      │   Attack    │                                         │
│      └──────┬──────┘                                         │
│             │ Health = 0                                     │
│             ▼                                                 │
│      ┌─────────────┐                                         │
│      │    Dead     │                                         │
│      └─────────────┘                                         │
│                                                               │
└───────────────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────────────────────────┐
│                      SEQUENCE DIAGRAMS                                  │
└─────────────────────────────────────────────────────────────────────────┘

┌───────────────────────────────────────────────────────────────────────┐
│  Сценарій: Гравець отримує урон від ворога                          │
│                                                                       │
│  EnemyAI          PlayerController      GameManager      UIManager   │
│     │                    │                   │               │        │
│     │ PerformAttack()    │                   │               │        │
│     ├───────────────────►│                   │               │        │
│     │                    │ TakeDamage(10)    │               │        │
│     │                    ├───────────┐       │               │        │
│     │                    │           │       │               │        │
│     │                    │◄──────────┘       │               │        │
│     │                    │                   │               │        │
│     │                    │ health <= 0?      │               │        │
│     │                    ├───────────┐       │               │        │
│     │                    │           │       │               │        │
│     │                    │◄──────────┘       │               │        │
│     │                    │                   │               │        │
│     │                    │ LoseLife()        │               │        │
│     │                    ├──────────────────►│               │        │
│     │                    │                   │ lives--       │        │
│     │                    │                   ├────────┐      │        │
│     │                    │                   │        │      │        │
│     │                    │                   │◄───────┘      │        │
│     │                    │                   │               │        │
│     │                    │                   │ UpdateUI()    │        │
│     │                    │                   ├──────────────►│        │
│     │                    │                   │               │        │
│                                                                       │
└───────────────────────────────────────────────────────────────────────┘

┌───────────────────────────────────────────────────────────────────────┐
│  Сценарій: Знищення ворога                                           │
│                                                                       │
│  PlayerController    EnemyAI       GameManager    GameDataManager    │
│     │                   │               │                │            │
│     │ Attack()          │               │                │            │
│     ├──────────────────►│               │                │            │
│     │                   │ TakeDamage()  │                │            │
│     │                   ├────────┐      │                │            │
│     │                   │        │      │                │            │
│     │                   │◄───────┘      │                │            │
│     │                   │               │                │            │
│     │                   │ health <= 0?  │                │            │
│     │                   ├────────┐      │                │            │
│     │                   │        │      │                │            │
│     │                   │◄───────┘      │                │            │
│     │                   │               │                │            │
│     │                   │ AddScore(100) │                │            │
│     │                   ├──────────────►│                │            │
│     │                   │               │                │            │
│     │                   │               │ AddEnemyDefeated()          │
│     │                   │               ├───────────────►│            │
│     │                   │               │                │            │
│     │                   │ Destroy()     │                │            │
│     │                   ├────────┐      │                │            │
│     │                   │        │      │                │            │
│     │                   │◄───────┘      │                │            │
│     │                   X               │                │            │
│                                                                       │
└───────────────────────────────────────────────────────────────────────┘


┌─────────────────────────────────────────────────────────────────────────┐
│                    DEPENDENCY GRAPH                                     │
└─────────────────────────────────────────────────────────────────────────┘

                             Unity Engine
                                  │
                ┌─────────────────┼─────────────────┐
                │                 │                 │
                ▼                 ▼                 ▼
         MonoBehaviour      Rigidbody2D      UI Components
                │                 │                 │
    ┌───────────┼──────────┬──────┴──────┬──────────┴──────────┐
    │           │          │             │                     │
    ▼           ▼          ▼             ▼                     ▼
GameManager  EnemyAI  PlayerController UIManager    GameDataManager
    │           │          │             │                     │
    │           │          │             │                     │
    │           └──────────┼─────────────┘                     │
    │                      │                                   │
    └──────────────────────┴───────────────────────────────────┘
                           │
                           ▼
                       GameData


┌─────────────────────────────────────────────────────────────────────────┐
│                  COMPONENT RELATIONSHIP                                 │
└─────────────────────────────────────────────────────────────────────────┘

Legend:
  ──────►  Викликає методи / Використовує
  ◄─────►  Двостороння взаємодія
  ─ ─ ─►  Читає дані
  ═════►  Наслідування


     GameManager                    PlayerController
     (Singleton)                    (Component)
         │                               │
         │ координує                     │ управляє
         ▼                               ▼
    ┌────────┐                      ┌─────────┐
    │  Game  │                      │  Player │
    │  State │                      │  Entity │
    └────────┘                      └─────────┘
         │                               │
         │                               │ отримує урон від
         │                               │
         ▼                               ▼
      EnemyAI ────────атакує────────► Health
    (Component)                        System
         │
         │ патрулює/переслідує
         ▼
    AI States
    (FSM)


     UIManager ─ ─ ─ ─читає─ ─ ─►  GameManager
         │                              │
         │                              │
    відображає                    відображає
         │                              │
         ▼                              ▼
    UI Elements                    Game State
    - Buttons                      - Score
    - Text                         - Lives
    - Panels                       - Level
    - Sliders


  GameDataManager ◄─────викликає────► GameManager
         │                                  │
         │ зберігає                    оновлює│
         ▼                                  ▼
    PlayerPrefs                         Statistics
    (Persistent)                        - High Score
                                       - Progress
```

## Пояснення зв'язків

### 1. **Композиція (Composition)**
- PlayerController містить Rigidbody2D
- UIManager містить UI елементи (Text, Slider, Panel)

### 2. **Агрегація (Aggregation)**
- EnemyAI посилається на PlayerTransform
- UIManager посилається на PlayerController

### 3. **Залежність (Dependency)**
- PlayerController викликає GameManager.LoseLife()
- EnemyAI викликає GameManager.AddScore()
- UIManager читає дані з GameManager

### 4. **Singleton Pattern**
- GameManager.Instance
- GameDataManager.Instance

### 5. **State Pattern**
- GameManager.GameState (enum)
- EnemyAI.EnemyState (enum)

---

## Потоки даних

### Ігровий цикл (Game Loop):
```
Update() → Input → Physics → AI → Collision → UI Update → Render
```

### Збереження даних:
```
GameManager → GameDataManager → JSON → PlayerPrefs → Persistent Storage
```

### UI оновлення:
```
Game Events → GameManager → UIManager → UI Elements → Screen
```
