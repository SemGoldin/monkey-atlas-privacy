using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int currentLevel;
    public int totalScore;
    public List<LevelData> completedLevels = new List<LevelData>();
}

[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public int stars;
    public int bestMoves;
    public bool completed;
}

[System.Serializable]
public class SaveData
{
    public GameData gameData;
    public int currentLevelProgress;
    public List<int> nutsOnBolts; // Serialized state of current game
    public int currentMoves;
}
