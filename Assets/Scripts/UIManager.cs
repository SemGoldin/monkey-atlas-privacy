using UnityEngine;
using UnityEngine.UI;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Manages the UI elements of the game
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("UI Panels")]
        [SerializeField] private GameObject victoryPanel;
        [SerializeField] private Text victoryText;
        
        [Header("Buttons")]
        [SerializeField] private Button menuButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button bonusButton;
        
        [Header("Game Info")]
        [SerializeField] private Text levelText;
        [SerializeField] private Text movesText;
        
        private int moveCount = 0;
        
        private void Start()
        {
            // Setup button listeners
            if (menuButton != null)
            {
                menuButton.onClick.AddListener(OnMenuButtonClicked);
            }
            
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(OnRestartButtonClicked);
            }
            
            if (bonusButton != null)
            {
                bonusButton.onClick.AddListener(OnBonusButtonClicked);
            }
            
            // Hide victory panel initially
            if (victoryPanel != null)
            {
                victoryPanel.SetActive(false);
            }
            
            UpdateLevelDisplay();
            UpdateMovesDisplay();
        }
        
        /// <summary>
        /// Show the victory panel
        /// </summary>
        public void ShowVictoryPanel()
        {
            if (victoryPanel != null)
            {
                victoryPanel.SetActive(true);
                
                if (victoryText != null)
                {
                    victoryText.text = "Winner!";
                }
            }
        }
        
        /// <summary>
        /// Hide the victory panel
        /// </summary>
        public void HideVictoryPanel()
        {
            if (victoryPanel != null)
            {
                victoryPanel.SetActive(false);
            }
        }
        
        /// <summary>
        /// Update the level display
        /// </summary>
        public void UpdateLevelDisplay()
        {
            if (levelText != null)
            {
                int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
                levelText.text = $"Level {currentLevel}";
            }
        }
        
        /// <summary>
        /// Update the moves counter
        /// </summary>
        public void UpdateMovesDisplay()
        {
            if (movesText != null)
            {
                movesText.text = $"Moves: {moveCount}";
            }
        }
        
        /// <summary>
        /// Increment move counter
        /// </summary>
        public void IncrementMoves()
        {
            moveCount++;
            UpdateMovesDisplay();
        }
        
        /// <summary>
        /// Reset move counter
        /// </summary>
        public void ResetMoves()
        {
            moveCount = 0;
            UpdateMovesDisplay();
        }
        
        /// <summary>
        /// Handle menu button click
        /// </summary>
        private void OnMenuButtonClicked()
        {
            GameManager.Instance?.ExitToMenu();
        }
        
        /// <summary>
        /// Handle restart button click
        /// </summary>
        private void OnRestartButtonClicked()
        {
            GameManager.Instance?.RestartGame();
            ResetMoves();
        }
        
        /// <summary>
        /// Handle bonus button click
        /// </summary>
        private void OnBonusButtonClicked()
        {
            GameManager.Instance?.UseBonusAction();
        }
    }
}
