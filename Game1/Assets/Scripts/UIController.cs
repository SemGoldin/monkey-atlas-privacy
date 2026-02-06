using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the game UI including move counter, level info, and buttons
/// </summary>
public class UIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI moveCountText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI winMovesText;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button undoButton;
    
    [Header("Settings")]
    [SerializeField] private string moveCountFormat = "Moves: {0}";
    [SerializeField] private string winMessageFormat = "Level Complete!\nMoves: {0}";
    
    private GameManager gameManager;
    
    private void Awake()
    {
        gameManager = GameManager.Instance;
        
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        
        SetupButtons();
        HideWinPanel();
    }
    
    private void SetupButtons()
    {
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(OnResetClicked);
        }
        
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(OnNextLevelClicked);
        }
        
        if (undoButton != null)
        {
            undoButton.onClick.AddListener(OnUndoClicked);
            // Disable undo button for now (feature not implemented)
            undoButton.interactable = false;
        }
    }
    
    /// <summary>
    /// Update the move counter display
    /// </summary>
    public void UpdateMoveCount(int moves)
    {
        if (moveCountText != null)
        {
            moveCountText.text = string.Format(moveCountFormat, moves);
        }
    }
    
    /// <summary>
    /// Update the level name display
    /// </summary>
    public void UpdateLevelName(string levelName)
    {
        if (levelNameText != null)
        {
            levelNameText.text = levelName;
        }
    }
    
    /// <summary>
    /// Show the win panel with move count
    /// </summary>
    public void ShowWinPanel(int moves)
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            
            if (winMovesText != null)
            {
                winMovesText.text = string.Format(winMessageFormat, moves);
            }
        }
    }
    
    /// <summary>
    /// Hide the win panel
    /// </summary>
    public void HideWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }
    
    /// <summary>
    /// Called when reset button is clicked
    /// </summary>
    private void OnResetClicked()
    {
        HideWinPanel();
        
        if (gameManager != null)
        {
            gameManager.ResetLevel();
        }
        
        Debug.Log("Level reset");
    }
    
    /// <summary>
    /// Called when next level button is clicked
    /// </summary>
    private void OnNextLevelClicked()
    {
        HideWinPanel();
        
        if (gameManager != null)
        {
            // This would load next level - for now just restart
            gameManager.GenerateLevel();
        }
        
        Debug.Log("Next level");
    }
    
    /// <summary>
    /// Called when undo button is clicked
    /// </summary>
    private void OnUndoClicked()
    {
        // Undo functionality not implemented yet
        Debug.Log("Undo not implemented");
    }
    
    /// <summary>
    /// Enable or disable the undo button
    /// </summary>
    public void SetUndoButtonEnabled(bool enabled)
    {
        if (undoButton != null)
        {
            undoButton.interactable = enabled;
        }
    }
}
