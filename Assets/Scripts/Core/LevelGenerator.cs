using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelConfig levelConfig;
    [SerializeField] private GameObject boltPrefab;
    [SerializeField] private GameObject nutPrefab;
    
    [Header("Layout Settings")]
    [SerializeField] private float boltSpacing = 1.5f;
    [SerializeField] private Vector3 startPosition = new Vector3(-3f, 0f, 0f);
    
    private List<Bolt> bolts = new List<Bolt>();
    private List<Nut> allNuts = new List<Nut>();
    
    public void GenerateLevel(int levelNumber)
    {
        ClearLevel();
        
        int numBolts = levelConfig.GetBoltsForLevel(levelNumber);
        int nutsPerBolt = levelConfig.nutsPerBolt;
        int numColoredBolts = numBolts - 2; // Leave 2 bolts empty for moves
        
        // Create bolts
        CreateBolts(numBolts);
        
        // Generate and distribute nuts
        GenerateAndDistributeNuts(numColoredBolts, nutsPerBolt);
    }
    
    private void CreateBolts(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = startPosition + Vector3.right * (i * boltSpacing);
            GameObject boltObj = Instantiate(boltPrefab, position, Quaternion.identity, transform);
            boltObj.name = $"Bolt_{i}";
            
            Bolt bolt = boltObj.GetComponent<Bolt>();
            if (bolt == null)
            {
                bolt = boltObj.AddComponent<Bolt>();
            }
            
            bolt.Initialize(levelConfig.nutsPerBolt);
            bolt.SetBoltColor(new Color(0.5f, 0.5f, 0.5f, 1f)); // Gray color for bolt
            bolts.Add(bolt);
        }
    }
    
    private void GenerateAndDistributeNuts(int numColors, int nutsPerColor)
    {
        List<Nut> tempNuts = new List<Nut>();
        
        // Create nuts for each color
        for (int colorIdx = 0; colorIdx < numColors; colorIdx++)
        {
            Color color = levelConfig.nutColors[colorIdx % levelConfig.nutColors.Length];
            
            for (int i = 0; i < nutsPerColor; i++)
            {
                GameObject nutObj = Instantiate(nutPrefab, Vector3.zero, Quaternion.identity, transform);
                nutObj.name = $"Nut_Color{colorIdx}_{i}";
                
                Nut nut = nutObj.GetComponent<Nut>();
                if (nut == null)
                {
                    nut = nutObj.AddComponent<Nut>();
                }
                
                nut.Initialize(colorIdx, color);
                tempNuts.Add(nut);
                allNuts.Add(nut);
            }
        }
        
        // Shuffle nuts randomly
        ShuffleList(tempNuts);
        
        // Distribute nuts to bolts (excluding the last 2 empty bolts)
        int nutIndex = 0;
        for (int boltIdx = 0; boltIdx < bolts.Count - 2; boltIdx++)
        {
            for (int i = 0; i < nutsPerColor; i++)
            {
                if (nutIndex < tempNuts.Count)
                {
                    bolts[boltIdx].AddNut(tempNuts[nutIndex]);
                    nutIndex++;
                }
            }
        }
    }
    
    private void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    
    public void ClearLevel()
    {
        foreach (Bolt bolt in bolts)
        {
            if (bolt != null)
                Destroy(bolt.gameObject);
        }
        bolts.Clear();
        
        foreach (Nut nut in allNuts)
        {
            if (nut != null)
                Destroy(nut.gameObject);
        }
        allNuts.Clear();
    }
    
    public List<Bolt> GetBolts()
    {
        return bolts;
    }
    
    public List<Nut> GetAllNuts()
    {
        return allNuts;
    }
    
    public void SetLevelConfig(LevelConfig config)
    {
        levelConfig = config;
    }
}
