using UnityEngine;

/// <summary>
/// PlayerController - Клас управління гравцем
/// 
/// Опис: Відповідає за обробку введення користувача та управління рухом гравця.
/// Взаємодіє з фізичним двигуном Unity для реалізації руху та стрибків.
/// 
/// Основні функції:
/// - Обробка клавіатурного та геймпадного введення
/// - Управління горизонтальним рухом персонажа
/// - Реалізація механіки стрибків
/// - Перевірка стану гравця (на землі чи в повітрі)
/// - Взаємодія з системою анімації
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Швидкість руху гравця
    /// </summary>
    [Header("Налаштування руху")]
    [SerializeField]
    [Tooltip("Швидкість горизонтального руху гравця")]
    private float moveSpeed = 5f;
    
    /// <summary>
    /// Сила стрибка
    /// </summary>
    [SerializeField]
    [Tooltip("Сила, що застосовується при стрибку")]
    private float jumpForce = 10f;
    
    /// <summary>
    /// Посилання на компонент Rigidbody2D для фізики
    /// </summary>
    private Rigidbody2D rb;
    
    /// <summary>
    /// Чи знаходиться гравець на землі
    /// </summary>
    private bool isGrounded = false;
    
    /// <summary>
    /// Шар для перевірки землі
    /// </summary>
    [Header("Перевірка землі")]
    [SerializeField]
    [Tooltip("Шар, який вважається землею")]
    private LayerMask groundLayer;
    
    /// <summary>
    /// Точка для перевірки, чи на землі гравець
    /// </summary>
    [SerializeField]
    [Tooltip("Трансформ для перевірки контакту з землею")]
    private Transform groundCheck;
    
    /// <summary>
    /// Радіус перевірки землі
    /// </summary>
    [SerializeField]
    [Tooltip("Радіус перевірки контакту з землею")]
    private float groundCheckRadius = 0.2f;
    
    /// <summary>
    /// Поточне здоров'я гравця
    /// </summary>
    [Header("Характеристики гравця")]
    [SerializeField]
    [Tooltip("Поточне здоров'я гравця")]
    private int health = 100;
    
    /// <summary>
    /// Максимальне здоров'я гравця
    /// </summary>
    [SerializeField]
    [Tooltip("Максимальне здоров'я гравця")]
    private int maxHealth = 100;
    
    /// <summary>
    /// Ініціалізація компонентів
    /// </summary>
    private void Start()
    {
        // Отримання компоненту Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("PlayerController: Rigidbody2D не знайдено!");
        }
    }
    
    /// <summary>
    /// Оновлення кожен кадр для обробки введення
    /// </summary>
    private void Update()
    {
        // Перевірка, чи гравець на землі
        CheckGrounded();
        
        // Обробка введення для стрибка
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }
    
    /// <summary>
    /// Фізичне оновлення для руху
    /// </summary>
    private void FixedUpdate()
    {
        // Отримання горизонтального введення
        float moveInput = Input.GetAxis("Horizontal");
        
        // Застосування руху
        Move(moveInput);
    }
    
    /// <summary>
    /// Рух гравця по горизонталі
    /// </summary>
    /// <param name="direction">Напрямок руху (-1 для лівого, 1 для правого)</param>
    private void Move(float direction)
    {
        // Обчислення нової швидкості
        Vector2 velocity = rb.velocity;
        velocity.x = direction * moveSpeed;
        rb.velocity = velocity;
        
        // Поворот спрайту в напрямку руху
        if (direction != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
        }
    }
    
    /// <summary>
    /// Виконання стрибка
    /// </summary>
    private void Jump()
    {
        // Додавання сили вгору для стрибка
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        Debug.Log("Гравець стрибнув!");
    }
    
    /// <summary>
    /// Перевірка, чи гравець на землі
    /// </summary>
    private void CheckGrounded()
    {
        if (groundCheck != null)
        {
            // Використання OverlapCircle для перевірки колізії з землею
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }
    }
    
    /// <summary>
    /// Отримання урону гравцем
    /// </summary>
    /// <param name="damage">Кількість отриманого урону</param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Max(0, health); // Переконуємось, що здоров'я не менше 0
        
        Debug.Log($"Гравець отримав {damage} урону. Залишилось здоров'я: {health}");
        
        if (health <= 0)
        {
            Die();
        }
    }
    
    /// <summary>
    /// Лікування гравця
    /// </summary>
    /// <param name="amount">Кількість відновлюваного здоров'я</param>
    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Min(health, maxHealth); // Переконуємось, що здоров'я не перевищує максимум
        
        Debug.Log($"Гравець вилікував {amount} здоров'я. Поточне здоров'я: {health}");
    }
    
    /// <summary>
    /// Смерть гравця
    /// </summary>
    private void Die()
    {
        Debug.Log("Гравець помер!");
        
        // Повідомляємо GameManager про втрату життя
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife();
        }
        
        // Респавн або інша логіка
        Respawn();
    }
    
    /// <summary>
    /// Відродження гравця
    /// </summary>
    private void Respawn()
    {
        health = maxHealth;
        transform.position = Vector3.zero; // Повернення на початкову позицію
        Debug.Log("Гравець відроджений!");
    }
    
    /// <summary>
    /// Отримання поточного здоров'я
    /// </summary>
    /// <returns>Поточне значення здоров'я</returns>
    public int GetHealth()
    {
        return health;
    }
    
    /// <summary>
    /// Отримання максимального здоров'я
    /// </summary>
    /// <returns>Максимальне значення здоров'я</returns>
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    
    /// <summary>
    /// Візуалізація в редакторі Unity
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
