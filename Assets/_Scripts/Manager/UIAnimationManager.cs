using UnityEngine;
using DG.Tweening;
public class UIAnimationManager : MonoBehaviour
{
    public static UIAnimationManager instance { get; private set; }

    #region Setting Animasi
    [Header("Hover")]
    [SerializeField] private float hoverScale = 1.1f; 
    [SerializeField] private float duration = 0.15f; 
    [SerializeField] private Ease ease = Ease.OutBack;
    
    [Header("Stamp")]
    [SerializeField] private float stampStartScale = 2.5f; 
    [SerializeField] private float stampDuration = 0.25f; 
    [SerializeField] private Ease stampEase = Ease.OutBack; 
    
    [Header("Fade")]
    [SerializeField] private float fadeDuration = 0.25f; 
    [SerializeField] private Ease fadeEase = Ease.OutQuad;
    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Hover(Transform target, Vector3 originalScale)
    {
        if (target == null) return;

        target.DOKill();
        target.DOScale(originalScale * hoverScale, duration).SetEase(ease).SetUpdate(true);
    }

    public void Unhover(Transform target, Vector3 originalScale)
    {
        if (target == null) return;

        target.DOKill();
        target.DOScale(originalScale, duration).SetEase(ease).SetUpdate(true);
    }

    public void Stamp(Transform target, Vector3 originalScale)
    {
        if (target == null) return; target.DOKill(); 
        target.localScale = originalScale * stampStartScale; 
        target.DOScale(originalScale, stampDuration).SetEase(stampEase).SetUpdate(true); 
    }

    public void FadeIn(CanvasGroup canvasGroup) 
    { 
        if (canvasGroup == null) return; 
        canvasGroup.DOKill(); 
        canvasGroup.DOFade(1f, fadeDuration).SetEase(fadeEase).SetUpdate(true); 
    }

    public void Fade(CanvasGroup canvasGroup, float targetAlpha) 
    { 
        if (canvasGroup == null) return; 
        canvasGroup.DOKill(); 
        canvasGroup.DOFade(targetAlpha, fadeDuration).SetEase(fadeEase).SetUpdate(true); 
    }
}
