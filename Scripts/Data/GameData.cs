using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// GameData - Клас для зберігання даних гри
/// 
/// Опис: Відповідає за зберігання, завантаження та управління даними гри,
/// включаючи налаштування, прогрес гравця, рекорди та статистику.
/// Використовує PlayerPrefs або серіалізацію для збереження даних.
/// 
/// Основні функції:
/// - Збереження та завантаження прогресу гри
/// - Управління найкращими рекордами
/// - Збереження налаштувань (звук, графіка, управління)
/// - Статистика гравця
/// - Серіалізація/десеріалізація даних
/// </summary>
[Serializable]
public class GameData
{
    /// <summary>
    /// Найкращий рахунок гравця
    /// </summary>
    public int highScore = 0;
    
    /// <summary>
    /// Останній досягнутий рівень
    /// </summary>
    public int lastLevel = 1;
    
    /// <summary>
    /// Загальна кількість зібраних монет
    /// </summary>
    public int totalCoinsCollected = 0;
    
    /// <summary>
    /// Загальна кількість знищених ворогів
    /// </summary>
    public int totalEnemiesDefeated = 0;
    
    /// <summary>
    /// Загальний час гри в секундах
    /// </summary>
    public float totalPlayTime = 0f;
    
    /// <summary>
    /// Гучність музики (0-1)
    /// </summary>
    public float musicVolume = 0.7f;
    
    /// <summary>
    /// Гучність звукових ефектів (0-1)
    /// </summary>
    public float sfxVolume = 0.8f;
    
    /// <summary>
    /// Список пройдених рівнів
    /// </summary>
    public List<int> completedLevels = new List<int>();
    
    /// <summary>
    /// Дата останнього збереження
    /// </summary>
    public string lastSaveDate = "";
    
    /// <summary>
    /// Конструктор за замовчуванням
    /// </summary>
    public GameData()
    {
        // Ініціалізація значень за замовчуванням
        highScore = 0;
        lastLevel = 1;
        totalCoinsCollected = 0;
        totalEnemiesDefeated = 0;
        totalPlayTime = 0f;
        musicVolume = 0.7f;
        sfxVolume = 0.8f;
        completedLevels = new List<int>();
        lastSaveDate = DateTime.Now.ToString();
    }
}

/// <summary>
/// GameDataManager - Менеджер для роботи з даними гри
/// 
/// Опис: Singleton клас, який керує збереженням та завантаженням даних гри.
/// Забезпечує центральну точку доступу до всіх даних гри.
/// </summary>
public class GameDataManager : MonoBehaviour
{
    /// <summary>
    /// Singleton екземпляр
    /// </summary>
    private static GameDataManager instance;
    
    /// <summary>
    /// Публічний доступ до екземпляру
    /// </summary>
    public static GameDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameDataManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameDataManager");
                    instance = go.AddComponent<GameDataManager>();
                }
            }
            return instance;
        }
    }
    
    /// <summary>
    /// Поточні дані гри
    /// </summary>
    private GameData currentData;
    
    /// <summary>
    /// Ключ для збереження в PlayerPrefs
    /// </summary>
    private const string SAVE_KEY = "GameData";
    
    /// <summary>
    /// Ініціалізація
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    /// <summary>
    /// Збереження гри
    /// </summary>
    public void SaveGame()
    {
        if (currentData == null)
        {
            Debug.LogWarning("Немає даних для збереження!");
            return;
        }
        
        // Оновлення дати збереження
        currentData.lastSaveDate = DateTime.Now.ToString();
        
        // Конвертація даних в JSON
        string jsonData = JsonUtility.ToJson(currentData, true);
        
        // Збереження через PlayerPrefs
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
        PlayerPrefs.Save();
        
        Debug.Log("Гру збережено успішно!");
    }
    
    /// <summary>
    /// Завантаження гри
    /// </summary>
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            // Завантаження JSON з PlayerPrefs
            string jsonData = PlayerPrefs.GetString(SAVE_KEY);
            
            // Десеріалізація JSON в об'єкт GameData
            currentData = JsonUtility.FromJson<GameData>(jsonData);
            
            Debug.Log("Гру завантажено успішно!");
        }
        else
        {
            // Створення нових даних, якщо збережень не знайдено
            Debug.Log("Збережень не знайдено. Створення нових даних.");
            currentData = new GameData();
        }
    }
    
    /// <summary>
    /// Скидання всіх даних
    /// </summary>
    public void ResetGame()
    {
        currentData = new GameData();
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
        Debug.Log("Всі дані гри скинуті!");
    }
    
    /// <summary>
    /// Отримання поточних даних
    /// </summary>
    /// <returns>Об'єкт GameData</returns>
    public GameData GetGameData()
    {
        if (currentData == null)
        {
            LoadGame();
        }
        return currentData;
    }
    
    /// <summary>
    /// Оновлення найкращого рахунку
    /// </summary>
    /// <param name="score">Новий рахунок</param>
    public void UpdateHighScore(int score)
    {
        if (score > currentData.highScore)
        {
            currentData.highScore = score;
            Debug.Log($"Новий рекорд: {score}!");
            SaveGame();
        }
    }
    
    /// <summary>
    /// Додавання зібраної монети
    /// </summary>
    public void AddCoin()
    {
        currentData.totalCoinsCollected++;
    }
    
    /// <summary>
    /// Додавання знищеного ворога
    /// </summary>
    public void AddEnemyDefeated()
    {
        currentData.totalEnemiesDefeated++;
    }
    
    /// <summary>
    /// Оновлення часу гри
    /// </summary>
    /// <param name="deltaTime">Час, що пройшов</param>
    public void UpdatePlayTime(float deltaTime)
    {
        currentData.totalPlayTime += deltaTime;
    }
    
    /// <summary>
    /// Позначити рівень як пройдений
    /// </summary>
    /// <param name="levelNumber">Номер рівня</param>
    public void CompleteLevel(int levelNumber)
    {
        if (!currentData.completedLevels.Contains(levelNumber))
        {
            currentData.completedLevels.Add(levelNumber);
            Debug.Log($"Рівень {levelNumber} пройдений!");
            SaveGame();
        }
    }
    
    /// <summary>
    /// Перевірка, чи рівень пройдений
    /// </summary>
    /// <param name="levelNumber">Номер рівня</param>
    /// <returns>True, якщо рівень пройдений</returns>
    public bool IsLevelCompleted(int levelNumber)
    {
        return currentData.completedLevels.Contains(levelNumber);
    }
    
    /// <summary>
    /// Встановлення гучності музики
    /// </summary>
    /// <param name="volume">Значення гучності (0-1)</param>
    public void SetMusicVolume(float volume)
    {
        currentData.musicVolume = Mathf.Clamp01(volume);
        SaveGame();
    }
    
    /// <summary>
    /// Встановлення гучності звукових ефектів
    /// </summary>
    /// <param name="volume">Значення гучності (0-1)</param>
    public void SetSFXVolume(float volume)
    {
        currentData.sfxVolume = Mathf.Clamp01(volume);
        SaveGame();
    }
    
    /// <summary>
    /// Отримання статистики гри
    /// </summary>
    /// <returns>Рядок зі статистикою</returns>
    public string GetStatistics()
    {
        return $"Статистика гри:\n" +
               $"Найкращий рахунок: {currentData.highScore}\n" +
               $"Останній рівень: {currentData.lastLevel}\n" +
               $"Зібрано монет: {currentData.totalCoinsCollected}\n" +
               $"Знищено ворогів: {currentData.totalEnemiesDefeated}\n" +
               $"Час гри: {FormatTime(currentData.totalPlayTime)}\n" +
               $"Пройдено рівнів: {currentData.completedLevels.Count}";
    }
    
    /// <summary>
    /// Форматування часу в читабельний вигляд
    /// </summary>
    /// <param name="timeInSeconds">Час в секундах</param>
    /// <returns>Відформатований рядок</returns>
    private string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        
        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }
    
    /// <summary>
    /// Автоматичне збереження при виході
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    
    /// <summary>
    /// Автоматичне збереження при паузі (для мобільних пристроїв)
    /// </summary>
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGame();
        }
    }
}
