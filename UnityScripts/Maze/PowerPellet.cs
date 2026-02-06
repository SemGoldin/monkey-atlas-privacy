using UnityEngine;

/// <summary>
/// Power pellet that grants temporary invincibility and allows eating ghosts
/// </summary>
public class PowerPellet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int pointValue = 50;
    [SerializeField] private float rotationSpeed = 120f;
    [SerializeField] private float pulseSpeed = 3f;
    [SerializeField] private float pulseScale = 0.3f;
    
    [Header("Visual Effects")]
    [SerializeField] private ParticleSystem collectEffect;
    [SerializeField] private Light pointLight;
    
    private MazeGenerator mazeGenerator;
    private bool isCollected = false;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Rotate the power pellet
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        // Pulse scale
        float scale = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseScale;
        transform.localScale = originalScale * scale;
        
        // Pulse the light more intensely than regular pellets
        if (pointLight != null)
        {
            float pulse = Mathf.Sin(Time.time * 3f) * 0.5f + 1f;
            pointLight.intensity = pulse * 2f;
        }
    }

    public void Collect()
    {
        if (isCollected) return;
        
        isCollected = true;
        
        // Add score
        ScoreManager.Instance?.AddScore(pointValue);
        
        // Play collect effect
        if (collectEffect != null)
        {
            ParticleSystem effect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            // Make the effect larger for power pellets
            effect.transform.localScale = Vector3.one * 2f;
        }
        
        // Notify maze generator
        if (mazeGenerator != null)
        {
            mazeGenerator.OnPelletCollected();
        }
        
        // Destroy the power pellet
        Destroy(gameObject);
    }

    public void SetMazeGenerator(MazeGenerator generator)
    {
        mazeGenerator = generator;
    }
}
