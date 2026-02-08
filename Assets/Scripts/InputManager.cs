using UnityEngine;

namespace BoltNutPuzzle
{
    /// <summary>
    /// Handles player input and interactions
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask boltLayer;
        
        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }
        
        private void Update()
        {
            HandleMouseInput();
            HandleTouchInput();
        }
        
        /// <summary>
        /// Handle mouse input for desktop
        /// </summary>
        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    Bolt bolt = hit.collider.GetComponent<Bolt>();
                    if (bolt != null)
                    {
                        OnBoltInteraction(bolt);
                    }
                }
            }
        }
        
        /// <summary>
        /// Handle touch input for mobile
        /// </summary>
        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = mainCamera.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    
                    if (Physics.Raycast(ray, out hit, 100f))
                    {
                        Bolt bolt = hit.collider.GetComponent<Bolt>();
                        if (bolt != null)
                        {
                            OnBoltInteraction(bolt);
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Handle bolt interaction
        /// </summary>
        private void OnBoltInteraction(Bolt bolt)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnBoltClicked(bolt);
            }
            
            // Play click sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayClickSound();
            }
        }
    }
}
