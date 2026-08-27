using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private void OnEnable()
    {
        EventHandler.OnObjectiveChanged += UpdateObjectiveCounter;
    }

    private void OnDisable()
    {
        EventHandler.OnObjectiveChanged -= UpdateObjectiveCounter;
    }

    private void Start()
    {
        UpdateObjectiveCounter();
        healthbarImage.fillAmount = trailController.maxTrailResource;
    }

    private void Update()
    {
        timerText.text = timerController.GetTimerText();
        healthbarImage.fillAmount = trailController.currentTrailResource / trailController.maxTrailResource;
    }

    private void UpdateObjectiveCounter()
    {
        objectiveSheepText.text = $"{objectiveController.currentSheepAmount}/{objectiveController.targetSheepAmount}";
    }
}
