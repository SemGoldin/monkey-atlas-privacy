using UnityEngine;
using DG.Tweening;

/// <summary>
/// Represents a single nut with a color that can be moved between bolts
/// </summary>
public class Nut : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material material;
    
    public NutColor Color { get; private set; }
    public Bolt ParentBolt { get; set; }
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Sequence currentAnimation;

    public void Initialize(NutColor color)
    {
        Color = color;
        UpdateVisualColor();
    }

    private void UpdateVisualColor()
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        
        if (meshRenderer != null)
        {
            if (material == null)
            {
                material = new Material(meshRenderer.sharedMaterial);
            }
            meshRenderer.material = material;
            material.color = GetColorFromEnum(Color);
        }
    }

    private Color GetColorFromEnum(NutColor nutColor)
    {
        switch (nutColor)
        {
            case NutColor.Red: return UnityEngine.Color.red;
            case NutColor.Blue: return UnityEngine.Color.blue;
            case NutColor.Green: return UnityEngine.Color.green;
            case NutColor.Yellow: return UnityEngine.Color.yellow;
            case NutColor.Purple: return new Color(0.5f, 0f, 0.5f);
            case NutColor.Orange: return new Color(1f, 0.5f, 0f);
            case NutColor.Cyan: return UnityEngine.Color.cyan;
            case NutColor.Magenta: return UnityEngine.Color.magenta;
            default: return UnityEngine.Color.white;
        }
    }

    /// <summary>
    /// Animate unscrewing from bolt with rotation and lift
    /// </summary>
    public void AnimateUnscrew(System.Action onComplete = null)
    {
        KillCurrentAnimation();
        
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        currentAnimation = DOTween.Sequence();
        
        // Play unscrew sound
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayUnscrewSound();
        }
        
        // Rotate as if unscrewing (counter-clockwise when viewed from top)
        currentAnimation.Append(transform.DORotate(new Vector3(0, 360, 0), 0.3f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad));
        
        // Lift up
        currentAnimation.Join(transform.DOMoveY(transform.position.y + 2f, 0.3f).SetEase(Ease.OutQuad));
        
        // Hover animation - slight bobbing and rotation
        currentAnimation.AppendCallback(() => StartHoverAnimation());
        
        currentAnimation.OnComplete(() => onComplete?.Invoke());
    }

    /// <summary>
    /// Continuous hover animation while nut is selected
    /// </summary>
    private void StartHoverAnimation()
    {
        KillCurrentAnimation();
        
        currentAnimation = DOTween.Sequence();
        
        // Gentle bobbing motion
        currentAnimation.Append(transform.DOMoveY(transform.position.y + 0.2f, 0.8f).SetEase(Ease.InOutSine));
        currentAnimation.Append(transform.DOMoveY(transform.position.y, 0.8f).SetEase(Ease.InOutSine));
        
        // Slow rotation
        currentAnimation.Join(transform.DORotate(new Vector3(0, 180, 0), 1.6f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        
        currentAnimation.SetLoops(-1); // Infinite loop
    }

    /// <summary>
    /// Move nut to target bolt with arc motion
    /// </summary>
    public void AnimateMoveToBolt(Bolt targetBolt, Vector3 targetPosition, System.Action onComplete = null)
    {
        KillCurrentAnimation();
        
        currentAnimation = DOTween.Sequence();
        
        // Calculate arc path
        Vector3 midPoint = (transform.position + targetPosition) / 2f;
        midPoint.y += 1.5f; // Arc height
        
        // Move to target position with arc
        Vector3[] path = new Vector3[] { transform.position, midPoint, targetPosition + Vector3.up * 2f };
        
        currentAnimation.Append(transform.DOPath(path, 0.5f, PathType.CatmullRom).SetEase(Ease.InOutQuad));
        
        // Rotate during flight
        currentAnimation.Join(transform.DORotate(new Vector3(0, 180, 0), 0.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear));
        
        // Align and screw down
        currentAnimation.AppendCallback(() => AnimateScrewDown(targetPosition, onComplete));
    }

    /// <summary>
    /// Animate screwing down onto bolt
    /// </summary>
    private void AnimateScrewDown(Vector3 finalPosition, System.Action onComplete = null)
    {
        var screwSequence = DOTween.Sequence();
        
        // Align rotation
        screwSequence.Append(transform.DORotate(Vector3.zero, 0.2f).SetEase(Ease.OutQuad));
        
        // Play screw sound when starting to go down
        screwSequence.AppendCallback(() => {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayScrewSound();
            }
        });
        
        // Move down while rotating (screwing motion)
        screwSequence.Append(transform.DOMoveY(finalPosition.y, 0.4f).SetEase(Ease.InQuad));
        screwSequence.Join(transform.DORotate(new Vector3(0, -360, 0), 0.4f, RotateMode.LocalAxisAdd).SetEase(Ease.InQuad));
        
        screwSequence.OnComplete(() => onComplete?.Invoke());
    }

    /// <summary>
    /// Return nut to original position on bolt
    /// </summary>
    public void AnimateReturnToBolt(System.Action onComplete = null)
    {
        KillCurrentAnimation();
        
        currentAnimation = DOTween.Sequence();
        
        // Move back to original position
        currentAnimation.Append(transform.DOMove(originalPosition, 0.3f).SetEase(Ease.OutQuad));
        currentAnimation.Join(transform.DORotate(originalRotation.eulerAngles, 0.3f).SetEase(Ease.OutQuad));
        
        // Screw back down
        currentAnimation.AppendCallback(() => AnimateScrewDown(originalPosition, onComplete));
    }

    /// <summary>
    /// Stop all running animations
    /// </summary>
    private void KillCurrentAnimation()
    {
        if (currentAnimation != null && currentAnimation.IsActive())
        {
            currentAnimation.Kill();
            currentAnimation = null;
        }
    }

    private void OnDestroy()
    {
        KillCurrentAnimation();
    }
}

/// <summary>
/// Available nut colors
/// </summary>
public enum NutColor
{
    Red,
    Blue,
    Green,
    Yellow,
    Purple,
    Orange,
    Cyan,
    Magenta
}
