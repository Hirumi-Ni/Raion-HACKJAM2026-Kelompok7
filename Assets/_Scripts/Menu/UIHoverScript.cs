using UnityEngine;
using UnityEngine.EventSystems;
public class UIHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject gameObjects;
    private Vector3 originalScale;
    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIAnimationManager.instance.Hover(gameObjects.transform, originalScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIAnimationManager.instance.Unhover(gameObjects.transform, originalScale);
    }
}