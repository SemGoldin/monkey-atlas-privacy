using UnityEngine;

/// <summary>
/// EnemyAI - Клас штучного інтелекту ворога
/// 
/// Опис: Керує поведінкою ворожих персонажів в грі. Реалізує різні стани 
/// поведінки (патрулювання, переслідування, атака) та перемикання між ними
/// залежно від ситуації.
/// 
/// Основні функції:
/// - Патрулювання між заданими точками
/// - Виявлення гравця в радіусі огляду
/// - Переслідування гравця
/// - Атака гравця при наближенні
/// - Управління здоров'ям ворога
/// </summary>
public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Стани поведінки ворога
    /// </summary>
    public enum EnemyState
    {
        Patrol,      // Патрулювання
        Chase,       // Переслідування
        Attack,      // Атака
        Dead         // Мертвий
    }
    
    /// <summary>
    /// Поточний стан ворога
    /// </summary>
    private EnemyState currentState = EnemyState.Patrol;
    
    /// <summary>
    /// Здоров'я ворога
    /// </summary>
    [Header("Характеристики")]
    [SerializeField]
    [Tooltip("Здоров'я ворога")]
    private int health = 50;
    
    /// <summary>
    /// Урон, який наносить ворог
    /// </summary>
    [SerializeField]
    [Tooltip("Кількість урону, що наноситься гравцю")]
    private int damage = 10;
    
    /// <summary>
    /// Очки, які отримує гравець за знищення ворога
    /// </summary>
    [SerializeField]
    [Tooltip("Очки за знищення цього ворога")]
    private int scoreValue = 100;
    
    /// <summary>
    /// Швидкість патрулювання
    /// </summary>
    [Header("Налаштування руху")]
    [SerializeField]
    [Tooltip("Швидкість під час патрулювання")]
    private float patrolSpeed = 2f;
    
    /// <summary>
    /// Швидкість переслідування
    /// </summary>
    [SerializeField]
    [Tooltip("Швидкість під час переслідування гравця")]
    private float chaseSpeed = 4f;
    
    /// <summary>
    /// Точки патрулювання
    /// </summary>
    [SerializeField]
    [Tooltip("Масив точок для патрулювання")]
    private Transform[] patrolPoints;
    
    /// <summary>
    /// Поточний індекс точки патрулювання
    /// </summary>
    private int currentPatrolIndex = 0;
    
    /// <summary>
    /// Радіус виявлення гравця
    /// </summary>
    [Header("Виявлення")]
    [SerializeField]
    [Tooltip("Відстань, на якій ворог помічає гравця")]
    private float detectionRange = 5f;
    
    /// <summary>
    /// Радіус атаки
    /// </summary>
    [SerializeField]
    [Tooltip("Відстань атаки")]
    private float attackRange = 1.5f;
    
    /// <summary>
    /// Затримка між атаками
    /// </summary>
    [SerializeField]
    [Tooltip("Час між атаками в секундах")]
    private float attackCooldown = 1f;
    
    /// <summary>
    /// Таймер для відліку часу між атаками
    /// </summary>
    private float attackTimer = 0f;
    
    /// <summary>
    /// Посилання на трансформ гравця
    /// </summary>
    private Transform playerTransform;
    
    /// <summary>
    /// Ініціалізація
    /// </summary>
    private void Start()
    {
        // Знаходження гравця в сцені
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("EnemyAI: Гравець не знайдений в сцені!");
        }
    }
    
    /// <summary>
    /// Оновлення кожен кадр
    /// </summary>
    private void Update()
    {
        if (currentState == EnemyState.Dead)
            return;
        
        // Оновлення таймеру атаки
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        
        // Виконання поведінки залежно від стану
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                CheckForPlayer();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                CheckAttackRange();
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break;
        }
    }
    
    /// <summary>
    /// Патрулювання між точками
    /// </summary>
    private void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;
        
        // Рух до поточної точки патрулювання
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * patrolSpeed * Time.deltaTime;
        
        // Перевірка досягнення точки
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Перехід до наступної точки
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
    
    /// <summary>
    /// Перевірка наявності гравця в радіусі виявлення
    /// </summary>
    private void CheckForPlayer()
    {
        if (playerTransform == null)
            return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            currentState = EnemyState.Chase;
            Debug.Log("Ворог виявив гравця!");
        }
    }
    
    /// <summary>
    /// Переслідування гравця
    /// </summary>
    private void ChasePlayer()
    {
        if (playerTransform == null)
        {
            currentState = EnemyState.Patrol;
            return;
        }
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        // Якщо гравець вийшов за межі виявлення, повертаємось до патрулювання
        if (distanceToPlayer > detectionRange * 1.5f)
        {
            currentState = EnemyState.Patrol;
            return;
        }
        
        // Рух до гравця
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * chaseSpeed * Time.deltaTime;
        
        // Поворот у напрямку гравця
        if (direction.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
        }
    }
    
    /// <summary>
    /// Перевірка, чи гравець в радіусі атаки
    /// </summary>
    private void CheckAttackRange()
    {
        if (playerTransform == null)
            return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
    }
    
    /// <summary>
    /// Атака гравця
    /// </summary>
    private void AttackPlayer()
    {
        if (playerTransform == null)
        {
            currentState = EnemyState.Patrol;
            return;
        }
        
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        // Якщо гравець вийшов з радіусу атаки, продовжуємо переслідування
        if (distanceToPlayer > attackRange)
        {
            currentState = EnemyState.Chase;
            return;
        }
        
        // Атакуємо, якщо cooldown минув
        if (attackTimer <= 0)
        {
            PerformAttack();
            attackTimer = attackCooldown;
        }
    }
    
    /// <summary>
    /// Виконання атаки
    /// </summary>
    private void PerformAttack()
    {
        Debug.Log($"Ворог атакує! Урон: {damage}");
        
        // Знаходження компоненту PlayerController та нанесення урону
        PlayerController player = playerTransform.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
    
    /// <summary>
    /// Отримання урону ворогом
    /// </summary>
    /// <param name="damageAmount">Кількість урону</param>
    public void TakeDamage(int damageAmount)
    {
        if (currentState == EnemyState.Dead)
            return;
        
        health -= damageAmount;
        Debug.Log($"Ворог отримав {damageAmount} урону. Залишилось здоров'я: {health}");
        
        if (health <= 0)
        {
            Die();
        }
    }
    
    /// <summary>
    /// Смерть ворога
    /// </summary>
    private void Die()
    {
        currentState = EnemyState.Dead;
        Debug.Log("Ворог знищений!");
        
        // Додавання очків гравцю
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }
        
        // Знищення об'єкту ворога
        Destroy(gameObject, 0.5f);
    }
    
    /// <summary>
    /// Візуалізація радіусів в редакторі
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // Радіус виявлення
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // Радіус атаки
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        // Точки патрулювання
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            Gizmos.color = Color.blue;
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                if (patrolPoints[i] != null)
                {
                    Gizmos.DrawWireSphere(patrolPoints[i].position, 0.3f);
                    
                    // Лінії між точками
                    if (i < patrolPoints.Length - 1 && patrolPoints[i + 1] != null)
                    {
                        Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                    }
                }
            }
        }
    }
}
