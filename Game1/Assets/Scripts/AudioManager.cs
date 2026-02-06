using UnityEngine;

/// <summary>
/// Manages audio and sound effects for the game
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] private AudioClip unscrewSound;
    [SerializeField] private AudioClip screwSound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip validMoveSound;
    [SerializeField] private AudioClip invalidMoveSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip clickSound;
    
    [Header("Settings")]
    [SerializeField] private float soundEffectVolume = 1.0f;
    [SerializeField] private bool soundEnabled = true;
    
    private AudioSource audioSource;
    
    public static AudioManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = soundEffectVolume;
    }
    
    /// <summary>
    /// Play sound effect for unscrewing a nut
    /// </summary>
    public void PlayUnscrewSound()
    {
        PlaySound(unscrewSound);
    }
    
    /// <summary>
    /// Play sound effect for screwing down a nut
    /// </summary>
    public void PlayScrewSound()
    {
        PlaySound(screwSound);
    }
    
    /// <summary>
    /// Play sound effect for nut hovering
    /// </summary>
    public void PlayHoverSound()
    {
        PlaySound(hoverSound, 0.5f); // Lower volume for ambient sound
    }
    
    /// <summary>
    /// Play sound effect for valid move
    /// </summary>
    public void PlayValidMoveSound()
    {
        PlaySound(validMoveSound);
    }
    
    /// <summary>
    /// Play sound effect for invalid move
    /// </summary>
    public void PlayInvalidMoveSound()
    {
        PlaySound(invalidMoveSound);
    }
    
    /// <summary>
    /// Play sound effect for winning
    /// </summary>
    public void PlayWinSound()
    {
        PlaySound(winSound);
    }
    
    /// <summary>
    /// Play sound effect for UI click
    /// </summary>
    public void PlayClickSound()
    {
        PlaySound(clickSound);
    }
    
    /// <summary>
    /// Play a sound effect with optional volume override
    /// </summary>
    private void PlaySound(AudioClip clip, float? volumeOverride = null)
    {
        if (!soundEnabled || audioSource == null || clip == null)
            return;
        
        float volume = volumeOverride ?? soundEffectVolume;
        audioSource.PlayOneShot(clip, volume);
    }
    
    /// <summary>
    /// Enable or disable all sound effects
    /// </summary>
    public void SetSoundEnabled(bool enabled)
    {
        soundEnabled = enabled;
    }
    
    /// <summary>
    /// Set the volume for sound effects (0-1)
    /// </summary>
    public void SetVolume(float volume)
    {
        soundEffectVolume = Mathf.Clamp01(volume);
        if (audioSource != null)
        {
            audioSource.volume = soundEffectVolume;
        }
    }
}
