using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Main game manager that controls the game state and logic
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [Header("Game Configuration")]
        [SerializeField] private int totalBolts = 9;
        [SerializeField] private int emptyBolts = 2;
        [SerializeField] private int nutsPerColor = 4;
        
        [Header("Prefabs")]
        [SerializeField] private GameObject boltPrefab;
        [SerializeField] private GameObject nutPrefab;
        
        private GameState currentState = GameState.Initialization;
        private List<Bolt> bolts = new List<Bolt>();
        private Bolt selectedBolt = null;
        private Nut liftedNut = null;
        
        // Total colors in the game (7 colors)
        private readonly NutColor[] allColors = new NutColor[]
        {
            NutColor.Red, NutColor.Blue, NutColor.Green, NutColor.Yellow,
            NutColor.Purple, NutColor.Orange, NutColor.Pink
        };
        
        public GameState CurrentState => currentState;
        
        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        
        private void Start()
        {
            InitializeGame();
        }
        
        /// <summary>
        /// Initialize the game - spawn bolts and nuts
        /// </summary>
        private void InitializeGame()
        {
            currentState = GameState.Initialization;
            
            // Create bolts
            SpawnBolts();
            
            // Create and assign nuts with random colors
            SpawnNuts();
            
            // Save initial state (progress tracking)
            SaveGameState();
            
            // Set state to player input
            currentState = GameState.PlayerInput;
        }
        
        /// <summary>
        /// Spawn all bolts in the scene
        /// </summary>
        private void SpawnBolts()
        {
            bolts.Clear();
            
            for (int i = 0; i < totalBolts; i++)
            {
                Vector3 position = GameConstants.GetBoltGridPosition(i);
                
                GameObject boltObj = Instantiate(boltPrefab, position, Quaternion.identity);
                Bolt bolt = boltObj.GetComponent<Bolt>();
                if (bolt != null)
                {
                    bolts.Add(bolt);
                }
            }
        }
        
        /// <summary>
        /// Spawn nuts with random colors on bolts
        /// </summary>
        private void SpawnNuts()
        {
            // Create list of all nuts to spawn (4 of each color = 28 nuts)
            List<NutColor> nutColors = new List<NutColor>();
            foreach (NutColor color in allColors)
            {
                for (int i = 0; i < nutsPerColor; i++)
                {
                    nutColors.Add(color);
                }
            }
            
            // Shuffle the colors
            GameConstants.Shuffle(nutColors);
            
            // Assign nuts to bolts (leaving last 2 bolts empty)
            int nutIndex = 0;
            int boltsToFill = totalBolts - emptyBolts;
            
            for (int boltIndex = 0; boltIndex < boltsToFill; boltIndex++)
            {
                Bolt bolt = bolts[boltIndex];
                
                // Add 4 nuts to each bolt
                for (int i = 0; i < nutsPerColor; i++)
                {
                    if (nutIndex >= nutColors.Count) break;
                    
                    NutColor color = nutColors[nutIndex];
                    Vector3 nutPosition = bolt.GetNutPosition(i);
                    
                    GameObject nutObj = Instantiate(nutPrefab, nutPosition, Quaternion.identity);
                    Nut nut = nutObj.GetComponent<Nut>();
                    
                    if (nut != null)
                    {
                        nut.Initialize(color, bolt);
                        bolt.AddNut(nut);
                    }
                    
                    nutIndex++;
                }
            }
        }
        
        /// <summary>
        /// Handle bolt click event
        /// </summary>
        public void OnBoltClicked(Bolt clickedBolt)
        {
            if (currentState != GameState.PlayerInput && currentState != GameState.NutMovement)
            {
                return;
            }
            
            // If no nut is lifted yet
            if (liftedNut == null)
            {
                // Try to lift a nut from this bolt
                if (!clickedBolt.IsEmpty)
                {
                    selectedBolt = clickedBolt;
                    liftedNut = clickedBolt.RemoveTopNut();
                    clickedBolt.SetSelected(true);
                    
                    // Start unscrewing animation
                    StartCoroutine(LiftNutAnimation());
                }
            }
            else
            {
                // A nut is already lifted
                if (clickedBolt == selectedBolt)
                {
                    // Clicked on the same bolt - return the nut
                    StartCoroutine(ReturnNutToBolt());
                }
                else
                {
                    // Clicked on a different bolt - try to transfer
                    StartCoroutine(TransferNutToBolt(clickedBolt));
                }
            }
        }
        
        /// <summary>
        /// Animate lifting a nut from bolt
        /// </summary>
        private IEnumerator LiftNutAnimation()
        {
            currentState = GameState.NutMovement;
            
            // Play unscrewing sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayUnscrewingSound();
            }
            
            Vector3 unscrewPosition = selectedBolt.GetUnscrewPosition();
            yield return StartCoroutine(liftedNut.UnscrewAnimation(unscrewPosition));
            
            currentState = GameState.PlayerInput;
        }
        
        /// <summary>
        /// Return the lifted nut to its original bolt
        /// </summary>
        private IEnumerator ReturnNutToBolt()
        {
            currentState = GameState.NutMovement;
            
            Vector3 returnPosition = selectedBolt.GetNextNutPosition();
            yield return StartCoroutine(liftedNut.ReturnToBolt(returnPosition));
            
            selectedBolt.AddNut(liftedNut);
            selectedBolt.SetSelected(false);
            
            liftedNut = null;
            selectedBolt = null;
            
            currentState = GameState.PlayerInput;
        }
        
        /// <summary>
        /// Transfer the lifted nut to a target bolt
        /// </summary>
        private IEnumerator TransferNutToBolt(Bolt targetBolt)
        {
            currentState = GameState.NutMovement;
            
            // Check if transfer is valid
            bool canTransfer = targetBolt.CanAcceptNut(liftedNut.Color);
            
            if (canTransfer)
            {
                // Success - transfer the nut
                yield return StartCoroutine(SuccessfulTransfer(targetBolt));
            }
            else
            {
                // Failure - show error and return
                yield return StartCoroutine(FailedTransfer());
            }
        }
        
        /// <summary>
        /// Handle successful nut transfer
        /// </summary>
        private IEnumerator SuccessfulTransfer(Bolt targetBolt)
        {
            currentState = GameState.SuccessSignaling;
            
            // Play screwing sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayScrewingSound();
            }
            
            // Animate nut to target bolt
            Vector3 targetPosition = targetBolt.GetNextNutPosition();
            yield return StartCoroutine(liftedNut.ScrewOntoAnimation(targetPosition, targetBolt));
            
            // Add nut to target bolt
            targetBolt.AddNut(liftedNut);
            
            // Visual effect for contact
            if (EffectsManager.Instance != null)
            {
                EffectsManager.Instance.PlayNutContactEffect(targetPosition);
            }
            
            // Reset selection
            selectedBolt.SetSelected(false);
            liftedNut = null;
            selectedBolt = null;
            
            // Check for victory
            if (CheckVictoryCondition())
            {
                yield return StartCoroutine(ShowVictory());
            }
            else
            {
                currentState = GameState.PlayerInput;
            }
        }
        
        /// <summary>
        /// Handle failed nut transfer
        /// </summary>
        private IEnumerator FailedTransfer()
        {
            currentState = GameState.SuccessSignaling;
            
            // Play error sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayErrorSound();
            }
            
            // Shake animation
            yield return StartCoroutine(liftedNut.ShakeAnimation());
            
            // Return nut to original bolt
            yield return StartCoroutine(ReturnNutToBolt());
        }
        
        /// <summary>
        /// Check if all nuts are sorted by color (victory condition)
        /// </summary>
        private bool CheckVictoryCondition()
        {
            // All bolts must be either empty or contain only one color
            foreach (Bolt bolt in bolts)
            {
                if (!bolt.IsSorted())
                {
                    return false;
                }
            }
            
            // Additionally, check that we have exactly 7 bolts with 4 nuts of the same color
            int sortedBolts = bolts.Count(b => b.IsFull && b.IsSorted());
            return sortedBolts == allColors.Length;
        }
        
        /// <summary>
        /// Show victory panel and play victory sound
        /// </summary>
        private IEnumerator ShowVictory()
        {
            currentState = GameState.Victory;
            
            // Play victory sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayVictorySound();
            }
            
            // Play victory effect
            if (EffectsManager.Instance != null)
            {
                EffectsManager.Instance.PlayVictoryEffect(Vector3.zero);
            }
            
            // Show winner panel (to be implemented in UI)
            Debug.Log("WINNER! All nuts are sorted!");
            
            yield return null;
        }
        
        /// <summary>
        /// Save game state (for progress tracking)
        /// </summary>
        private void SaveGameState()
        {
            // Save to PlayerPrefs or other persistence system
            PlayerPrefs.SetInt("CurrentLevel", 1);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Restart the game
        /// </summary>
        public void RestartGame()
        {
            // Clear existing bolts and nuts
            foreach (Bolt bolt in bolts)
            {
                if (bolt != null)
                {
                    Destroy(bolt.gameObject);
                }
            }
            
            // Re-initialize
            InitializeGame();
        }
        
        /// <summary>
        /// Exit to menu
        /// </summary>
        public void ExitToMenu()
        {
            // Load menu scene or show menu UI
            Debug.Log("Exiting to menu...");
        }
        
        /// <summary>
        /// Use bonus action
        /// </summary>
        public void UseBonusAction()
        {
            // Implement bonus action (e.g., hint, auto-solve one bolt)
            Debug.Log("Using bonus action...");
        }
    }
}
