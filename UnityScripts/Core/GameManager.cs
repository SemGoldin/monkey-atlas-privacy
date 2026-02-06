using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Main game manager that controls the overall game flow, level progression, and game states
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private int startingLives = 3;
    [SerializeField] private float levelStartDelay = 2f;
    [SerializeField] private float gameOverDelay = 3f;

    [Header("References")]
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform ghostContainer;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private MazeGenerator mazeGenerator;

    // Game State
    private GameState currentState;
    private int currentLevel = 1;
    private int lives;
    private List<GhostAI> ghosts = new List<GhostAI>();

    // Events
    public delegate void GameStateChangedHandler(GameState newState);
    public event GameStateChangedHandler OnGameStateChanged;

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

        InitializeGame();
    }

    private void Start()
    {
        StartGame();
    }

    private void InitializeGame()
    {
        lives = startingLives;
        currentLevel = 1;
        ChangeState(GameState.MainMenu);

        // Find all ghosts in the scene
        if (ghostContainer != null)
        {
            ghosts.AddRange(ghostContainer.GetComponentsInChildren<GhostAI>());
        }
    }

    public void StartGame()
    {
        ChangeState(GameState.Starting);
        StartCoroutine(StartLevelRoutine());
    }

    private IEnumerator StartLevelRoutine()
    {
        // Generate maze
        if (mazeGenerator != null)
        {
            mazeGenerator.GenerateMaze(currentLevel);
        }

        // Reset player position
        if (player != null)
        {
            player.ResetPosition();
        }

        // Reset ghosts
        foreach (var ghost in ghosts)
        {
            ghost.ResetGhost();
        }

        // Update UI
        if (uiManager != null)
        {
            uiManager.UpdateLives(lives);
            uiManager.ShowLevelStart(currentLevel);
        }

        yield return new WaitForSeconds(levelStartDelay);

        ChangeState(GameState.Playing);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        OnGameStateChanged?.Invoke(newState);

        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                EnableGameplay(true);
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                StartCoroutine(GameOverRoutine());
                break;

            case GameState.LevelComplete:
                StartCoroutine(LevelCompleteRoutine());
                break;
        }
    }

    private void EnableGameplay(bool enabled)
    {
        if (player != null)
        {
            player.enabled = enabled;
        }

        foreach (var ghost in ghosts)
        {
            ghost.enabled = enabled;
        }
    }

    public void OnPlayerDeath()
    {
        lives--;

        if (uiManager != null)
        {
            uiManager.UpdateLives(lives);
        }

        if (lives <= 0)
        {
            ChangeState(GameState.GameOver);
        }
        else
        {
            StartCoroutine(RespawnPlayerRoutine());
        }
    }

    private IEnumerator RespawnPlayerRoutine()
    {
        EnableGameplay(false);
        yield return new WaitForSeconds(2f);

        // Reset player and ghosts
        if (player != null)
        {
            player.ResetPosition();
        }

        foreach (var ghost in ghosts)
        {
            ghost.ResetGhost();
        }

        EnableGameplay(true);
        ChangeState(GameState.Playing);
    }

    private IEnumerator GameOverRoutine()
    {
        EnableGameplay(false);

        if (uiManager != null)
        {
            uiManager.ShowGameOver();
        }

        yield return new WaitForSeconds(gameOverDelay);

        // Reset game
        lives = startingLives;
        currentLevel = 1;
        ScoreManager.Instance?.ResetScore();

        if (uiManager != null)
        {
            uiManager.ShowMainMenu();
        }

        ChangeState(GameState.MainMenu);
    }

    public void OnAllPelletsCollected()
    {
        ChangeState(GameState.LevelComplete);
    }

    private IEnumerator LevelCompleteRoutine()
    {
        EnableGameplay(false);

        if (uiManager != null)
        {
            uiManager.ShowLevelComplete(currentLevel);
        }

        yield return new WaitForSeconds(levelStartDelay);

        currentLevel++;
        StartCoroutine(StartLevelRoutine());
    }

    public void PauseGame()
    {
        if (currentState == GameState.Playing)
        {
            ChangeState(GameState.Paused);

            if (uiManager != null)
            {
                uiManager.ShowPauseMenu();
            }
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(GameState.Playing);

            if (uiManager != null)
            {
                uiManager.HidePauseMenu();
            }
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // Getters
    public GameState GetCurrentState() => currentState;
    public int GetCurrentLevel() => currentLevel;
    public int GetLives() => lives;
    public List<GhostAI> GetGhosts() => ghosts;
}
