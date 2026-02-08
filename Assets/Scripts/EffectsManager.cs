using UnityEngine;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Visual effects manager for particle systems and effects
    /// </summary>
    public class EffectsManager : MonoBehaviour
    {
        public static EffectsManager Instance { get; private set; }
        
        [Header("Particle Systems")]
        [SerializeField] private ParticleSystem nutContactEffect;
        [SerializeField] private ParticleSystem victoryEffect;
        [SerializeField] private ParticleSystem sparkleEffect;
        
        [Header("Animation Settings")]
        [SerializeField] private AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        /// <summary>
        /// Play nut contact effect when two nuts touch
        /// </summary>
        public void PlayNutContactEffect(Vector3 position)
        {
            if (nutContactEffect != null)
            {
                PlayEffectAtPosition(nutContactEffect, position);
            }
        }
        
        /// <summary>
        /// Play victory effect when player wins
        /// </summary>
        public void PlayVictoryEffect(Vector3 position)
        {
            if (victoryEffect != null)
            {
                PlayEffectAtPosition(victoryEffect, position);
            }
        }
        
        /// <summary>
        /// Play sparkle effect for highlights
        /// </summary>
        public void PlaySparkleEffect(Vector3 position)
        {
            if (sparkleEffect != null)
            {
                PlayEffectAtPosition(sparkleEffect, position);
            }
        }
        
        /// <summary>
        /// Play a particle effect at a specific position
        /// </summary>
        private void PlayEffectAtPosition(ParticleSystem effect, Vector3 position)
        {
            ParticleSystem instance = Instantiate(effect, position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
        
        /// <summary>
        /// Get animation curve value
        /// </summary>
        public float EvaluateMovementCurve(float time)
        {
            return movementCurve.Evaluate(time);
        }
    }
}
