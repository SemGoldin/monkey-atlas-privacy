using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Scriptable Object for level configuration
/// Allows designers to create level presets without coding
/// </summary>
[CreateAssetMenu(fileName = "Level", menuName = "Bolt Puzzle/Level Configuration")]
public class LevelConfiguration : ScriptableObject
{
    [Header("Level Info")]
    [SerializeField] private string levelName = "Level 1";
    [SerializeField] private int levelNumber = 1;
    [SerializeField] [TextArea] private string description = "";
    
    [Header("Puzzle Settings")]
    [SerializeField] private int numberOfBolts = 6;
    [SerializeField] private int nutsPerBolt = 4;
    [SerializeField] private int numberOfColors = 4;
    [SerializeField] private int emptyBolts = 2;
    
    [Header("Difficulty")]
    [SerializeField] private Difficulty difficulty = Difficulty.Easy;
    [SerializeField] private int recommendedMoves = 20;
    
    [Header("Layout")]
    [SerializeField] private float boltSpacing = 2f;
    [SerializeField] private Vector3 startPosition = new Vector3(-5f, 0f, 0f);
    
    [Header("Custom Distribution (Optional)")]
    [SerializeField] private bool useCustomDistribution = false;
    [SerializeField] private BoltSetup[] customBoltSetup;
    
    public string LevelName => levelName;
    public int LevelNumber => levelNumber;
    public string Description => description;
    public int NumberOfBolts => numberOfBolts;
    public int NutsPerBolt => nutsPerBolt;
    public int NumberOfColors => numberOfColors;
    public int EmptyBolts => emptyBolts;
    public Difficulty LevelDifficulty => difficulty;
    public int RecommendedMoves => recommendedMoves;
    public float BoltSpacing => boltSpacing;
    public Vector3 StartPosition => startPosition;
    public bool UseCustomDistribution => useCustomDistribution;
    public BoltSetup[] CustomBoltSetup => customBoltSetup;
    
    /// <summary>
    /// Validate level configuration
    /// </summary>
    public bool IsValid(out string errorMessage)
    {
        if (numberOfBolts <= 0)
        {
            errorMessage = "Number of bolts must be greater than 0";
            return false;
        }
        
        if (nutsPerBolt <= 0)
        {
            errorMessage = "Nuts per bolt must be greater than 0";
            return false;
        }
        
        if (numberOfColors <= 0)
        {
            errorMessage = "Number of colors must be greater than 0";
            return false;
        }
        
        if (emptyBolts < 0)
        {
            errorMessage = "Empty bolts cannot be negative";
            return false;
        }
        
        if (emptyBolts >= numberOfBolts)
        {
            errorMessage = "Empty bolts must be less than total bolts";
            return false;
        }
        
        // Check if there are enough bolts to hold all nuts
        int totalNuts = numberOfColors * nutsPerBolt;
        int availableCapacity = (numberOfBolts - emptyBolts) * nutsPerBolt;
        
        if (totalNuts > availableCapacity)
        {
            errorMessage = $"Not enough bolt capacity for all nuts. Total nuts: {totalNuts}, Available capacity: {availableCapacity}";
            return false;
        }
        
        errorMessage = "";
        return true;
    }
    
    /// <summary>
    /// Calculate difficulty score based on parameters
    /// </summary>
    public float CalculateDifficultyScore()
    {
        float score = 0;
        
        // More bolts = slightly harder
        score += numberOfBolts * 0.5f;
        
        // More nuts per bolt = harder
        score += nutsPerBolt * 2f;
        
        // More colors = much harder
        score += numberOfColors * 3f;
        
        // Fewer empty bolts = harder (less maneuvering room)
        score += (numberOfBolts - emptyBolts) * 2f;
        
        return score;
    }
}

/// <summary>
/// Difficulty levels
/// </summary>
public enum Difficulty
{
    Tutorial,
    Easy,
    Medium,
    Hard,
    Expert
}

/// <summary>
/// Custom setup for a single bolt
/// </summary>
[System.Serializable]
public class BoltSetup
{
    [SerializeField] private List<NutColor> nutsFromBottom = new List<NutColor>();
    
    public List<NutColor> NutsFromBottom => nutsFromBottom;
}
