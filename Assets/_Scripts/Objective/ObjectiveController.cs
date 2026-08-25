using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [Header("Objectives Amount")]
    [field: SerializeField] public int targetSheepAmount { get; private set; }
    [field: SerializeField] public int targetSacrificeAmount { get; private set; }
    public int currentSheepAmount { get; private set; }
    public int currentSacrificeAmount { get; private set; }

    private void OnEnable()
    {
        EventHandler.OnAnimalCaptured += CaptureAnimal;
        EventHandler.OnAnimalSacrificed += SacrificeAnimal;
        EventHandler.OnTimerEnded += CheckObjectiveComplete;
    }

    private void OnDisable()
    {
        EventHandler.OnAnimalCaptured -= CaptureAnimal;
        EventHandler.OnAnimalSacrificed -= SacrificeAnimal;
        EventHandler.OnTimerEnded -= CheckObjectiveComplete;
    }

    private void Start()
    {
        currentSheepAmount = 0;
        currentSacrificeAmount = 0;
    }

    private void CaptureAnimal()
    {
        currentSheepAmount++;

        EventHandler.WhenObjectiveChanged();
    }

    private void SacrificeAnimal()
    {
        if (currentSheepAmount <= 0) return;
        currentSheepAmount--;
        currentSacrificeAmount++;

        EventHandler.WhenTrailResourceGain();
        EventHandler.WhenObjectiveChanged();
    }

    private void CheckObjectiveComplete()
    {
        bool objectivesComplete = currentSheepAmount >= targetSheepAmount && currentSacrificeAmount >= targetSacrificeAmount;
        EventHandler.WhenGameEnded(objectivesComplete);
    }
}
