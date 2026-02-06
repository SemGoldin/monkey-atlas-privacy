using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Generates maze layout and spawns pellets dynamically
/// </summary>
public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Settings")]
    [SerializeField] private int width = 28;
    [SerializeField] private int height = 31;
    [SerializeField] private float cellSize = 1f;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject pelletPrefab;
    [SerializeField] private GameObject powerPelletPrefab;
    
    [Header("Maze Layout")]
    [SerializeField] private Transform mazeContainer;
    [SerializeField] private Transform pelletContainer;

    private int[,] mazeData;
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private int totalPellets = 0;
    private int collectedPellets = 0;

    // Maze generation algorithm constants
    private const int WALL = 1;
    private const int PATH = 0;
    private const int PELLET = 2;
    private const int POWER_PELLET = 3;

    public void GenerateMaze(int level)
    {
        ClearMaze();
        CreateMazeData(level);
        BuildMaze();
        SpawnPellets();
    }

    private void ClearMaze()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        spawnedObjects.Clear();
        collectedPellets = 0;
        totalPellets = 0;
    }

    private void CreateMazeData(int level)
    {
        mazeData = new int[width, height];
        
        // Create border walls
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (x == 0 || x == width - 1 || z == 0 || z == height - 1)
                {
                    mazeData[x, z] = WALL;
                }
                else
                {
                    mazeData[x, z] = PATH;
                }
            }
        }

        // Generate internal maze structure using recursive backtracking
        GenerateInternalMaze(level);
        
        // Place power pellets in corners
        PlacePowerPellets();
    }

    private void GenerateInternalMaze(int level)
    {
        // Create a classic maze pattern with complexity based on level
        int complexity = Mathf.Min(level, 5);
        
        // Add horizontal and vertical walls in a pattern
        for (int x = 2; x < width - 2; x += 4)
        {
            for (int z = 2; z < height - 2; z += 4)
            {
                // Create wall blocks
                int wallHeight = Random.Range(2, 4 + complexity);
                int wallWidth = Random.Range(2, 4 + complexity);
                
                for (int wx = 0; wx < wallWidth && x + wx < width - 1; wx++)
                {
                    for (int wz = 0; wz < wallHeight && z + wz < height - 1; wz++)
                    {
                        if (Random.value > 0.3f) // Add some randomness
                        {
                            mazeData[x + wx, z + wz] = WALL;
                        }
                    }
                }
            }
        }

        // Ensure there are paths
        CreatePaths();
        
        // Mark pellet positions
        for (int x = 1; x < width - 1; x++)
        {
            for (int z = 1; z < height - 1; z++)
            {
                if (mazeData[x, z] == PATH)
                {
                    mazeData[x, z] = PELLET;
                }
            }
        }
    }

    private void CreatePaths()
    {
        // Create guaranteed paths through the maze
        // Horizontal center path
        int centerZ = height / 2;
        for (int x = 1; x < width - 1; x++)
        {
            mazeData[x, centerZ] = PATH;
        }

        // Vertical center path
        int centerX = width / 2;
        for (int z = 1; z < height - 1; z++)
        {
            mazeData[centerX, z] = PATH;
        }

        // Create corner access paths
        CreateCornerPaths();
    }

    private void CreateCornerPaths()
    {
        // Top-left to bottom-right diagonal path
        int steps = Mathf.Min(width, height) / 4;
        for (int i = 0; i < steps; i++)
        {
            mazeData[i + 2, i + 2] = PATH;
            mazeData[width - i - 3, i + 2] = PATH;
            mazeData[i + 2, height - i - 3] = PATH;
            mazeData[width - i - 3, height - i - 3] = PATH;
        }
    }

    private void PlacePowerPellets()
    {
        // Place power pellets in the four corners of the playable area
        int offset = 3;
        
        mazeData[offset, offset] = POWER_PELLET;
        mazeData[width - offset - 1, offset] = POWER_PELLET;
        mazeData[offset, height - offset - 1] = POWER_PELLET;
        mazeData[width - offset - 1, height - offset - 1] = POWER_PELLET;
    }

    private void BuildMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = new Vector3(
                    (x - width / 2) * cellSize,
                    0,
                    (z - height / 2) * cellSize
                );

                // Always create floor
                if (floorPrefab != null)
                {
                    GameObject floor = Instantiate(
                        floorPrefab,
                        position,
                        Quaternion.identity,
                        mazeContainer
                    );
                    spawnedObjects.Add(floor);
                }

                // Create walls
                if (mazeData[x, z] == WALL && wallPrefab != null)
                {
                    GameObject wall = Instantiate(
                        wallPrefab,
                        position + Vector3.up * 0.5f,
                        Quaternion.identity,
                        mazeContainer
                    );
                    wall.tag = "Wall";
                    spawnedObjects.Add(wall);
                }
            }
        }
    }

    private void SpawnPellets()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = new Vector3(
                    (x - width / 2) * cellSize,
                    0.3f,
                    (z - height / 2) * cellSize
                );

                if (mazeData[x, z] == PELLET && pelletPrefab != null)
                {
                    GameObject pellet = Instantiate(
                        pelletPrefab,
                        position,
                        Quaternion.identity,
                        pelletContainer
                    );
                    pellet.tag = "Pellet";
                    
                    Pellet pelletScript = pellet.GetComponent<Pellet>();
                    if (pelletScript != null)
                    {
                        pelletScript.SetMazeGenerator(this);
                    }
                    
                    spawnedObjects.Add(pellet);
                    totalPellets++;
                }
                else if (mazeData[x, z] == POWER_PELLET && powerPelletPrefab != null)
                {
                    GameObject powerPellet = Instantiate(
                        powerPelletPrefab,
                        position,
                        Quaternion.identity,
                        pelletContainer
                    );
                    powerPellet.tag = "PowerPellet";
                    
                    PowerPellet powerPelletScript = powerPellet.GetComponent<PowerPellet>();
                    if (powerPelletScript != null)
                    {
                        powerPelletScript.SetMazeGenerator(this);
                    }
                    
                    spawnedObjects.Add(powerPellet);
                    totalPellets++;
                }
            }
        }
    }

    public void OnPelletCollected()
    {
        collectedPellets++;
        
        if (collectedPellets >= totalPellets)
        {
            GameManager.Instance?.OnAllPelletsCollected();
        }
    }

    // Getters
    public int GetTotalPellets() => totalPellets;
    public int GetCollectedPellets() => collectedPellets;
    public int GetRemainingPellets() => totalPellets - collectedPellets;
}
