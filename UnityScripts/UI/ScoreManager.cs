using UnityEngine;

/// <summary>
/// Manages player score, high score, and score-related events
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Score Settings")]
    [SerializeField] private int pelletScore = 10;
    [SerializeField] private int powerPelletScore = 50;
    [SerializeField] private int ghostBaseScore = 200;
    [SerializeField] private int ghostScoreMultiplier = 2;

    private int currentScore = 0;
    private int highScore = 0;
    private int currentGhostStreak = 0;
    private const string HIGH_SCORE_KEY = "HighScore";

    // Events
    public delegate void ScoreChangedHandler(int newScore);
    public event ScoreChangedHandler OnScoreChanged;

    public delegate void HighScoreChangedHandler(int newHighScore);
    public event HighScoreChangedHandler OnHighScoreChanged;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadHighScore();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        OnHighScoreChanged?.Invoke(highScore);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
        PlayerPrefs.Save();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        OnScoreChanged?.Invoke(currentScore);

        // Check for new high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
            OnHighScoreChanged?.Invoke(highScore);
        }
    }

    public void AddPelletScore()
    {
        AddScore(pelletScore);
    }

    public void AddPowerPelletScore()
    {
        AddScore(powerPelletScore);
        ResetGhostStreak();
    }

    public void AddGhostScore()
    {
        // Each ghost eaten in succession is worth more
        int ghostScore = ghostBaseScore * (int)Mathf.Pow(ghostScoreMultiplier, currentGhostStreak);
        AddScore(ghostScore);
        currentGhostStreak++;
    }

    public void ResetGhostStreak()
    {
        currentGhostStreak = 0;
    }

    public void ResetScore()
    {
        currentScore = 0;
        currentGhostStreak = 0;
        OnScoreChanged?.Invoke(currentScore);
    }

    // Getters
    public int GetCurrentScore() => currentScore;
    public int GetHighScore() => highScore;
}
