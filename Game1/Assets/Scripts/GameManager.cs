using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Manages the game state, level configuration, and win conditions
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject nutPrefab;
    [SerializeField] private GameObject boltPrefab;
    
    [Header("Level Configuration")]
    [SerializeField] private int numberOfBolts = 6;
    [SerializeField] private int nutsPerBolt = 4;
    [SerializeField] private int numberOfColors = 4;
    [SerializeField] private int emptyBolts = 2;
    
    [Header("Layout")]
    [SerializeField] private float boltSpacing = 2f;
    [SerializeField] private Vector3 startPosition = new Vector3(-5f, 0f, 0f);
    
    private List<Bolt> bolts = new List<Bolt>();
    private BoltSelector boltSelector;
    private int moveCount = 0;
    
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        boltSelector = GetComponent<BoltSelector>();
        if (boltSelector == null)
        {
            boltSelector = gameObject.AddComponent<BoltSelector>();
        }
    }

    private void Start()
    {
        GenerateLevel();
    }

    /// <summary>
    /// Generate a new level with bolts and nuts
    /// </summary>
    public void GenerateLevel()
    {
        ClearLevel();
        
        // Create bolts
        for (int i = 0; i < numberOfBolts; i++)
        {
            Vector3 position = startPosition + Vector3.right * (i * boltSpacing);
            Bolt bolt = CreateBolt(position);
            bolts.Add(bolt);
        }
        
        // Generate nut distribution
        List<NutColor> allNuts = GenerateNutDistribution();
        
        // Shuffle nuts
        ShuffleList(allNuts);
        
        // Distribute nuts to bolts (leaving some empty)
        int nutsToDistribute = allNuts.Count;
        int boltsToFill = numberOfBolts - emptyBolts;
        int nutIndex = 0;
        
        for (int i = 0; i < boltsToFill && nutIndex < nutsToDistribute; i++)
        {
            Bolt bolt = bolts[i];
            
            // Fill each bolt up to capacity
            for (int j = 0; j < nutsPerBolt && nutIndex < nutsToDistribute; j++)
            {
                Nut nut = CreateNut(allNuts[nutIndex], bolt.GetNextNutPosition());
                bolt.AddNut(nut);
                nutIndex++;
            }
        }
        
        // Initialize bolt selector
        if (boltSelector != null)
        {
            boltSelector.Initialize(bolts);
        }
        
        moveCount = 0;
        Debug.Log($"Level generated with {numberOfBolts} bolts, {nutsToDistribute} nuts, {numberOfColors} colors");
    }

    /// <summary>
    /// Generate the distribution of nuts for the level
    /// </summary>
    private List<NutColor> GenerateNutDistribution()
    {
        List<NutColor> nuts = new List<NutColor>();
        
        // Get available colors
        NutColor[] availableColors = System.Enum.GetValues(typeof(NutColor))
            .Cast<NutColor>()
            .Take(numberOfColors)
            .ToArray();
        
        // Create nutsPerBolt nuts of each color
        foreach (NutColor color in availableColors)
        {
            for (int i = 0; i < nutsPerBolt; i++)
            {
                nuts.Add(color);
            }
        }
        
        return nuts;
    }

    /// <summary>
    /// Create a bolt at the specified position
    /// </summary>
    private Bolt CreateBolt(Vector3 position)
    {
        GameObject boltObj;
        
        if (boltPrefab != null)
        {
            boltObj = Instantiate(boltPrefab, position, Quaternion.identity);
        }
        else
        {
            // Create simple bolt if no prefab
            boltObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            boltObj.transform.position = position;
            boltObj.transform.localScale = new Vector3(0.3f, 2f, 0.3f);
            boltObj.GetComponent<Renderer>().material.color = Color.gray;
        }
        
        boltObj.name = $"Bolt_{bolts.Count}";
        
        Bolt bolt = boltObj.GetComponent<Bolt>();
        if (bolt == null)
        {
            bolt = boltObj.AddComponent<Bolt>();
        }
        
        return bolt;
    }

    /// <summary>
    /// Create a nut with the specified color at position
    /// </summary>
    private Nut CreateNut(NutColor color, Vector3 position)
    {
        GameObject nutObj;
        
        if (nutPrefab != null)
        {
            nutObj = Instantiate(nutPrefab, position, Quaternion.identity);
        }
        else
        {
            // Create simple nut if no prefab
            nutObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            nutObj.transform.position = position;
            nutObj.transform.localScale = Vector3.one * 0.5f;
        }
        
        Nut nut = nutObj.GetComponent<Nut>();
        if (nut == null)
        {
            nut = nutObj.AddComponent<Nut>();
        }
        
        nut.Initialize(color);
        return nut;
    }

    /// <summary>
    /// Clear all bolts and nuts from the scene
    /// </summary>
    private void ClearLevel()
    {
        foreach (Bolt bolt in bolts)
        {
            if (bolt != null)
                Destroy(bolt.gameObject);
        }
        bolts.Clear();
    }

    /// <summary>
    /// Shuffle a list randomly
    /// </summary>
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }

    /// <summary>
    /// Record a move
    /// </summary>
    public void RecordMove()
    {
        moveCount++;
        CheckWinCondition();
    }

    /// <summary>
    /// Check if the player has won
    /// </summary>
    private void CheckWinCondition()
    {
        bool allSorted = true;
        
        foreach (Bolt bolt in bolts)
        {
            if (!bolt.IsSorted())
            {
                allSorted = false;
                break;
            }
        }
        
        if (allSorted)
        {
            OnLevelComplete();
        }
    }

    /// <summary>
    /// Called when level is completed
    /// </summary>
    private void OnLevelComplete()
    {
        Debug.Log($"Level Complete! Moves: {moveCount}");
        // Add UI feedback, next level logic, etc.
    }

    /// <summary>
    /// Reset the current level
    /// </summary>
    public void ResetLevel()
    {
        GenerateLevel();
    }
}
