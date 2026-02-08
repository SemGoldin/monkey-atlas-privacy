using UnityEngine;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Manages game settings and preferences
    /// </summary>
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance { get; private set; }
        
        // Settings keys
        private const string SFX_VOLUME_KEY = "SFXVolume";
        private const string MUSIC_VOLUME_KEY = "MusicVolume";
        private const string VIBRATION_KEY = "VibrationEnabled";
        private const string TUTORIAL_KEY = "TutorialCompleted";
        
        // Default values
        private float defaultSFXVolume = 1f;
        private float defaultMusicVolume = 0.5f;
        private bool defaultVibration = true;
        
        // Current settings
        private float sfxVolume;
        private float musicVolume;
        private bool vibrationEnabled;
        private bool tutorialCompleted;
        
        public float SFXVolume => sfxVolume;
        public float MusicVolume => musicVolume;
        public bool VibrationEnabled => vibrationEnabled;
        public bool TutorialCompleted => tutorialCompleted;
        
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
            
            LoadSettings();
        }
        
        /// <summary>
        /// Load settings from PlayerPrefs
        /// </summary>
        private void LoadSettings()
        {
            sfxVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, defaultSFXVolume);
            musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, defaultMusicVolume);
            vibrationEnabled = PlayerPrefs.GetInt(VIBRATION_KEY, defaultVibration ? 1 : 0) == 1;
            tutorialCompleted = PlayerPrefs.GetInt(TUTORIAL_KEY, 0) == 1;
        }
        
        /// <summary>
        /// Save settings to PlayerPrefs
        /// </summary>
        private void SaveSettings()
        {
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, musicVolume);
            PlayerPrefs.SetInt(VIBRATION_KEY, vibrationEnabled ? 1 : 0);
            PlayerPrefs.SetInt(TUTORIAL_KEY, tutorialCompleted ? 1 : 0);
            PlayerPrefs.Save();
        }
        
        /// <summary>
        /// Set SFX volume
        /// </summary>
        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
            SaveSettings();
            
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetSFXVolume(sfxVolume);
            }
        }
        
        /// <summary>
        /// Set music volume
        /// </summary>
        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            SaveSettings();
            
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetMusicVolume(musicVolume);
            }
        }
        
        /// <summary>
        /// Toggle vibration
        /// </summary>
        public void SetVibration(bool enabled)
        {
            vibrationEnabled = enabled;
            SaveSettings();
        }
        
        /// <summary>
        /// Mark tutorial as completed
        /// </summary>
        public void CompleteTutorial()
        {
            tutorialCompleted = true;
            SaveSettings();
        }
        
        /// <summary>
        /// Reset all settings to default
        /// </summary>
        public void ResetToDefault()
        {
            sfxVolume = defaultSFXVolume;
            musicVolume = defaultMusicVolume;
            vibrationEnabled = defaultVibration;
            tutorialCompleted = false;
            
            SaveSettings();
            
            // Apply to audio manager
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.SetSFXVolume(sfxVolume);
                AudioManager.Instance.SetMusicVolume(musicVolume);
            }
        }
    }
}
