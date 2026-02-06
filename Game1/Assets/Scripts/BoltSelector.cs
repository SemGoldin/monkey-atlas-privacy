using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Handles user input and manages nut selection and movement between bolts
/// </summary>
public class BoltSelector : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField] private LayerMask boltLayerMask = ~0;
    [SerializeField] private float raycastDistance = 100f;
    
    private List<Bolt> bolts = new List<Bolt>();
    private Bolt selectedBolt;
    private Nut selectedNut;
    private Camera mainCamera;
    
    private enum SelectionState
    {
        Idle,           // No nut selected
        NutSelected,    // Nut is picked up and hovering
        Moving          // Nut is animating to new position
    }
    
    private SelectionState currentState = SelectionState.Idle;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    /// <summary>
    /// Initialize with list of bolts in the game
    /// </summary>
    public void Initialize(List<Bolt> gameBolts)
    {
        bolts = gameBolts;
        currentState = SelectionState.Idle;
        selectedBolt = null;
        selectedNut = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }
    }

    /// <summary>
    /// Handle mouse click based on current state
    /// </summary>
    private void HandleMouseClick()
    {
        Bolt clickedBolt = GetBoltUnderMouse();
        
        if (clickedBolt == null)
            return;

        switch (currentState)
        {
            case SelectionState.Idle:
                TrySelectNut(clickedBolt);
                break;
                
            case SelectionState.NutSelected:
                TryPlaceNut(clickedBolt);
                break;
                
            case SelectionState.Moving:
                // Ignore clicks while animating
                break;
        }
    }

    /// <summary>
    /// Try to select a nut from the clicked bolt
    /// </summary>
    private void TrySelectNut(Bolt bolt)
    {
        if (bolt.IsEmpty)
        {
            Debug.Log("Bolt is empty, cannot select nut");
            return;
        }

        // Get top nut from bolt
        selectedNut = bolt.RemoveTopNut();
        selectedBolt = bolt;
        
        if (selectedNut != null)
        {
            currentState = SelectionState.Moving;
            
            // Animate unscrewing
            selectedNut.AnimateUnscrew(() =>
            {
                currentState = SelectionState.NutSelected;
                HighlightValidMoves();
            });
            
            Debug.Log($"Selected {selectedNut.Color} nut from bolt");
        }
    }

    /// <summary>
    /// Try to place the selected nut on the clicked bolt
    /// </summary>
    private void TryPlaceNut(Bolt targetBolt)
    {
        // If clicking the same bolt, return nut to original position
        if (targetBolt == selectedBolt)
        {
            ReturnNutToOriginalBolt();
            return;
        }

        // Check if move is valid
        if (!targetBolt.CanPlaceNut(selectedNut))
        {
            // Invalid move - show feedback
            targetBolt.HighlightAsInvalid();
            
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayInvalidMoveSound();
            }
            
            Debug.Log($"Cannot place {selectedNut.Color} nut on bolt - invalid move");
            
            // Reset highlight after a moment
            Invoke(nameof(ResetAllHighlights), 0.5f);
            return;
        }

        // Valid move - animate and place nut
        PlaceNutOnBolt(targetBolt);
    }

    /// <summary>
    /// Place the selected nut on the target bolt
    /// </summary>
    private void PlaceNutOnBolt(Bolt targetBolt)
    {
        currentState = SelectionState.Moving;
        ResetAllHighlights();
        
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayValidMoveSound();
        }
        
        Vector3 targetPosition = targetBolt.GetNextNutPosition();
        
        selectedNut.AnimateMoveToBolt(targetBolt, targetPosition, () =>
        {
            // Add nut to target bolt
            targetBolt.AddNut(selectedNut);
            
            // Record move
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RecordMove();
            }
            
            // Reset selection
            selectedNut = null;
            selectedBolt = null;
            currentState = SelectionState.Idle;
            
            Debug.Log($"Nut placed successfully");
        });
    }

    /// <summary>
    /// Return the selected nut to its original bolt
    /// </summary>
    private void ReturnNutToOriginalBolt()
    {
        if (selectedNut == null || selectedBolt == null)
            return;

        currentState = SelectionState.Moving;
        ResetAllHighlights();
        
        selectedNut.AnimateReturnToBolt(() =>
        {
            // Add nut back to original bolt
            selectedBolt.AddNut(selectedNut);
            
            // Reset selection
            selectedNut = null;
            selectedBolt = null;
            currentState = SelectionState.Idle;
            
            Debug.Log("Nut returned to original bolt");
        });
    }

    /// <summary>
    /// Highlight bolts that can accept the currently selected nut
    /// </summary>
    private void HighlightValidMoves()
    {
        if (selectedNut == null)
            return;

        foreach (Bolt bolt in bolts)
        {
            if (bolt == selectedBolt)
                continue; // Skip the original bolt
            
            if (bolt.CanPlaceNut(selectedNut))
            {
                bolt.HighlightAsValid();
            }
        }
    }

    /// <summary>
    /// Reset all bolt highlights to normal
    /// </summary>
    private void ResetAllHighlights()
    {
        foreach (Bolt bolt in bolts)
        {
            bolt.ResetHighlight();
        }
    }

    /// <summary>
    /// Get the bolt under the mouse cursor using raycast
    /// </summary>
    private Bolt GetBoltUnderMouse()
    {
        if (mainCamera == null)
            return null;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, raycastDistance, boltLayerMask))
        {
            // Check if hit object or parent has Bolt component
            Bolt bolt = hit.collider.GetComponent<Bolt>();
            if (bolt == null)
            {
                bolt = hit.collider.GetComponentInParent<Bolt>();
            }
            
            return bolt;
        }
        
        return null;
    }

    /// <summary>
    /// Cancel current selection and return nut if one is selected
    /// </summary>
    public void CancelSelection()
    {
        if (currentState == SelectionState.NutSelected && selectedNut != null && selectedBolt != null)
        {
            ReturnNutToOriginalBolt();
        }
    }
}
