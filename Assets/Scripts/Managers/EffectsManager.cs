using UnityEngine;
using System.Collections.Generic;

public class EffectsManager : MonoBehaviour
{
    private static EffectsManager instance;
    public static EffectsManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("EffectsManager");
                instance = go.AddComponent<EffectsManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }
    
    [Header("Particle Prefabs")]
    [SerializeField] private GameObject nutPickupEffect;
    [SerializeField] private GameObject nutPlaceEffect;
    [SerializeField] private GameObject boltCompleteEffect;
    [SerializeField] private GameObject starEffect;
    [SerializeField] private GameObject levelCompleteEffect;
    
    [Header("Effect Settings")]
    [SerializeField] private bool effectsEnabled = true;
    [SerializeField] private float effectLifetime = 2f;
    
    private Queue<GameObject> effectPool = new Queue<GameObject>();
    private int poolSize = 20;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        LoadEffectSettings();
    }
    
    private void LoadEffectSettings()
    {
        effectsEnabled = PlayerPrefs.GetInt("EffectsEnabled", 1) == 1;
    }
    
    public void SetEffectsEnabled(bool enabled)
    {
        effectsEnabled = enabled;
        PlayerPrefs.SetInt("EffectsEnabled", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }
    
    public bool AreEffectsEnabled() => effectsEnabled;
    
    public void PlayNutPickupEffect(Vector3 position)
    {
        if (!effectsEnabled || nutPickupEffect == null) return;
        PlayEffect(nutPickupEffect, position);
    }
    
    public void PlayNutPlaceEffect(Vector3 position)
    {
        if (!effectsEnabled || nutPlaceEffect == null) return;
        PlayEffect(nutPlaceEffect, position);
    }
    
    public void PlayBoltCompleteEffect(Vector3 position)
    {
        if (!effectsEnabled || boltCompleteEffect == null) return;
        PlayEffect(boltCompleteEffect, position);
    }
    
    public void PlayStarEffect(Vector3 position)
    {
        if (!effectsEnabled || starEffect == null) return;
        PlayEffect(starEffect, position);
    }
    
    public void PlayLevelCompleteEffect(Vector3 position)
    {
        if (!effectsEnabled || levelCompleteEffect == null) return;
        PlayEffect(levelCompleteEffect, position);
    }
    
    private void PlayEffect(GameObject effectPrefab, Vector3 position)
    {
        GameObject effect = Instantiate(effectPrefab, position, Quaternion.identity);
        Destroy(effect, effectLifetime);
    }
    
    // Animation helpers
    public void AnimateScaleBounce(Transform target, float duration = 0.3f, float scaleFactor = 1.2f)
    {
        if (!effectsEnabled || target == null) return;
        StartCoroutine(ScaleBounceCoroutine(target, duration, scaleFactor));
    }
    
    private System.Collections.IEnumerator ScaleBounceCoroutine(Transform target, float duration, float scaleFactor)
    {
        Vector3 originalScale = target.localScale;
        Vector3 targetScale = originalScale * scaleFactor;
        
        float elapsed = 0f;
        
        // Scale up
        while (elapsed < duration / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (duration / 2);
            target.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }
        
        elapsed = 0f;
        
        // Scale down
        while (elapsed < duration / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (duration / 2);
            target.localScale = Vector3.Lerp(targetScale, originalScale, t);
            yield return null;
        }
        
        target.localScale = originalScale;
    }
    
    public void AnimateRotation(Transform target, float duration = 0.5f, float angle = 360f)
    {
        if (!effectsEnabled || target == null) return;
        StartCoroutine(RotationCoroutine(target, duration, angle));
    }
    
    private System.Collections.IEnumerator RotationCoroutine(Transform target, float duration, float angle)
    {
        Quaternion startRotation = target.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, angle);
        
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            target.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        
        target.rotation = endRotation;
    }
}
