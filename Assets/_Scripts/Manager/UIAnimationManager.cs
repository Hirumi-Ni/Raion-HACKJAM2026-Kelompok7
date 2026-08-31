using UnityEngine;
using DG.Tweening;
public class UIAnimationManager : MonoBehaviour
{
    public static UIAnimationManager Instance { get; private set; }

    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float duration = 0.15f;
    [SerializeField] private Ease ease = Ease.OutBack;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Hover(Transform target, Vector3 originalScale)
    {
        if (target == null) return;

        target.DOKill();
        target.DOScale(originalScale * hoverScale, duration).SetEase(ease);
    }

    public void Unhover(Transform target, Vector3 originalScale)
    {
        if (target == null) return;

        target.DOKill();
        target.DOScale(originalScale, duration).SetEase(ease);
    }
}
