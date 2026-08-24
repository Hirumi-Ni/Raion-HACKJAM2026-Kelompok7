using UnityEngine;
using TMPro;

public class UIHelper : MonoBehaviour
{
    [Header("Controller Script")]
    [SerializeField] private SheepObjectiveController sheepController;
    [SerializeField] private SacrificeObjectiveController sacrificeController;

    [Header("UI Component")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text objectiveSheepText;
    [SerializeField] private TMP_Text objectiveSacrificeText;

    private void Start()
    {
        UpdateObjectiveCounter();
    }

    public void ButtonSheepSacrifice()
    {
        EventHandler.WhenAnimalSacrificed();
        UpdateObjectiveCounter();
    }

    public void ButtonSheepCapture()
    {
        EventHandler.WhenAnimalCaptured();
        UpdateObjectiveCounter();
    }

    private void UpdateObjectiveCounter()
    {
        objectiveSheepText.text = $"{sheepController.currentSheepAmount}/{sheepController.targetSheepAmount}";
        objectiveSacrificeText.text = $"{sacrificeController.currentSacrificeAmount}/{sacrificeController.targetSacrificeAmount}";
    }
}
