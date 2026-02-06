using UnityEngine;
using System.Collections;

/// <summary>
/// Base AI controller for ghost enemies with different behavior states
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class GhostAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float normalSpeed = 4f;
    [SerializeField] private float frightenedSpeed = 2f;
    [SerializeField] private float frightenedDuration = 10f;
    [SerializeField] private float scatterDuration = 7f;
    [SerializeField] private float chaseDuration = 20f;
    
    [Header("Ghost Type")]
    [SerializeField] private GhostType ghostType = GhostType.Blinky;
    
    [Header("Visual Settings")]
    [SerializeField] private Renderer ghostRenderer;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material frightenedMaterial;
    [SerializeField] private Material eatenMaterial;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip frightenedSound;

    // References
    private Rigidbody rb;
    private Transform player;
    private Vector3 startPosition;
    private Quaternion startRotation;

    // State management
    private GhostState currentState;
    private float stateTimer;
    private bool isEaten = false;
    private Vector3 targetPosition;
    private Vector3 moveDirection;

    // Scatter targets (corners of the maze)
    private Vector3 scatterTarget;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        
        startPosition = transform.position;
        startRotation = transform.rotation;
        
        // Set scatter target based on ghost type
        SetScatterTarget();
    }

    private void Start()
    {
        // Find player
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Start in scatter state
        ChangeState(GhostState.Scatter);
    }

    private void Update()
    {
        if (GameManager.Instance?.GetCurrentState() != GameState.Playing || isEaten)
        {
            return;
        }

        UpdateState();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance?.GetCurrentState() != GameState.Playing)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        if (isEaten)
        {
            ReturnToHome();
        }
        else
        {
            Move();
        }
    }

    private void UpdateState()
    {
        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0)
        {
            switch (currentState)
            {
                case GhostState.Scatter:
                    ChangeState(GhostState.Chase);
                    break;
                case GhostState.Chase:
                    ChangeState(GhostState.Scatter);
                    break;
                case GhostState.Frightened:
                    ChangeState(GhostState.Chase);
                    break;
            }
        }
    }

    private void ChangeState(GhostState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GhostState.Scatter:
                stateTimer = scatterDuration;
                UpdateVisuals(normalMaterial);
                break;
            case GhostState.Chase:
                stateTimer = chaseDuration;
                UpdateVisuals(normalMaterial);
                break;
            case GhostState.Frightened:
                stateTimer = frightenedDuration;
                UpdateVisuals(frightenedMaterial);
                PlaySound(frightenedSound);
                break;
        }
    }

    private void Move()
    {
        if (player == null) return;

        // Determine target based on state
        switch (currentState)
        {
            case GhostState.Scatter:
                targetPosition = scatterTarget;
                break;
            case GhostState.Chase:
                targetPosition = GetChaseTarget();
                break;
            case GhostState.Frightened:
                targetPosition = GetRandomTarget();
                break;
        }

        // Calculate direction to target
        Vector3 directionToTarget = (targetPosition - transform.position).normalized;
        directionToTarget.y = 0; // Keep movement on horizontal plane

        // Choose best direction at intersections
        Vector3 bestDirection = ChooseBestDirection(directionToTarget);
        
        if (bestDirection != Vector3.zero)
        {
            moveDirection = bestDirection;
        }

        // Apply movement
        float currentSpeed = (currentState == GhostState.Frightened) ? frightenedSpeed : normalSpeed;
        rb.velocity = moveDirection * currentSpeed;

        // Rotate towards movement direction
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    private Vector3 ChooseBestDirection(Vector3 targetDirection)
    {
        Vector3[] possibleDirections = new Vector3[]
        {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right
        };

        Vector3 bestDirection = moveDirection;
        float bestDot = -1f;

        foreach (Vector3 direction in possibleDirections)
        {
            // Don't allow reversing direction
            if (direction == -moveDirection && moveDirection != Vector3.zero)
            {
                continue;
            }

            // Check if path is clear
            if (CanMove(direction))
            {
                float dot = Vector3.Dot(direction, targetDirection);
                if (dot > bestDot)
                {
                    bestDot = dot;
                    bestDirection = direction;
                }
            }
        }

        return bestDirection;
    }

    private bool CanMove(Vector3 direction)
    {
        float rayDistance = 0.5f;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, direction, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return false;
            }
        }
        
        return true;
    }

    private Vector3 GetChaseTarget()
    {
        if (player == null) return transform.position;

        // Different chase behaviors based on ghost type
        switch (ghostType)
        {
            case GhostType.Blinky: // Direct chase
                return player.position;
            
            case GhostType.Pinky: // Ambush ahead of player
                PlayerController pc = player.GetComponent<PlayerController>();
                if (pc != null)
                {
                    return player.position + player.forward * 4f;
                }
                return player.position;
            
            case GhostType.Inky: // Complex targeting
                return player.position + (player.position - transform.position) * 0.5f;
            
            case GhostType.Clyde: // Chase if far, scatter if close
                float distance = Vector3.Distance(transform.position, player.position);
                return (distance > 8f) ? player.position : scatterTarget;
            
            default:
                return player.position;
        }
    }

    private Vector3 GetRandomTarget()
    {
        // Move randomly when frightened
        return transform.position + new Vector3(
            Random.Range(-10f, 10f),
            0,
            Random.Range(-10f, 10f)
        );
    }

    private void ReturnToHome()
    {
        // Move back to start position
        Vector3 direction = (startPosition - transform.position).normalized;
        rb.velocity = direction * normalSpeed * 1.5f;

        // Check if reached home
        if (Vector3.Distance(transform.position, startPosition) < 0.5f)
        {
            isEaten = false;
            ChangeState(GhostState.Chase);
            UpdateVisuals(normalMaterial);
        }
    }

    private void SetScatterTarget()
    {
        // Set corner targets based on ghost type
        switch (ghostType)
        {
            case GhostType.Blinky:
                scatterTarget = new Vector3(20, 0, 20);
                break;
            case GhostType.Pinky:
                scatterTarget = new Vector3(-20, 0, 20);
                break;
            case GhostType.Inky:
                scatterTarget = new Vector3(20, 0, -20);
                break;
            case GhostType.Clyde:
                scatterTarget = new Vector3(-20, 0, -20);
                break;
        }
    }

    public void EnterFrightenedState()
    {
        if (!isEaten)
        {
            ChangeState(GhostState.Frightened);
        }
    }

    public void GetEaten()
    {
        isEaten = true;
        UpdateVisuals(eatenMaterial);
    }

    public void ResetGhost()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        moveDirection = Vector3.forward;
        rb.velocity = Vector3.zero;
        isEaten = false;
        ChangeState(GhostState.Scatter);
    }

    private void UpdateVisuals(Material material)
    {
        if (ghostRenderer != null && material != null)
        {
            ghostRenderer.material = material;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Getters
    public bool IsEaten() => isEaten;
    public GhostState GetCurrentState() => currentState;
}

/// <summary>
/// Ghost behavior states
/// </summary>
public enum GhostState
{
    Scatter,    // Return to corner
    Chase,      // Chase player
    Frightened  // Run away from player
}

/// <summary>
/// Different ghost personalities
/// </summary>
public enum GhostType
{
    Blinky,  // Red - Direct chaser
    Pinky,   // Pink - Ambusher
    Inky,    // Cyan - Unpredictable
    Clyde    // Orange - Random
}
