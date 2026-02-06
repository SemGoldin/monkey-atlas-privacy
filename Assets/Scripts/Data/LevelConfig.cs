using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/Level Configuration")]
public class LevelConfig : ScriptableObject
{
    [Header("Level Settings")]
    public int minBolts = 3;
    public int maxBolts = 8;
    public int nutsPerBolt = 4;
    
    [Header("Difficulty Progression")]
    public int boltsIncreaseEveryNLevels = 5;
    public int maxDifficulty = 10;
    
    [Header("Star Ratings")]
    public int threeStar_MaxMoves = 10;
    public int twoStar_MaxMoves = 20;
    public int oneStar_MaxMoves = 40;
    
    [Header("Colors")]
    public Color[] nutColors = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.cyan,
        Color.magenta,
        new Color(1f, 0.5f, 0f), // Orange
        new Color(0.5f, 0f, 0.5f) // Purple
    };
    
    public int GetBoltsForLevel(int level)
    {
        int bolts = minBolts + (level / boltsIncreaseEveryNLevels);
        return Mathf.Min(bolts, maxBolts);
    }
    
    public int GetStars(int moves)
    {
        if (moves <= threeStar_MaxMoves) return 3;
        if (moves <= twoStar_MaxMoves) return 2;
        if (moves <= oneStar_MaxMoves) return 1;
        return 0;
    }
}
