using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Game/Audio Configuration")]
public class AudioConfig : ScriptableObject
{
    [Header("UI Sounds")]
    public AudioClip buttonClick;
    public AudioClip levelComplete;
    public AudioClip levelFailed;
    
    [Header("Game Sounds")]
    public AudioClip nutPickup;
    public AudioClip nutPlace;
    public AudioClip boltComplete;
    public AudioClip invalidMove;
    
    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    
    [Header("Volume Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.5f;
}
