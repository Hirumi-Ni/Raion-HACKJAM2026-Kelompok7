using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class HudScript : MonoBehaviour
{
    [Header("Reference Script")]
    [SerializeField] private ObjectiveController objectiveController;
    [SerializeField] private TimerController timerController;
    [SerializeField] private TrailController trailController;
    private bool isPaused = false;
    private bool isStolen = false;

    [Header("UI Component")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text objectiveSheepText;
    [SerializeField] private Image healthbarImage;
    [SerializeField] private Image blindScreen;
    [SerializeField] private PauseScript pauseObject;
    [SerializeField] private CanvasGroup stolenSheepIndicator;
    [SerializeField] private Color redColor = Color.red;
    [SerializeField] private Color purpleColor = Color.purple;

    private void OnEnable()
    {
        EventHandler.OnObjectiveChanged += UpdateObjectiveCounter;
        EventHandler.OnBlindPlayer += HandleBlindPlayer;
        EventHandler.OnDecreaseObjective += SheepStolen;
    }

    private void OnDisable()
    {
        EventHandler.OnObjectiveChanged -= UpdateObjectiveCounter;
        EventHandler.OnBlindPlayer -= HandleBlindPlayer;
        EventHandler.OnDecreaseObjective -= SheepStolen;
    }

    private void Start()
    {
        UpdateObjectiveCounter();
        healthbarImage.fillAmount = trailController.maxTrailResource;
        healthbarImage.color = redColor;

        blindScreen.color = new Color(1f, 1f, 1f, 0f);
        blindScreen.transform.localScale = Vector3.one * 0.1f;

        stolenSheepIndicator.gameObject.SetActive(false);
        stolenSheepIndicator.alpha = 0;
        pauseObject.ButtonResumeGame();
    }

    private void Update()
    {
        if (InputManager.instance.GetPauseKeyPress()) TogglePause();

        healthbarImage.color = trailController.isTrailHeal ? purpleColor : redColor;

        if (Time.timeScale == 0f) return;

        timerText.text = timerController.GetTimerText();
        healthbarImage.fillAmount = trailController.currentTrailResource / trailController.maxTrailResource;
    }

    private void UpdateObjectiveCounter()
    {
        objectiveSheepText.text = $"{objectiveController.currentSheepAmount}/{objectiveController.targetSheepAmount}";
    }

    private void HandleBlindPlayer(float duration)
    {
        blindScreen.DOKill();
        blindScreen.transform.DOKill();

        blindScreen.DOFade(1f, 0.4f);
        blindScreen.transform.DOScale(1f, 0.4f).SetEase(Ease.OutBack); 

        StartCoroutine(BlindPlayerCoroutine(duration));
    }

    private IEnumerator BlindPlayerCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);

        blindScreen.DOFade(0f, 0.4f);
        blindScreen.transform.DOScale(0.1f, 0.4f).SetEase(Ease.InBack); 
    }

    private void TogglePause()
    {
        if (isPaused)
        { 
            pauseObject.ButtonResumeGame(); 
            isPaused = false; 
        } 
        else 
        { 
            pauseObject.ButtonPauseGame(); 
            isPaused = true; 
        }
    }

    private void SheepStolen(int _)
    {
        if (isStolen) return;
        StartCoroutine(StartStolenIndicator());
    }

    private IEnumerator StartStolenIndicator()
    {
        isStolen = true;
        stolenSheepIndicator.gameObject.SetActive(true);

        UIAnimationManager.instance.Fade(stolenSheepIndicator, 1);

        yield return new WaitForSeconds(1);

        UIAnimationManager.instance.Fade(stolenSheepIndicator, 0);

        yield return new WaitForSeconds(2);

        stolenSheepIndicator.gameObject.SetActive(false);
        isStolen = false;
    }
}
