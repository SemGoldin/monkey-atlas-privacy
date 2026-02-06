using UnityEngine;
using System.Collections;

/// <summary>
/// Controls player movement, input handling, and collision detection
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f;
    [SerializeField] private float acceleration = 10f;
    
    [Header("Animation")]
    [SerializeField] private PlayerAnimation playerAnimation;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip movementSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip eatPelletSound;
    [SerializeField] private AudioClip eatGhostSound;

    private Rigidbody rb;
    private Vector3 moveDirection;
    private Vector3 nextDirection;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool isAlive = true;
    private float currentSpeed;

    // Power-up state
    private bool isPoweredUp = false;
    private float powerUpTimer = 0f;
    private float powerUpDuration = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;
        
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.loop = true;
        audioSource.clip = movementSound;
    }

    private void Update()
    {
        if (!isAlive || GameManager.Instance?.GetCurrentState() != GameState.Playing)
        {
            return;
        }

        HandleInput();
        UpdatePowerUpState();
    }

    private void FixedUpdate()
    {
        if (!isAlive || GameManager.Instance?.GetCurrentState() != GameState.Playing)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        MovePlayer();
    }

    private void HandleInput()
    {
        // Get input from keyboard or controller
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Store the desired direction
        if (horizontal != 0 || vertical != 0)
        {
            nextDirection = new Vector3(horizontal, 0, vertical).normalized;
        }

        // Try to change direction if possible
        if (nextDirection != Vector3.zero && CanMove(nextDirection))
        {
            moveDirection = nextDirection;
        }

        // Pause game
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance?.PauseGame();
        }
    }

    private bool CanMove(Vector3 direction)
    {
        // Raycast to check if the path is clear
        float rayDistance = 0.5f;
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, direction, out hit, rayDistance))
        {
            // Check if we hit a wall
            if (hit.collider.CompareTag("Wall"))
            {
                return false;
            }
        }
        
        return true;
    }

    private void MovePlayer()
    {
        if (moveDirection != Vector3.zero && CanMove(moveDirection))
        {
            // Accelerate to move speed
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, acceleration * Time.fixedDeltaTime);
            
            // Move the player
            Vector3 velocity = moveDirection * currentSpeed;
            rb.velocity = velocity;

            // Rotate towards movement direction
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation, 
                    targetRotation, 
                    rotationSpeed * Time.fixedDeltaTime
                );
            }

            // Play movement sound
            if (!audioSource.isPlaying && movementSound != null)
            {
                audioSource.Play();
            }

            // Update animation
            if (playerAnimation != null)
            {
                playerAnimation.SetMoving(true);
            }
        }
        else
        {
            // Decelerate
            currentSpeed = Mathf.Lerp(currentSpeed, 0, acceleration * Time.fixedDeltaTime);
            rb.velocity = Vector3.zero;

            // Stop movement sound
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // Update animation
            if (playerAnimation != null)
            {
                playerAnimation.SetMoving(false);
            }
        }
    }

    private void UpdatePowerUpState()
    {
        if (isPoweredUp)
        {
            powerUpTimer -= Time.deltaTime;
            
            if (powerUpTimer <= 0)
            {
                EndPowerUp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAlive) return;

        // Collect pellet
        if (other.CompareTag("Pellet"))
        {
            Pellet pellet = other.GetComponent<Pellet>();
            if (pellet != null)
            {
                pellet.Collect();
                PlaySound(eatPelletSound);
            }
        }
        // Collect power pellet
        else if (other.CompareTag("PowerPellet"))
        {
            PowerPellet powerPellet = other.GetComponent<PowerPellet>();
            if (powerPellet != null)
            {
                powerPellet.Collect();
                ActivatePowerUp();
                PlaySound(eatPelletSound);
            }
        }
        // Collision with ghost
        else if (other.CompareTag("Ghost"))
        {
            GhostAI ghost = other.GetComponent<GhostAI>();
            if (ghost != null)
            {
                if (isPoweredUp && !ghost.IsEaten())
                {
                    // Eat the ghost
                    ghost.GetEaten();
                    PlaySound(eatGhostSound);
                    ScoreManager.Instance?.AddGhostScore();
                }
                else if (!ghost.IsEaten())
                {
                    // Player dies
                    Die();
                }
            }
        }
    }

    private void ActivatePowerUp()
    {
        isPoweredUp = true;
        powerUpTimer = powerUpDuration;
        
        // Notify ghosts to enter frightened state
        var ghosts = GameManager.Instance?.GetGhosts();
        if (ghosts != null)
        {
            foreach (var ghost in ghosts)
            {
                ghost.EnterFrightenedState();
            }
        }

        // Visual feedback (could add glow effect here)
        if (playerAnimation != null)
        {
            playerAnimation.SetPoweredUp(true);
        }
    }

    private void EndPowerUp()
    {
        isPoweredUp = false;
        
        // Visual feedback
        if (playerAnimation != null)
        {
            playerAnimation.SetPoweredUp(false);
        }
    }

    private void Die()
    {
        if (!isAlive) return;

        isAlive = false;
        moveDirection = Vector3.zero;
        rb.velocity = Vector3.zero;

        // Play death sound
        PlaySound(deathSound);

        // Death animation
        if (playerAnimation != null)
        {
            playerAnimation.PlayDeathAnimation();
        }

        // Stop movement sound
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Notify game manager
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance?.OnPlayerDeath();
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        moveDirection = Vector3.zero;
        nextDirection = Vector3.zero;
        rb.velocity = Vector3.zero;
        currentSpeed = 0;
        isAlive = true;
        isPoweredUp = false;
        powerUpTimer = 0;

        if (playerAnimation != null)
        {
            playerAnimation.ResetAnimation();
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Getters
    public bool IsAlive() => isAlive;
    public bool IsPoweredUp() => isPoweredUp;
    public Vector3 GetPosition() => transform.position;
}
