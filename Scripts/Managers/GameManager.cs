using UnityEngine;

/// <summary>
/// GameManager - Головний клас управління грою (Singleton Pattern)
/// 
/// Опис: Центральний контролер гри, який керує станом гри, рівнями та координує
/// взаємодію між різними системами. Використовує патерн Singleton для забезпечення
/// єдиного екземпляру в грі.
/// 
/// Основні функції:
/// - Управління станом гри (меню, гра, пауза, завершення)
/// - Відстеження рахунку гравця
/// - Управління життями гравця
/// - Завантаження та перезапуск рівнів
/// - Координація між різними менеджерами (UI, звук, тощо)
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Статичний екземпляр для доступу до GameManager з будь-якого місця в коді
    /// </summary>
    private static GameManager instance;
    
    /// <summary>
    /// Публічне властивість для отримання екземпляру GameManager
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    
    /// <summary>
    /// Поточний стан гри
    /// </summary>
    public enum GameState
    {
        MainMenu,    // Головне меню
        Playing,     // Гра в процесі
        Paused,      // Гра на паузі
        GameOver     // Кінець гри
    }
    
    /// <summary>
    /// Поточний стан гри
    /// </summary>
    private GameState currentState = GameState.MainMenu;
    
    /// <summary>
    /// Поточний рахунок гравця
    /// </summary>
    private int score = 0;
    
    /// <summary>
    /// Поточна кількість життів гравця
    /// </summary>
    private int lives = 3;
    
    /// <summary>
    /// Поточний рівень
    /// </summary>
    private int currentLevel = 1;
    
    /// <summary>
    /// Ініціалізація при створенні об'єкту
    /// </summary>
    private void Awake()
    {
        // Забезпечення єдиного екземпляру GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// Запуск нової гри
    /// </summary>
    public void StartNewGame()
    {
        score = 0;
        lives = 3;
        currentLevel = 1;
        currentState = GameState.Playing;
        Debug.Log("Нова гра розпочата!");
    }
    
    /// <summary>
    /// Додавання очків до рахунку
    /// </summary>
    /// <param name="points">Кількість очків для додавання</param>
    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Рахунок: {score}");
    }
    
    /// <summary>
    /// Отримання поточного рахунку
    /// </summary>
    /// <returns>Поточний рахунок гравця</returns>
    public int GetScore()
    {
        return score;
    }
    
    /// <summary>
    /// Зменшення кількості життів гравця
    /// </summary>
    public void LoseLife()
    {
        lives--;
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            Debug.Log($"Залишилось життів: {lives}");
        }
    }
    
    /// <summary>
    /// Отримання поточної кількості життів
    /// </summary>
    /// <returns>Поточна кількість життів</returns>
    public int GetLives()
    {
        return lives;
    }
    
    /// <summary>
    /// Встановлення паузи в грі
    /// </summary>
    /// <param name="pause">True - увімкнути паузу, False - вимкнути</param>
    public void SetPause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;
            currentState = GameState.Paused;
            Debug.Log("Гра на паузі");
        }
        else
        {
            Time.timeScale = 1f;
            currentState = GameState.Playing;
            Debug.Log("Гра продовжена");
        }
    }
    
    /// <summary>
    /// Завершення гри
    /// </summary>
    private void GameOver()
    {
        currentState = GameState.GameOver;
        Time.timeScale = 0f;
        Debug.Log($"Кінець гри! Фінальний рахунок: {score}");
    }
    
    /// <summary>
    /// Перехід на наступний рівень
    /// </summary>
    public void LoadNextLevel()
    {
        currentLevel++;
        Debug.Log($"Завантаження рівня {currentLevel}");
    }
    
    /// <summary>
    /// Отримання поточного стану гри
    /// </summary>
    /// <returns>Поточний стан гри</returns>
    public GameState GetCurrentState()
    {
        return currentState;
    }
}
