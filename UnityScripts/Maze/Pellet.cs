using UnityEngine;

/// <summary>
/// Regular pellet collectible that gives points
/// </summary>
public class Pellet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int pointValue = 10;
    [SerializeField] private float rotationSpeed = 90f;
    
    [Header("Visual Effects")]
    [SerializeField] private ParticleSystem collectEffect;
    [SerializeField] private Light pointLight;
    
    private MazeGenerator mazeGenerator;
    private bool isCollected = false;

    private void Update()
    {
        // Rotate the pellet for visual appeal
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        
        // Pulse the light if it exists
        if (pointLight != null)
        {
            float pulse = Mathf.Sin(Time.time * 2f) * 0.3f + 0.7f;
            pointLight.intensity = pulse;
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
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
        
        // Notify maze generator
        if (mazeGenerator != null)
        {
            mazeGenerator.OnPelletCollected();
        }
        
        // Destroy the pellet
        Destroy(gameObject);
    }

    public void SetMazeGenerator(MazeGenerator generator)
    {
        mazeGenerator = generator;
    }
}
