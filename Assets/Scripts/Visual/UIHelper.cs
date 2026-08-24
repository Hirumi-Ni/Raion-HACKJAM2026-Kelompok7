using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private float trailGainAmount;

    [Header("Reference Script")]
    [SerializeField] private ObjectiveController objectiveController;
    [SerializeField] private TimerController timerController;
    [SerializeField] private TrailController trailController;

    [Header("UI Component")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text objectiveSheepText;
    [SerializeField] private TMP_Text objectiveSacrificeText;
    [SerializeField] private Image healthbarImage;

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

    public void ButtonSheepCapture()
    {
        EventHandler.WhenAnimalCaptured();
        UpdateObjectiveCounter();
    }

    public void ButtonSheepSacrifice()
    {
        if (objectiveController.currentSheepAmount <= 0) return;
        EventHandler.WhenTrailResourceGain();
        EventHandler.WhenAnimalSacrificed();
        UpdateObjectiveCounter();
    }

    private void UpdateObjectiveCounter()
    {
        objectiveSheepText.text = $"{objectiveController.currentSheepAmount}/{objectiveController.targetSheepAmount}";
        objectiveSacrificeText.text = $"{objectiveController.currentSacrificeAmount}/{objectiveController.targetSacrificeAmount}";
    }
}
