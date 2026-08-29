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

    [Header("UI Component")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text objectiveSheepText;
    [SerializeField] private Image healthbarImage;
    [SerializeField] private Image blindScreen;
    [SerializeField] private GameObject pauseScreen;

    private void OnEnable()
    {
        EventHandler.OnObjectiveChanged += UpdateObjectiveCounter;
        EventHandler.OnBlindPlayer += HandleBlindPlayer;
    }

    private void OnDisable()
    {
        EventHandler.OnObjectiveChanged -= UpdateObjectiveCounter;
        EventHandler.OnBlindPlayer -= HandleBlindPlayer;
    }

    private void Start()
    {
        UpdateObjectiveCounter();
        healthbarImage.fillAmount = trailController.maxTrailResource;

        blindScreen.color = new Color(1f, 1f, 1f, 0f);
        blindScreen.transform.localScale = Vector3.one * 0.1f;
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        if (InputManager.instance.GetPauseKeyPress()) TogglePause();

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
        bool isPaused = Time.timeScale == 0f;

        if (isPaused)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
