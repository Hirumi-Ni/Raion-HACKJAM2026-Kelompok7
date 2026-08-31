using UnityEngine;
using UnityEngine.EventSystems;
public class UIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject gameObject;
    private Vector3 originalScale;
    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIAnimationManager.Instance.Hover(gameObject.transform, originalScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIAnimationManager.Instance.Unhover(gameObject.transform, originalScale);
    }
}