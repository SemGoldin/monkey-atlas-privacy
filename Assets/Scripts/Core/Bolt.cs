using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Bolt : MonoBehaviour
{
    [SerializeField] private int capacity = 4;
    [SerializeField] private float nutSpacing = 0.3f;
    
    private List<Nut> nuts = new List<Nut>();
    private SpriteRenderer boltRenderer;
    private Vector3 basePosition;
    
    private void Awake()
    {
        boltRenderer = GetComponent<SpriteRenderer>();
        if (boltRenderer == null)
        {
            boltRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        basePosition = transform.position;
    }
    
    public void Initialize(int capacity)
    {
        this.capacity = capacity;
        nuts.Clear();
    }
    
    public bool CanAddNut(Nut nut)
    {
        if (nuts.Count >= capacity) return false;
        if (nuts.Count == 0) return true;
        
        // Can only add if all nuts on bolt are the same color
        return nuts.All(n => n.colorId == nut.colorId);
    }
    
    public void AddNut(Nut nut)
    {
        nuts.Add(nut);
        nut.SetCurrentBolt(this);
        PositionNut(nut, nuts.Count - 1);
    }
    
    public Nut RemoveTopNut()
    {
        if (nuts.Count == 0) return null;
        
        Nut nut = nuts[nuts.Count - 1];
        nuts.RemoveAt(nuts.Count - 1);
        nut.SetCurrentBolt(null);
        
        return nut;
    }
    
    public Nut GetTopNut()
    {
        if (nuts.Count == 0) return null;
        return nuts[nuts.Count - 1];
    }
    
    public int GetNutCount()
    {
        return nuts.Count;
    }
    
    public bool IsFull()
    {
        return nuts.Count >= capacity;
    }
    
    public bool IsEmpty()
    {
        return nuts.Count == 0;
    }
    
    public bool IsComplete()
    {
        if (nuts.Count != capacity) return false;
        if (nuts.Count == 0) return false;
        
        int firstColorId = nuts[0].colorId;
        return nuts.All(n => n.colorId == firstColorId);
    }
    
    public List<Nut> GetNuts()
    {
        return new List<Nut>(nuts);
    }
    
    private void PositionNut(Nut nut, int index)
    {
        Vector3 position = basePosition + Vector3.up * (index * nutSpacing);
        nut.SetPosition(position);
    }
    
    public void RepositionAllNuts()
    {
        for (int i = 0; i < nuts.Count; i++)
        {
            PositionNut(nuts[i], i);
        }
    }
    
    public Vector3 GetNextNutPosition()
    {
        return basePosition + Vector3.up * (nuts.Count * nutSpacing);
    }
    
    public void SetBoltColor(Color color)
    {
        if (boltRenderer != null)
        {
            boltRenderer.color = color;
        }
    }
}
