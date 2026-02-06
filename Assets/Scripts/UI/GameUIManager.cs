using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI movesText;
    [SerializeField] private Image[] starImages;
    
    [Header("Level Complete Panel")]
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private TextMeshProUGUI completeMessageText;
    [SerializeField] private TextMeshProUGUI completeMoves;
    [SerializeField] private Image[] completeStarImages;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;
    
    [Header("Pause Panel")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseRestartButton;
    [SerializeField] private Button pauseMenuButton;
    
    [Header("Settings")]
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle effectsToggle;
    
    [Header("Colors")]
    [SerializeField] private Color starActiveColor = Color.yellow;
    [SerializeField] private Color starInactiveColor = Color.gray;
    
    private void Start()
    {
        SetupButtons();
        SetupToggles();
        
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
        
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
    
    private void SetupButtons()
    {
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(OnNextLevel);
        
        if (restartButton != null)
            restartButton.onClick.AddListener(OnRestart);
        
        if (menuButton != null)
            menuButton.onClick.AddListener(OnMenu);
        
        if (resumeButton != null)
            resumeButton.onClick.AddListener(OnResume);
        
        if (pauseRestartButton != null)
            pauseRestartButton.onClick.AddListener(OnRestart);
        
        if (pauseMenuButton != null)
            pauseMenuButton.onClick.AddListener(OnMenu);
    }
    
    private void SetupToggles()
    {
        if (soundToggle != null)
        {
            soundToggle.isOn = AudioManager.Instance.IsSoundEnabled();
            soundToggle.onValueChanged.AddListener(OnSoundToggle);
        }
        
        if (musicToggle != null)
        {
            musicToggle.isOn = AudioManager.Instance.IsMusicEnabled();
            musicToggle.onValueChanged.AddListener(OnMusicToggle);
        }
        
        if (effectsToggle != null)
        {
            effectsToggle.isOn = EffectsManager.Instance.AreEffectsEnabled();
            effectsToggle.onValueChanged.AddListener(OnEffectsToggle);
        }
    }
    
    public void UpdateLevel(int level)
    {
        if (levelText != null)
            levelText.text = $"Level {level}";
    }
    
    public void UpdateMoves(int moves)
    {
        if (movesText != null)
            movesText.text = $"Moves: {moves}";
    }
    
    public void UpdateStars(int stars)
    {
        if (starImages == null) return;
        
        for (int i = 0; i < starImages.Length; i++)
        {
            if (starImages[i] != null)
            {
                starImages[i].color = i < stars ? starActiveColor : starInactiveColor;
            }
        }
    }
    
    public void ShowLevelComplete(int level, int stars, int moves)
    {
        if (levelCompletePanel == null) return;
        
        levelCompletePanel.SetActive(true);
        
        if (completeMessageText != null)
            completeMessageText.text = $"Level {level} Complete!";
        
        if (completeMoves != null)
            completeMoves.text = $"Moves: {moves}";
        
        if (completeStarImages != null)
        {
            for (int i = 0; i < completeStarImages.Length; i++)
            {
                if (completeStarImages[i] != null)
                {
                    completeStarImages[i].color = i < stars ? starActiveColor : starInactiveColor;
                    
                    if (i < stars)
                    {
                        EffectsManager.Instance.AnimateScaleBounce(completeStarImages[i].transform, 0.5f, 1.5f);
                        EffectsManager.Instance.PlayStarEffect(completeStarImages[i].transform.position);
                    }
                }
            }
        }
    }
    
    public void ShowPause()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
    public void HidePause()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
    private void OnNextLevel()
    {
        AudioManager.Instance.PlayButtonClick();
        if (GameManager.Instance != null)
            GameManager.Instance.NextLevel();
        
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }
    
    private void OnRestart()
    {
        AudioManager.Instance.PlayButtonClick();
        HidePause();
        
        if (GameManager.Instance != null)
            GameManager.Instance.RestartLevel();
        
        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(false);
    }
    
    private void OnMenu()
    {
        AudioManager.Instance.PlayButtonClick();
        HidePause();
        
        if (GameManager.Instance != null)
            GameManager.Instance.ReturnToMenu();
    }
    
    private void OnResume()
    {
        AudioManager.Instance.PlayButtonClick();
        HidePause();
    }
    
    private void OnSoundToggle(bool enabled)
    {
        AudioManager.Instance.SetSoundEnabled(enabled);
    }
    
    private void OnMusicToggle(bool enabled)
    {
        AudioManager.Instance.SetMusicEnabled(enabled);
    }
    
    private void OnEffectsToggle(bool enabled)
    {
        EffectsManager.Instance.SetEffectsEnabled(enabled);
    }
    
    public void OnPauseButtonClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        ShowPause();
    }
}
