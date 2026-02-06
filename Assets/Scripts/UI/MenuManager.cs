using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private Button playButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    
    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle effectsToggle;
    [SerializeField] private Button closeSettingsButton;
    
    [Header("Level Select")]
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private Transform levelButtonsContainer;
    [SerializeField] private GameObject levelButtonPrefab;
    
    [Header("Audio")]
    [SerializeField] private AudioConfig audioConfig;
    
    private GameData gameData;
    
    private void Start()
    {
        LoadGameData();
        SetupUI();
        SetupButtons();
        
        if (audioConfig != null)
        {
            AudioManager.Instance.SetAudioConfig(audioConfig);
            if (audioConfig.menuMusic != null)
            {
                AudioManager.Instance.PlayMusic(audioConfig.menuMusic);
            }
        }
        
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
        
        if (levelSelectPanel != null)
            levelSelectPanel.SetActive(false);
    }
    
    private void LoadGameData()
    {
        SaveData saveData = SaveManager.Instance.LoadGame();
        gameData = saveData.gameData;
    }
    
    private void SetupUI()
    {
        if (currentLevelText != null)
            currentLevelText.text = $"Level {gameData.currentLevel}";
        
        if (totalScoreText != null)
            totalScoreText.text = $"Score: {gameData.totalScore}";
        
        if (continueButton != null)
            continueButton.gameObject.SetActive(SaveManager.Instance.HasInProgressLevel());
    }
    
    private void SetupButtons()
    {
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayClicked);
        
        if (continueButton != null)
            continueButton.onClick.AddListener(OnContinueClicked);
        
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingsClicked);
        
        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuitClicked);
        
        if (closeSettingsButton != null)
            closeSettingsButton.onClick.AddListener(OnCloseSettings);
        
        SetupSettingsToggles();
    }
    
    private void SetupSettingsToggles()
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
    
    private void OnPlayClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        StartGame(gameData.currentLevel);
    }
    
    private void OnContinueClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        SaveData saveData = SaveManager.Instance.LoadGame();
        StartGame(saveData.currentLevelProgress > 0 ? saveData.currentLevelProgress : gameData.currentLevel);
    }
    
    private void OnSettingsClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }
    
    private void OnCloseSettings()
    {
        AudioManager.Instance.PlayButtonClick();
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }
    
    private void OnQuitClicked()
    {
        AudioManager.Instance.PlayButtonClick();
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
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
    
    private void StartGame(int level)
    {
        // Save the level we're starting
        gameData.currentLevel = level;
        SaveManager.Instance.SaveGame(gameData, level);
        
        SceneManager.LoadScene("Game");
    }
}
