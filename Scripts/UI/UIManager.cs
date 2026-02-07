using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UIManager - Клас управління користувацьким інтерфейсом
/// 
/// Опис: Відповідає за відображення та оновлення всіх елементів UI в грі,
/// включаючи рахунок, життя, здоров'я та різні меню. Взаємодіє з GameManager
/// для отримання актуальних даних про стан гри.
/// 
/// Основні функції:
/// - Відображення рахунку гравця
/// - Відображення життів та здоров'я
/// - Управління меню (головне меню, меню паузи, екран завершення)
/// - Оновлення UI в реальному часі
/// - Обробка кліків на кнопки інтерфейсу
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Текстове поле для відображення рахунку
    /// </summary>
    [Header("Ігрові елементи UI")]
    [SerializeField]
    [Tooltip("Текст для відображення поточного рахунку")]
    private TextMeshProUGUI scoreText;
    
    /// <summary>
    /// Текстове поле для відображення життів
    /// </summary>
    [SerializeField]
    [Tooltip("Текст для відображення кількості життів")]
    private TextMeshProUGUI livesText;
    
    /// <summary>
    /// Слайдер для відображення здоров'я
    /// </summary>
    [SerializeField]
    [Tooltip("Слайдер здоров'я гравця")]
    private Slider healthSlider;
    
    /// <summary>
    /// Текст рівня
    /// </summary>
    [SerializeField]
    [Tooltip("Текст для відображення поточного рівня")]
    private TextMeshProUGUI levelText;
    
    /// <summary>
    /// Панель головного меню
    /// </summary>
    [Header("Меню")]
    [SerializeField]
    [Tooltip("Панель головного меню")]
    private GameObject mainMenuPanel;
    
    /// <summary>
    /// Панель меню паузи
    /// </summary>
    [SerializeField]
    [Tooltip("Панель меню паузи")]
    private GameObject pauseMenuPanel;
    
    /// <summary>
    /// Панель екрану завершення гри
    /// </summary>
    [SerializeField]
    [Tooltip("Панель екрану Game Over")]
    private GameObject gameOverPanel;
    
    /// <summary>
    /// Текст фінального рахунку
    /// </summary>
    [SerializeField]
    [Tooltip("Текст фінального рахунку на екрані Game Over")]
    private TextMeshProUGUI finalScoreText;
    
    /// <summary>
    /// Панель HUD (постійні елементи інтерфейсу)
    /// </summary>
    [SerializeField]
    [Tooltip("Панель з основними ігровими елементами")]
    private GameObject hudPanel;
    
    /// <summary>
    /// Посилання на PlayerController для відстеження здоров'я
    /// </summary>
    private PlayerController playerController;
    
    /// <summary>
    /// Ініціалізація
    /// </summary>
    private void Start()
    {
        // Знаходження гравця
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
        
        // Початкова ініціалізація UI
        ShowMainMenu();
    }
    
    /// <summary>
    /// Оновлення UI кожен кадр
    /// </summary>
    private void Update()
    {
        // Оновлення ігрових елементів UI якщо гра йде
        if (GameManager.Instance != null && 
            GameManager.Instance.GetCurrentState() == GameManager.GameState.Playing)
        {
            UpdateGameUI();
        }
        
        // Перевірка натискання клавіші паузи
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    /// <summary>
    /// Оновлення ігрових елементів інтерфейсу
    /// </summary>
    private void UpdateGameUI()
    {
        // Оновлення рахунку
        if (scoreText != null && GameManager.Instance != null)
        {
            scoreText.text = $"Рахунок: {GameManager.Instance.GetScore()}";
        }
        
        // Оновлення життів
        if (livesText != null && GameManager.Instance != null)
        {
            livesText.text = $"Життя: {GameManager.Instance.GetLives()}";
        }
        
        // Оновлення здоров'я
        if (healthSlider != null && playerController != null)
        {
            healthSlider.value = (float)playerController.GetHealth() / playerController.GetMaxHealth();
        }
    }
    
    /// <summary>
    /// Показати головне меню
    /// </summary>
    public void ShowMainMenu()
    {
        SetActivePanel(mainMenuPanel);
        Time.timeScale = 0f; // Зупинка гри
    }
    
    /// <summary>
    /// Показати HUD
    /// </summary>
    public void ShowHUD()
    {
        SetActivePanel(hudPanel);
        Time.timeScale = 1f; // Відновлення гри
    }
    
    /// <summary>
    /// Показати меню паузи
    /// </summary>
    public void ShowPauseMenu()
    {
        SetActivePanel(pauseMenuPanel);
        Time.timeScale = 0f; // Зупинка гри
    }
    
    /// <summary>
    /// Показати екран Game Over
    /// </summary>
    public void ShowGameOver()
    {
        SetActivePanel(gameOverPanel);
        
        // Відображення фінального рахунку
        if (finalScoreText != null && GameManager.Instance != null)
        {
            finalScoreText.text = $"Фінальний рахунок: {GameManager.Instance.GetScore()}";
        }
        
        Time.timeScale = 0f;
    }
    
    /// <summary>
    /// Встановити активну панель (деактивує всі інші)
    /// </summary>
    /// <param name="panelToActivate">Панель для активації</param>
    private void SetActivePanel(GameObject panelToActivate)
    {
        // Деактивація всіх панелей
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (hudPanel != null) hudPanel.SetActive(false);
        
        // Активація вибраної панелі
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
    }
    
    /// <summary>
    /// Перемикання паузи
    /// </summary>
    private void TogglePause()
    {
        if (GameManager.Instance == null)
            return;
        
        if (GameManager.Instance.GetCurrentState() == GameManager.GameState.Playing)
        {
            ShowPauseMenu();
            GameManager.Instance.SetPause(true);
        }
        else if (GameManager.Instance.GetCurrentState() == GameManager.GameState.Paused)
        {
            ResumeGame();
        }
    }
    
    // ============ Методи для кнопок UI ============
    
    /// <summary>
    /// Обробник кнопки "Нова гра"
    /// </summary>
    public void OnNewGameButton()
    {
        Debug.Log("Початок нової гри");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartNewGame();
        }
        
        ShowHUD();
    }
    
    /// <summary>
    /// Обробник кнопки "Продовжити"
    /// </summary>
    public void OnResumeButton()
    {
        ResumeGame();
    }
    
    /// <summary>
    /// Продовження гри
    /// </summary>
    private void ResumeGame()
    {
        Debug.Log("Продовження гри");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetPause(false);
        }
        
        ShowHUD();
    }
    
    /// <summary>
    /// Обробник кнопки "Перезапустити"
    /// </summary>
    public void OnRestartButton()
    {
        Debug.Log("Перезапуск гри");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartNewGame();
        }
        
        ShowHUD();
    }
    
    /// <summary>
    /// Обробник кнопки "Головне меню"
    /// </summary>
    public void OnMainMenuButton()
    {
        Debug.Log("Повернення в головне меню");
        ShowMainMenu();
    }
    
    /// <summary>
    /// Обробник кнопки "Вихід"
    /// </summary>
    public void OnQuitButton()
    {
        Debug.Log("Вихід з гри");
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
    /// <summary>
    /// Показати повідомлення на екрані
    /// </summary>
    /// <param name="message">Текст повідомлення</param>
    /// <param name="duration">Тривалість відображення</param>
    public void ShowMessage(string message, float duration = 3f)
    {
        Debug.Log($"Повідомлення: {message}");
        // Тут можна додати логіку для відображення тимчасового повідомлення
        // наприклад, через Coroutine або тимчасовий текстовий об'єкт
    }
}
