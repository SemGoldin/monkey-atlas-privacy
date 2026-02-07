# Повний курс C# в Unity (українською)

Практично орієнтований курс для початківців та студентів середнього рівня. Кожен урок містить теорію, приклади коду C#, пояснення, практичні завдання та типові помилки.

---

## Модуль 1. Знайомство з Unity та C#

### Урок 1. Що таке Unity та C#
**Теорія:** Unity — рушій для створення 2D/3D ігор; C# — основна мова скриптів у Unity. Проєкт складається зі сцен, на яких розміщені GameObject’и з компонентами.  
**Код-приклад:**
```csharp
using UnityEngine;

public class HelloUnity : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Привіт, Unity!");
    }
}
```
**Пояснення:** `MonoBehaviour` дає життєвий цикл. `Start()` викликається раз після активації об’єкта.  
**Практика:** Створити сцену, додати порожній GameObject, прикріпити скрипт, запустити та побачити лог у Console.  
**Типові помилки:** Не зберегли скрипт у папці `Assets`; не додали скрипт як компонент до об’єкта.

### Урок 2. Інтерфейс Unity Editor
**Теорія:** Scene/Hierarchy/Inspector/Game/Project/Console. Панелі можна докувати.  
**Практика:** Відкрити зразок сцени, змінити вікно Layout, знайти Transform об’єкта в Inspector.  
**Типові помилки:** Закривання Console → не видно помилок; зміни в сцені без `Ctrl+S` не зберігаються.

### Урок 3. GameObject і Component. MonoBehaviour
**Теорія:** GameObject — контейнер; Component — поведінка/дані. Скрипти C# є компонентами.  
**Код-приклад:**
```csharp
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 45f; // градусів за секунду

    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f);
    }
}
```
**Пояснення:** `transform` — компонент Transform. `Time.deltaTime` робить рух плавним.  
**Практика:** Додати куб, прикріпити `Rotator`, змінити speed у Inspector.  
**Типові помилки:** Використання `Update` без `deltaTime` → залежність від FPS; забули зробити поле `public` або `[SerializeField]`, тому не видно в Inspector.

---

## Модуль 2. Основи C# для Unity

### Урок 4. Змінні та типи даних
**Теорія:** `int`, `float`, `bool`, `string`, `Vector3`. Різниця між `public` і `private`.  
**Код-приклад:**
```csharp
using UnityEngine;

public class VariablesDemo : MonoBehaviour
{
    [SerializeField] private string playerName = "Hero";
    public int health = 100;

    void Start()
    {
        Vector3 spawn = new Vector3(0f, 1f, 0f);
        Debug.Log($"Гравець {playerName} має {health} HP. Спавн у {spawn}");
    }
}
```
**Пояснення:** `[SerializeField] private` ховає поле в коді, але показує в Inspector.  
**Практика:** Створити скрипт зі змінними різних типів, показати/сховати їх у Inspector.  
**Типові помилки:** Використання `double` замість `float` у Unity інспекторі; відсутність `f` у літералах (`1.0` → слід `1.0f`).

### Урок 5. Методи та життєвий цикл (Start, Update)
**Теорія:** `Awake` (ініціалізація), `Start` (після активації), `Update` (кожен кадр), `FixedUpdate` (фізика), `OnEnable`/`OnDisable`.  
**Код-приклад:**
```csharp
using UnityEngine;

public class LifecycleDemo : MonoBehaviour
{
    void Awake()  { Debug.Log("Awake"); }
    void Start()  { Debug.Log("Start"); }
    void Update() { Debug.Log("Update кожен кадр"); }
}
```
**Пояснення:** Порядок викликів важливий для ініціалізації залежностей.  
**Практика:** Додати `FixedUpdate` з логом, увімкнути/вимкнути об’єкт у грі й простежити виклики.  
**Типові помилки:** Логіка фізики в `Update` → різний результат при різному FPS; спроба звернутися до компонента в `Awake`, якщо він додається пізніше.

### Урок 6. Умови (if / else)
**Теорія:** Порівняння, логічні оператори `&&`, `||`, `!`.  
**Код-приклад:**
```csharp
if (health <= 0)
{
    Die();
}
else if (health < 30)
{
    Debug.Log("Мало HP!");
}
```
**Пояснення:** Керування гілками логіки.  
**Практика:** Реалізувати перевірку стану здоров’я та відображення повідомлень.  
**Типові помилки:** Використання `=` замість `==`; забули фігурні дужки при кількох рядках.

### Урок 7. Цикли (for / while)
**Теорія:** Перебір колекцій, лічильники.  
**Код-приклад:**
```csharp
for (int i = 0; i < enemies.Length; i++)
{
    enemies[i].TakeDamage(1);
}
```
**Пояснення:** Цикл for має лічильник; while виконує до умови.  
**Практика:** Створити масив чисел, порахувати суму; у грі — пронумерувати ворогів у логах.  
**Типові помилки:** Нескінченний while без зміни умови; вихід за межі масиву.

---

## Модуль 3. Робота зі сценою та взаємодіями

### Урок 8. Робота з Input (клавіатура, миша)
**Теорія:** `Input.GetAxis`, `Input.GetKey`, новий Input System (огляд).  
**Код-приклад (старий Input):**
```csharp
float h = Input.GetAxis("Horizontal");
float v = Input.GetAxis("Vertical");
Vector3 dir = new Vector3(h, 0f, v);
transform.Translate(dir * moveSpeed * Time.deltaTime, Space.World);
```
**Пояснення:** Axis — згладжений ввід. `Space.World` рухає у світових координатах.  
**Практика:** Зробити рух куба клавішами WASD; обертання правою кнопкою миші.  
**Типові помилки:** Відсутність `deltaTime`; використання `GetKeyDown/GetKeyUp` у `FixedUpdate` (події вводу можуть загубитися між фізичними тиками).

### Урок 9. Transform: позиція, обертання, масштаб
**Теорія:** `transform.position`, `rotation`, `localScale`.  
**Код-приклад:**
```csharp
transform.position = new Vector3(0, 1, 0);
transform.localScale *= 1.1f;
transform.Rotate(Vector3.up * 90f); // одноразовий поворот 90° у події (кнопка/клік)
// приклад методу, який викликає кнопка UI:
// public void OnRotateButton() { transform.Rotate(Vector3.up * 90f); }
```
**Приклад безперервного обертання в Update:**  
```csharp
void Update()
{
    transform.Rotate(Vector3.up * 45f * Time.deltaTime); // 45°/с
}
```
**Пояснення:** `Rotate` додає обертання при кожному виклику; для одноразового повороту викликаємо метод у події. Якщо потрібно плавне обертання, перенесіть виклик у `Update` і помножте на `Time.deltaTime`.  
**Практика:** Створити платформу, збільшувати її масштаб при натисканні клавіші; додати `Mathf.Clamp` для обмеження масштабу (наприклад 0.5f–3f); одноразово обертати на 90° після натискання окремої клавіші.  
**Типові помилки:** Плутанина `position` vs `localPosition`; множення масштабу без обмежень → нульовий або надто великий scale.

### Урок 10. Collider і Rigidbody. Зіткнення та тригери
**Теорія:** Collider визначає форму; Rigidbody додає фізику. `isTrigger` для подій без фізичного відштовхування.  
**Код-приклад:**
```csharp
void OnCollisionEnter(Collision collision)
{
    Debug.Log("Зіткнення з " + collision.gameObject.name);
}

void OnTriggerEnter(Collider other)
{
    Debug.Log("Увійшли в тригер " + other.name);
}
```
**Пояснення:** Для зіткнень потрібен хоча б один Rigidbody.  
**Практика:** Створити кулю з Rigidbody, куб зі стіною-колайдером; налаштувати тригер для підбору монети.  
**Типові помилки:** Відсутній Rigidbody → події OnCollision не спрацьовують; забули увімкнути `isTrigger`.

---

## Модуль 4. Базові ігрові механіки

### Урок 11. Рух персонажа
**Теорія:** Комбінація вводу, фізики та Transform.  
**Код-приклад (CharacterController):**
```csharp
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleMover : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0f, v).normalized;
        controller.SimpleMove(move * speed);
    }
}
```
**Пояснення:** `RequireComponent` гарантує наявність контролера. `SimpleMove` додає гравітацію.  
**Практика:** Додати камеру, що слідує за гравцем; обмежити рух по осі.  
**Типові помилки:** Забули компонент CharacterController; ігнорування нормалізації вектора руху → швидший рух по діагоналі.

### Урок 12. Просте підбирання предметів і система очок
**Теорія:** Тригери + лічильник.  
**Код-приклад:**
```csharp
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int value = 1;
}

public class PlayerCollector : MonoBehaviour
{
    public int score = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pickup pickup))
        {
            score += pickup.value;
            Destroy(other.gameObject);
            Debug.Log($"Очки: {score}");
        }
    }
}
```
**Пояснення:** `TryGetComponent` повертає `true`, якщо компонент знайдено, і одразу записує його в параметр `out`, тож окрема перевірка на `null` не потрібна.  
**Практика:** Розставити 10 монет, перевірити підрахунок очок.  
**Типові помилки:** Відсутній `isTrigger`; не додали колайдер гравцю.

---

## Модуль 5. ООП та структура коду (середній рівень)

### Урок 13. Класи та ООП
**Теорія:** Класи як шаблони, екземпляри, спадкування.  
**Код-приклад:**
```csharp
public class Enemy
{
    public string Name { get; private set; }
    public int Health { get; private set; }

    public Enemy(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void TakeDamage(int amount)
    {
        if (amount < 0) return; // ігноруємо від’ємні значення; для лікування використовуйте Heal нижче
        Health = Mathf.Max(0, Health - amount);
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;
        Health += amount; // за потреби обмежте максимумом
    }
}
```
**Пояснення:** Властивості з приватним сеттером для інкапсуляції; метод приймає лише додатне пошкодження, відновлення робиться окремим `Heal(int amount)`.  
**Практика:** Створити список ворогів, завдати шкоди першому.  
**Типові помилки:** Використання публічних полів замість властивостей без потреби.

### Урок 14. Інкапсуляція, public/private
**Теорія:** Контроль доступу, чому приховуємо дані. `[SerializeField] private` для Inspector.  
**Практика:** Переробити попередній код, приховати внутрішні змінні, дати методи взаємодії.  
**Типові помилки:** Логіка в `public` полях, що змінюються ззовні; зайвий `static`.

### Урок 15. Масиви та списки (List)
**Теорія:** `T[]` проти `List<T>`.  
**Код-приклад:**
```csharp
List<string> inventory = new List<string>();
inventory.Add("Key");
if (inventory.Contains("Key")) { /* ... */ }
```
**Практика:** Зробити інвентар списком; вивести елементи у Console.  
**Типові помилки:** Зміна колекції під час `foreach`; невидалені елементи → витік пам’яті.

---

## Модуль 6. ScriptableObject, події та робота між скриптами

### Урок 16. ScriptableObject для даних
**Теорія:** Дані, що зберігаються як асети, спільні між сценами.  
**Код-приклад:**
```csharp
using UnityEngine;

[CreateAssetMenu(menuName = "Config/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int maxHealth = 100;
    public float speed = 5f;
}
```
**Пояснення:** Створюємо асет через меню, не потребує GameObject.  
**Практика:** Створити асет PlayerStats, прочитати значення в скрипті руху.  
**Типові помилки:** Зміна полів у рантаймі прямо в асеті → впливає на всі екземпляри.

### Урок 17. Події та делегати
**Теорія:** `event` для сигналів між об’єктами; делегати як типи методів.  
**Код-приклад:**
```csharp
using System;
using UnityEngine;

public class ScoreEvents : MonoBehaviour
{
    public static event Action<int> OnScoreChanged;
    private int score;

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }
}
```
**Пояснення:** `?.Invoke` — null-safe виклик події, що захищає від `NullReferenceException`, коли немає підписників.  
**Код-приклад (підписник):**
```csharp
public class ScoreListener : MonoBehaviour
{
    void OnEnable()  => ScoreEvents.OnScoreChanged += HandleScoreChanged;
    void OnDisable() => ScoreEvents.OnScoreChanged -= HandleScoreChanged;
    void HandleScoreChanged(int value) { /* оновити UI або логіку */ }
}
```
**Практика:** Створити UI-текст, що оновлюється на подію.  
**Типові помилки:** Не відписалися → пам’ять/подвійні виклики; використання подій для дуже частих апдейтів (перевантаження).

### Урок 18. Робота з кількома скриптами
**Теорія:** Залежності через посилання в Inspector, пошук через `GetComponent`, Singleton для менеджерів (обережно).  
**Практика:** Гравець викликає метод у GameManager при завершенні рівня.  
**Типові помилки:** Зловживання `FindObjectOfType` у `Update`; циклічні залежності.

---

## Модуль 7. Менеджер гри та стани

### Урок 19. Прості стани (FSM)
**Теорія:** Машина станів для управління ігровим потоком: Menu, Playing, Paused, GameOver.  
**Код-приклад:**
```csharp
public enum GameState { Menu, Playing, Paused, GameOver }

public class GameManager : MonoBehaviour
{
    public GameState State { get; private set; } = GameState.Menu;

    public void SetState(GameState newState)
    {
        State = newState;
        // обробка переходів
    }
}
```
**Практика:** Кнопки UI міняють стан; у Paused зупиняємо `Time.timeScale = 0f`.  
**Типові помилки:** Забули повернути `timeScale` на 1; перемикання станів без перевірки поточного.

---

## Модуль 8. Робота з UI та збереженнями

### Урок 20. Canvas, Button, Text (TMP)
**Теорія:** Canvas рендерить UI, Button має подію OnClick, рекомендовано TextMeshPro.  
**Код-приклад:**
```csharp
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button restartButton;
    private int score;

    void Awake()
    {
        restartButton.onClick.AddListener(Restart);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = $"Очки: {score}";
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
```
**Практика:** Створити UI-лічильник очок і кнопку Restart, прив’язати методи.  
**Типові помилки:** Забули додати EventSystem; не під’єднали посилання на UI-елементи в Inspector.

### Урок 21. Збереження даних (PlayerPrefs)
**Теорія:** Просте ключ-значення сховище. Не для конфіденційних даних.  
**Код-приклад:**
```csharp
PlayerPrefs.SetInt("BestScore", score);
PlayerPrefs.Save();
int best = PlayerPrefs.GetInt("BestScore", 0);
```
**Практика:** Зберегти рекорд після завершення рівня, завантажити при старті.  
**Типові помилки:** Не викликали `Save` на мобільних; колізія ключів.

---

## Модуль 9. Підсумкові міні-проєкти

### Проєкт A. Збір монет
**Мета:** Рух персонажа, тригери, UI-рахунок.  
**Кроки:**  
1. Створити сцену з платформою й монетами.  
2. Скрипт руху з `CharacterController`.  
3. Скрипт `PlayerCollector` з подією `OnScoreChanged`.  
4. UI-текст, що слухає подію.  
**Типові помилки:** Не призначили шари для коректної фізики; не додали колайдер монетам.

### Проєкт B. Простий рівень з таймером
**Мета:** FSM + UI + збереження.  
**Кроки:**  
1. `GameManager` зі станами Menu/Playing/GameOver.  
2. Таймер у стані Playing; у GameOver показувати результат.  
3. Зберігати найкращий час через `PlayerPrefs`.  
**Типові помилки:** Таймер в `Update` без `deltaTime`; нескинутий стан при рестарті.

---

## Додаткові поради
- Коментуйте код коротко та по суті.  
- Тестуйте кожну зміну у Play Mode невеликими кроками.  
- Використовуйте префаби для повторюваних об’єктів.  
- Частіше зберігайте сцени та робіть резервні копії проєкту.

---

### Що далі?
Після проходження курсу студент уміє створювати прості 2D/3D прототипи, працювати з ввідом, фізикою, UI, подіями та базовим збереженням. Наступні кроки: анімація, навігація (NavMesh), Addressables, оптимізація під мобільні пристрої.
