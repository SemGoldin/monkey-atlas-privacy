using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Nut : MonoBehaviour
{
    public int colorId;
    public Color nutColor;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    private Bolt currentBolt;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
    }
    
    public void Initialize(int colorId, Color color)
    {
        this.colorId = colorId;
        this.nutColor = color;
        
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        spriteRenderer.color = color;
    }
    
    public void SetCurrentBolt(Bolt bolt)
    {
        currentBolt = bolt;
    }
    
    public Bolt GetCurrentBolt()
    {
        return currentBolt;
    }
    
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        originalPosition = position;
    }
    
    public Vector3 GetOriginalPosition()
    {
        return originalPosition;
    }
    
    public void OnPickedUp()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 100; // Bring to front
        }
    }
    
    public void OnPlaced()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = 0; // Return to normal
        }
    }
}
