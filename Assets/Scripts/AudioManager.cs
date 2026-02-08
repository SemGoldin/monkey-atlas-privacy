using UnityEngine;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Manages all audio in the game
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        
        [Header("Sound Effects")]
        [SerializeField] private AudioClip screwingSound;
        [SerializeField] private AudioClip unscrewingSound;
        [SerializeField] private AudioClip errorSound;
        [SerializeField] private AudioClip victorySound;
        [SerializeField] private AudioClip clickSound;
        
        [Header("Music")]
        [SerializeField] private AudioClip backgroundMusic;
        
        [Header("Settings")]
        [SerializeField] private float sfxVolume = 1f;
        [SerializeField] private float musicVolume = 0.5f;
        
        private AudioSource sfxSource;
        private AudioSource musicSource;
        
        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            
            // Setup audio sources
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.volume = sfxVolume;
            sfxSource.playOnAwake = false;
            
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.volume = musicVolume;
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }
        
        private void Start()
        {
            PlayBackgroundMusic();
        }
        
        /// <summary>
        /// Play screwing sound effect
        /// </summary>
        public void PlayScrewingSound()
        {
            if (screwingSound != null)
            {
                sfxSource.PlayOneShot(screwingSound);
            }
        }
        
        /// <summary>
        /// Play unscrewing sound effect
        /// </summary>
        public void PlayUnscrewingSound()
        {
            if (unscrewingSound != null)
            {
                sfxSource.PlayOneShot(unscrewingSound);
            }
        }
        
        /// <summary>
        /// Play error sound effect
        /// </summary>
        public void PlayErrorSound()
        {
            if (errorSound != null)
            {
                sfxSource.PlayOneShot(errorSound);
            }
        }
        
        /// <summary>
        /// Play victory sound effect
        /// </summary>
        public void PlayVictorySound()
        {
            if (victorySound != null)
            {
                sfxSource.PlayOneShot(victorySound);
            }
        }
        
        /// <summary>
        /// Play click sound effect
        /// </summary>
        public void PlayClickSound()
        {
            if (clickSound != null)
            {
                sfxSource.PlayOneShot(clickSound);
            }
        }
        
        /// <summary>
        /// Play background music
        /// </summary>
        public void PlayBackgroundMusic()
        {
            if (backgroundMusic != null && !musicSource.isPlaying)
            {
                musicSource.clip = backgroundMusic;
                musicSource.Play();
            }
        }
        
        /// <summary>
        /// Stop background music
        /// </summary>
        public void StopBackgroundMusic()
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
            }
        }
        
        /// <summary>
        /// Set SFX volume
        /// </summary>
        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
            sfxSource.volume = sfxVolume;
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        }
        
        /// <summary>
        /// Set music volume
        /// </summary>
        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            musicSource.volume = musicVolume;
            PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        }
        
        /// <summary>
        /// Load volume settings from PlayerPrefs
        /// </summary>
        private void LoadVolumeSettings()
        {
            if (PlayerPrefs.HasKey("SFXVolume"))
            {
                SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume"));
            }
            
            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
            }
        }
    }
}
