using UnityEngine;
using System.Collections;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Represents a single nut in the game
    /// </summary>
    public class Nut : MonoBehaviour
    {
        [SerializeField] private NutColor color;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float rotationSpeed = 360f;
        [SerializeField] private float shakeAmount = 0.1f;
        
        private Vector3 targetPosition;
        private bool isMoving = false;
        private Bolt currentBolt;
        
        public NutColor Color 
        { 
            get => color; 
            set 
            { 
                color = value;
                UpdateVisualColor();
            }
        }
        
        public Bolt CurrentBolt => currentBolt;
        
        /// <summary>
        /// Initialize the nut with a specific color
        /// </summary>
        public void Initialize(NutColor nutColor, Bolt bolt)
        {
            this.color = nutColor;
            this.currentBolt = bolt;
            UpdateVisualColor();
        }
        
        /// <summary>
        /// Update the visual representation based on color
        /// </summary>
        private void UpdateVisualColor()
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                Material mat = renderer.material;
                mat.color = GameConstants.GetUnityColor(color);
            }
        }
        
        /// <summary>
        /// Move the nut to unscrew position above its bolt
        /// </summary>
        public IEnumerator UnscrewAnimation(Vector3 unscrewPosition)
        {
            isMoving = true;
            Vector3 startPosition = transform.position;
            float elapsed = 0f;
            float duration = 0.5f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                // Move upward
                transform.position = Vector3.Lerp(startPosition, unscrewPosition, t);
                
                // Rotate during unscrewing
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                
                yield return null;
            }
            
            transform.position = unscrewPosition;
            isMoving = false;
        }
        
        /// <summary>
        /// Move the nut to a target bolt and screw it on
        /// </summary>
        public IEnumerator ScrewOntoAnimation(Vector3 targetPosition, Bolt targetBolt)
        {
            isMoving = true;
            Vector3 startPosition = transform.position;
            float elapsed = 0f;
            float duration = 0.5f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                // Move to target
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                
                // Rotate during screwing
                transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
                
                yield return null;
            }
            
            transform.position = targetPosition;
            currentBolt = targetBolt;
            isMoving = false;
        }
        
        /// <summary>
        /// Shake animation for invalid move
        /// </summary>
        public IEnumerator ShakeAnimation()
        {
            Vector3 originalPosition = transform.position;
            float elapsed = 0f;
            float duration = 0.5f;
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                
                // Shake side to side
                float offsetX = Mathf.Sin(t * 20f) * shakeAmount * (1f - t);
                transform.position = originalPosition + new Vector3(offsetX, 0f, 0f);
                
                yield return null;
            }
            
            transform.position = originalPosition;
        }
        
        /// <summary>
        /// Return to original position on same bolt
        /// </summary>
        public IEnumerator ReturnToBolt(Vector3 boltPosition)
        {
            yield return ScrewOntoAnimation(boltPosition, currentBolt);
        }
    }
}
