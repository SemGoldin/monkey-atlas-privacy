using UnityEngine;
using UnityEngine.EventSystems;

public class BoltInputHandler : MonoBehaviour, IPointerClickHandler
{
    private Bolt bolt;
    
    private void Awake()
    {
        bolt = GetComponent<Bolt>();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance != null && bolt != null)
        {
            GameManager.Instance.OnBoltClicked(bolt);
        }
    }
}
