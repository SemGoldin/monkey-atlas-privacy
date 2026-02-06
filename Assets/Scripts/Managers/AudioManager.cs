using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("AudioManager");
                instance = go.AddComponent<AudioManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    
    [SerializeField] private AudioConfig audioConfig;
    
    private AudioSource musicSource;
    private List<AudioSource> sfxSources = new List<AudioSource>();
    private int maxSfxSources = 10;
    
    private bool soundEnabled = true;
    private bool musicEnabled = true;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitializeAudioSources();
        LoadAudioSettings();
    }
    
    private void InitializeAudioSources()
    {
        // Create music source
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        
        // Create SFX sources
        for (int i = 0; i < maxSfxSources; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            sfxSources.Add(source);
        }
    }
    
    public void SetAudioConfig(AudioConfig config)
    {
        audioConfig = config;
        UpdateVolumes();
    }
    
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || !musicEnabled) return;
        
        if (musicSource.clip == clip && musicSource.isPlaying) return;
        
        musicSource.clip = clip;
        musicSource.volume = audioConfig != null ? audioConfig.musicVolume * audioConfig.masterVolume : 0.5f;
        musicSource.Play();
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
    
    public void PlaySFX(AudioClip clip, float volumeMultiplier = 1f)
    {
        if (clip == null || !soundEnabled) return;
        
        AudioSource availableSource = GetAvailableSFXSource();
        if (availableSource != null)
        {
            float volume = audioConfig != null ? audioConfig.sfxVolume * audioConfig.masterVolume * volumeMultiplier : volumeMultiplier;
            availableSource.PlayOneShot(clip, volume);
        }
    }
    
    private AudioSource GetAvailableSFXSource()
    {
        foreach (AudioSource source in sfxSources)
        {
            if (!source.isPlaying)
                return source;
        }
        return sfxSources[0]; // Return first source if all are busy
    }
    
    public void PlayButtonClick()
    {
        if (audioConfig != null) PlaySFX(audioConfig.buttonClick);
    }
    
    public void PlayNutPickup()
    {
        if (audioConfig != null) PlaySFX(audioConfig.nutPickup);
    }
    
    public void PlayNutPlace()
    {
        if (audioConfig != null) PlaySFX(audioConfig.nutPlace);
    }
    
    public void PlayBoltComplete()
    {
        if (audioConfig != null) PlaySFX(audioConfig.boltComplete);
    }
    
    public void PlayInvalidMove()
    {
        if (audioConfig != null) PlaySFX(audioConfig.invalidMove);
    }
    
    public void PlayLevelComplete()
    {
        if (audioConfig != null) PlaySFX(audioConfig.levelComplete);
    }
    
    public void PlayLevelFailed()
    {
        if (audioConfig != null) PlaySFX(audioConfig.levelFailed);
    }
    
    public void SetSoundEnabled(bool enabled)
    {
        soundEnabled = enabled;
        PlayerPrefs.SetInt("SoundEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
        
        if (!enabled)
            musicSource.Pause();
        else
            musicSource.UnPause();
    }
    
    public bool IsSoundEnabled() => soundEnabled;
    public bool IsMusicEnabled() => musicEnabled;
    
    private void LoadAudioSettings()
    {
        soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
    }
    
    private void UpdateVolumes()
    {
        if (audioConfig == null) return;
        
        musicSource.volume = audioConfig.musicVolume * audioConfig.masterVolume;
    }
}
