using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("References")]
    [SerializeField] private LevelConfig levelConfig;
    [SerializeField] private AudioConfig audioConfig;
    [SerializeField] private LevelGenerator levelGenerator;
    
    [Header("UI References")]
    [SerializeField] private GameUIManager gameUIManager;
    
    private GameData gameData;
    private int currentLevel;
    private int currentMoves;
    private Nut selectedNut;
    private Bolt selectedBolt;
    private bool isLevelComplete;
    private List<Bolt> allBolts;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        // Initialize managers
        if (audioConfig != null)
        {
            AudioManager.Instance.SetAudioConfig(audioConfig);
        }
    }
    
    private void Start()
    {
        LoadGameData();
        StartLevel(currentLevel);
        
        if (audioConfig != null && audioConfig.gameMusic != null)
        {
            AudioManager.Instance.PlayMusic(audioConfig.gameMusic);
        }
    }
    
    private void LoadGameData()
    {
        SaveData saveData = SaveManager.Instance.LoadGame();
        gameData = saveData.gameData;
        currentLevel = gameData.currentLevel;
    }
    
    public void StartLevel(int level)
    {
        currentLevel = level;
        currentMoves = 0;
        isLevelComplete = false;
        selectedNut = null;
        selectedBolt = null;
        
        if (levelGenerator == null)
        {
            levelGenerator = FindObjectOfType<LevelGenerator>();
        }
        
        if (levelConfig != null)
        {
            levelGenerator.SetLevelConfig(levelConfig);
        }
        
        levelGenerator.GenerateLevel(level);
        allBolts = levelGenerator.GetBolts();
        
        UpdateUI();
    }
    
    public void OnBoltClicked(Bolt clickedBolt)
    {
        if (isLevelComplete) return;
        
        // If no nut is selected, try to pick from this bolt
        if (selectedNut == null)
        {
            TryPickNut(clickedBolt);
        }
        else
        {
            // Try to place nut on this bolt
            TryPlaceNut(clickedBolt);
        }
    }
    
    private void TryPickNut(Bolt bolt)
    {
        Nut nut = bolt.GetTopNut();
        if (nut == null) return;
        
        selectedNut = nut;
        selectedBolt = bolt;
        bolt.RemoveTopNut();
        
        nut.OnPickedUp();
        AudioManager.Instance.PlayNutPickup();
        EffectsManager.Instance.PlayNutPickupEffect(nut.transform.position);
        EffectsManager.Instance.AnimateScaleBounce(nut.transform);
    }
    
    private void TryPlaceNut(Bolt targetBolt)
    {
        if (targetBolt == selectedBolt)
        {
            // Return nut to original bolt
            selectedBolt.AddNut(selectedNut);
            selectedNut.OnPlaced();
            selectedNut = null;
            selectedBolt = null;
            return;
        }
        
        if (targetBolt.CanAddNut(selectedNut))
        {
            // Valid move
            targetBolt.AddNut(selectedNut);
            selectedNut.OnPlaced();
            
            currentMoves++;
            
            AudioManager.Instance.PlayNutPlace();
            EffectsManager.Instance.PlayNutPlaceEffect(selectedNut.transform.position);
            
            // Check if bolt is complete
            if (targetBolt.IsComplete())
            {
                AudioManager.Instance.PlayBoltComplete();
                EffectsManager.Instance.PlayBoltCompleteEffect(targetBolt.transform.position);
                EffectsManager.Instance.AnimateScaleBounce(targetBolt.transform, 0.5f, 1.3f);
            }
            
            selectedNut = null;
            selectedBolt = null;
            
            UpdateUI();
            CheckLevelComplete();
            SaveProgress();
        }
        else
        {
            // Invalid move - return to original bolt
            selectedBolt.AddNut(selectedNut);
            selectedNut.OnPlaced();
            selectedNut = null;
            selectedBolt = null;
            
            AudioManager.Instance.PlayInvalidMove();
        }
    }
    
    private void CheckLevelComplete()
    {
        bool allComplete = true;
        
        foreach (Bolt bolt in allBolts)
        {
            if (!bolt.IsEmpty() && !bolt.IsComplete())
            {
                allComplete = false;
                break;
            }
        }
        
        if (allComplete)
        {
            OnLevelComplete();
        }
    }
    
    private void OnLevelComplete()
    {
        isLevelComplete = true;
        
        int stars = levelConfig.GetStars(currentMoves);
        
        AudioManager.Instance.PlayLevelComplete();
        EffectsManager.Instance.PlayLevelCompleteEffect(Camera.main.transform.position + Vector3.forward * 5f);
        
        // Update game data
        LevelData levelData = gameData.completedLevels.Find(l => l.levelNumber == currentLevel);
        if (levelData == null)
        {
            levelData = new LevelData
            {
                levelNumber = currentLevel,
                stars = stars,
                bestMoves = currentMoves,
                completed = true
            };
            gameData.completedLevels.Add(levelData);
        }
        else
        {
            if (currentMoves < levelData.bestMoves)
            {
                levelData.bestMoves = currentMoves;
            }
            if (stars > levelData.stars)
            {
                levelData.stars = stars;
            }
        }
        
        gameData.totalScore += stars * 100;
        
        // Unlock next level
        if (currentLevel >= gameData.currentLevel)
        {
            gameData.currentLevel = currentLevel + 1;
        }
        
        SaveManager.Instance.SaveGame(gameData);
        SaveManager.Instance.ClearCurrentProgress();
        
        if (gameUIManager != null)
        {
            gameUIManager.ShowLevelComplete(currentLevel, stars, currentMoves);
        }
    }
    
    public void RestartLevel()
    {
        StartLevel(currentLevel);
    }
    
    public void NextLevel()
    {
        StartLevel(currentLevel + 1);
    }
    
    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
    
    private void UpdateUI()
    {
        if (gameUIManager != null)
        {
            gameUIManager.UpdateMoves(currentMoves);
            gameUIManager.UpdateLevel(currentLevel);
            
            int stars = levelConfig.GetStars(currentMoves);
            gameUIManager.UpdateStars(stars);
        }
    }
    
    private void SaveProgress()
    {
        List<int> gameState = new List<int>();
        // Serialize current game state for saving
        SaveManager.Instance.SaveGame(gameData, currentLevel, gameState, currentMoves);
    }
    
    public int GetCurrentMoves() => currentMoves;
    public int GetCurrentLevel() => currentLevel;
    public GameData GetGameData() => gameData;
}
