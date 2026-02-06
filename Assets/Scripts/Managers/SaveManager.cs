using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SaveManager");
                instance = go.AddComponent<SaveManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    
    private string saveFilePath;
    private SaveData currentSaveData;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
        LoadGame();
    }
    
    public void SaveGame(GameData gameData, int currentLevel = -1, System.Collections.Generic.List<int> gameState = null, int moves = 0)
    {
        currentSaveData = new SaveData
        {
            gameData = gameData,
            currentLevelProgress = currentLevel,
            nutsOnBolts = gameState,
            currentMoves = moves
        };
        
        string json = JsonUtility.ToJson(currentSaveData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to: " + saveFilePath);
    }
    
    public SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            currentSaveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded from: " + saveFilePath);
        }
        else
        {
            currentSaveData = new SaveData
            {
                gameData = new GameData
                {
                    currentLevel = 1,
                    totalScore = 0
                }
            };
            Debug.Log("No save file found. Creating new game data.");
        }
        
        return currentSaveData;
    }
    
    public GameData GetGameData()
    {
        return currentSaveData?.gameData ?? new GameData { currentLevel = 1, totalScore = 0 };
    }
    
    public void ClearSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }
        
        currentSaveData = new SaveData
        {
            gameData = new GameData { currentLevel = 1, totalScore = 0 }
        };
    }
    
    public bool HasInProgressLevel()
    {
        return currentSaveData != null && currentSaveData.currentLevelProgress > 0;
    }
    
    public void ClearCurrentProgress()
    {
        if (currentSaveData != null)
        {
            currentSaveData.currentLevelProgress = -1;
            currentSaveData.nutsOnBolts = null;
            currentSaveData.currentMoves = 0;
        }
    }
}
