using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// Represents a bolt that holds a stack of nuts
/// </summary>
public class Bolt : MonoBehaviour
{
    [SerializeField] private int maxCapacity = 4;
    [SerializeField] private float nutSpacing = 0.3f;
    [SerializeField] private Transform nutsContainer;
    [SerializeField] private MeshRenderer boltRenderer;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material validMoveMaterial;
    [SerializeField] private Material invalidMoveMaterial;
    
    private List<Nut> nuts = new List<Nut>();
    private Material currentMaterial;

    public int MaxCapacity => maxCapacity;
    public int CurrentCount => nuts.Count;
    public bool IsFull => nuts.Count >= maxCapacity;
    public bool IsEmpty => nuts.Count == 0;

    private void Awake()
    {
        if (nutsContainer == null)
        {
            nutsContainer = transform;
        }
        
        if (boltRenderer != null && normalMaterial != null)
        {
            currentMaterial = new Material(normalMaterial);
            boltRenderer.material = currentMaterial;
        }
    }

    /// <summary>
    /// Get the top nut from this bolt
    /// </summary>
    public Nut GetTopNut()
    {
        if (IsEmpty)
            return null;
        
        return nuts[nuts.Count - 1];
    }

    /// <summary>
    /// Get the color of the top nut, or null if empty
    /// </summary>
    public NutColor? GetTopNutColor()
    {
        var topNut = GetTopNut();
        return topNut != null ? (NutColor?)topNut.Color : null;
    }

    /// <summary>
    /// Check if a nut can be placed on this bolt
    /// </summary>
    public bool CanPlaceNut(Nut nut)
    {
        if (nut == null)
            return false;
        
        // Can't place if bolt is full
        if (IsFull)
            return false;
        
        // Can place on empty bolt
        if (IsEmpty)
            return true;
        
        // Can place if top nut is same color
        var topNut = GetTopNut();
        return topNut.Color == nut.Color;
    }

    /// <summary>
    /// Remove the top nut from this bolt
    /// </summary>
    public Nut RemoveTopNut()
    {
        if (IsEmpty)
            return null;
        
        var topNut = nuts[nuts.Count - 1];
        nuts.RemoveAt(nuts.Count - 1);
        return topNut;
    }

    /// <summary>
    /// Add a nut to the top of this bolt
    /// </summary>
    public void AddNut(Nut nut)
    {
        if (nut == null || IsFull)
            return;
        
        nuts.Add(nut);
        nut.ParentBolt = this;
        nut.transform.SetParent(nutsContainer);
    }

    /// <summary>
    /// Get the world position where the next nut should be placed
    /// </summary>
    public Vector3 GetNextNutPosition()
    {
        return nutsContainer.position + Vector3.up * (nuts.Count * nutSpacing);
    }

    /// <summary>
    /// Check if this bolt is sorted (all nuts are same color or empty)
    /// </summary>
    public bool IsSorted()
    {
        if (IsEmpty)
            return true;
        
        if (nuts.Count == 1)
            return true;
        
        NutColor firstColor = nuts[0].Color;
        foreach (var nut in nuts)
        {
            if (nut.Color != firstColor)
                return false;
        }
        
        return true;
    }

    /// <summary>
    /// Check if bolt is complete (full and sorted)
    /// </summary>
    public bool IsComplete()
    {
        return IsFull && IsSorted();
    }

    /// <summary>
    /// Highlight bolt to indicate valid move
    /// </summary>
    public void HighlightAsValid()
    {
        if (boltRenderer != null && validMoveMaterial != null)
        {
            boltRenderer.material = validMoveMaterial;
            
            // Subtle pulse animation
            boltRenderer.transform.DOScale(1.1f, 0.3f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutQuad);
        }
    }

    /// <summary>
    /// Highlight bolt to indicate invalid move
    /// </summary>
    public void HighlightAsInvalid()
    {
        if (boltRenderer != null && invalidMoveMaterial != null)
        {
            boltRenderer.material = invalidMoveMaterial;
            
            // Shake animation to indicate invalid
            boltRenderer.transform.DOShakePosition(0.3f, 0.1f, 10, 90, false, true);
        }
    }

    /// <summary>
    /// Reset bolt highlighting to normal
    /// </summary>
    public void ResetHighlight()
    {
        if (boltRenderer != null && normalMaterial != null)
        {
            boltRenderer.material = normalMaterial;
            boltRenderer.transform.DOKill();
            boltRenderer.transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// Initialize bolt with starting nuts
    /// </summary>
    public void Initialize(List<NutColor> startingNuts)
    {
        // Clear existing nuts
        foreach (var nut in nuts)
        {
            if (nut != null)
                Destroy(nut.gameObject);
        }
        nuts.Clear();
        
        // Add starting nuts (will be populated by GameManager with actual nut objects)
    }

    private void OnDestroy()
    {
        if (boltRenderer != null)
        {
            boltRenderer.transform.DOKill();
        }
    }
}
