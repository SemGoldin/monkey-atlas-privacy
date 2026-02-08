using UnityEngine;
using System.Collections.Generic;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Represents a bolt that can hold nuts
    /// </summary>
    public class Bolt : MonoBehaviour
    {
        [SerializeField] private int maxCapacity = 4;
        [SerializeField] private float nutSpacing = 0.3f;
        [SerializeField] private float unscrewHeight = 1f;
        
        private List<Nut> nuts = new List<Nut>();
        private Vector3 pivotPosition;
        private bool isSelected = false;
        
        public int MaxCapacity => maxCapacity;
        public int CurrentCount => nuts.Count;
        public bool IsEmpty => nuts.Count == 0;
        public bool IsFull => nuts.Count >= maxCapacity;
        public bool IsSelected => isSelected;
        
        private void Awake()
        {
            pivotPosition = transform.position;
        }
        
        /// <summary>
        /// Get the top nut on this bolt
        /// </summary>
        public Nut GetTopNut()
        {
            if (nuts.Count > 0)
            {
                return nuts[nuts.Count - 1];
            }
            return null;
        }
        
        /// <summary>
        /// Get the color of the top nut (if any)
        /// </summary>
        public NutColor? GetTopNutColor()
        {
            Nut topNut = GetTopNut();
            return topNut?.Color;
        }
        
        /// <summary>
        /// Add a nut to this bolt
        /// </summary>
        public bool AddNut(Nut nut)
        {
            if (IsFull)
            {
                return false;
            }
            
            nuts.Add(nut);
            return true;
        }
        
        /// <summary>
        /// Remove the top nut from this bolt
        /// </summary>
        public Nut RemoveTopNut()
        {
            if (IsEmpty)
            {
                return null;
            }
            
            Nut topNut = nuts[nuts.Count - 1];
            nuts.RemoveAt(nuts.Count - 1);
            return topNut;
        }
        
        /// <summary>
        /// Get the position where a nut should be placed at given index
        /// </summary>
        public Vector3 GetNutPosition(int index)
        {
            return pivotPosition + new Vector3(0f, index * nutSpacing, 0f);
        }
        
        /// <summary>
        /// Get the position where the next nut should be placed
        /// </summary>
        public Vector3 GetNextNutPosition()
        {
            return GetNutPosition(nuts.Count);
        }
        
        /// <summary>
        /// Get the unscrew position above this bolt
        /// </summary>
        public Vector3 GetUnscrewPosition()
        {
            return pivotPosition + new Vector3(0f, unscrewHeight, 0f);
        }
        
        /// <summary>
        /// Check if a nut of given color can be placed on this bolt
        /// </summary>
        public bool CanAcceptNut(NutColor nutColor)
        {
            // Empty bolt or not full
            if (IsEmpty || !IsFull)
            {
                // If bolt has nuts, check if top nut matches color
                if (!IsEmpty)
                {
                    NutColor? topColor = GetTopNutColor();
                    return topColor.HasValue && topColor.Value == nutColor;
                }
                // Empty bolt accepts any color
                return true;
            }
            
            return false;
        }
        
        /// <summary>
        /// Check if all nuts on this bolt are the same color
        /// </summary>
        public bool IsSorted()
        {
            if (IsEmpty) return true;
            
            NutColor firstColor = nuts[0].Color;
            foreach (Nut nut in nuts)
            {
                if (nut.Color != firstColor)
                {
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// Set the selected state
        /// </summary>
        public void SetSelected(bool selected)
        {
            isSelected = selected;
            // Visual feedback could be added here
        }
        
        /// <summary>
        /// Handle click on this bolt
        /// </summary>
        private void OnMouseDown()
        {
            GameManager.Instance?.OnBoltClicked(this);
        }
        
        /// <summary>
        /// Get all nuts on this bolt (for debugging/testing)
        /// </summary>
        public List<Nut> GetAllNuts()
        {
            return new List<Nut>(nuts);
        }
    }
}
