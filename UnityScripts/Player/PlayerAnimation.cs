using UnityEngine;

/// <summary>
/// Handles player visual animations and effects
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobAmount = 0.1f;
    
    [Header("Visual Effects")]
    [SerializeField] private GameObject model;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material poweredUpMaterial;
    [SerializeField] private ParticleSystem deathParticles;

    private Renderer meshRenderer;
    private Vector3 originalPosition;
    private bool isMoving = false;
    private float bobTimer = 0f;

    private void Awake()
    {
        if (model != null)
        {
            meshRenderer = model.GetComponent<Renderer>();
        }
        
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        if (isMoving)
        {
            ApplyBobbing();
        }
    }

    private void ApplyBobbing()
    {
        // Create a bobbing motion while moving
        bobTimer += Time.deltaTime * bobSpeed;
        float newY = originalPosition.y + Mathf.Sin(bobTimer) * bobAmount;
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            newY,
            transform.localPosition.z
        );
    }

    public void SetMoving(bool moving)
    {
        isMoving = moving;
        
        if (!moving)
        {
            transform.localPosition = originalPosition;
            bobTimer = 0f;
        }

        // Update animator if available
        if (animator != null)
        {
            animator.SetBool("IsMoving", moving);
        }
    }

    public void SetPoweredUp(bool poweredUp)
    {
        if (meshRenderer != null)
        {
            if (poweredUp && poweredUpMaterial != null)
            {
                meshRenderer.material = poweredUpMaterial;
            }
            else if (normalMaterial != null)
            {
                meshRenderer.material = normalMaterial;
            }
        }

        if (animator != null)
        {
            animator.SetBool("IsPoweredUp", poweredUp);
        }
    }

    public void PlayDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Play death particles
        if (deathParticles != null)
        {
            deathParticles.Play();
        }

        // Optionally hide the model
        if (model != null)
        {
            model.SetActive(false);
        }
    }

    public void ResetAnimation()
    {
        transform.localPosition = originalPosition;
        bobTimer = 0f;
        isMoving = false;

        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsPoweredUp", false);
        }

        if (model != null)
        {
            model.SetActive(true);
        }

        // Reset material
        if (meshRenderer != null && normalMaterial != null)
        {
            meshRenderer.material = normalMaterial;
        }
    }
}
