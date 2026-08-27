using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    [Header("Objectives Amount")]
    [field: SerializeField] public int targetSheepAmount { get; private set; }
    public int currentSheepAmount { get; private set; }

    private void OnEnable()
    {
        EventHandler.OnIncreaseObjective += IncreaseObjective;
        EventHandler.OnDecreaseObjective += DecreaseObjective;
    }

    private void OnDisable()
    {
        EventHandler.OnIncreaseObjective -= IncreaseObjective;
        EventHandler.OnDecreaseObjective -= DecreaseObjective;
    }

    private void Start()
    {
        currentSheepAmount = 0;
    }

    private void IncreaseObjective(int amount)
    {
        currentSheepAmount += amount;
        EventHandler.WhenObjectiveChanged();
        CheckObjectiveComplete();
    }

    private void DecreaseObjective(int amount)
    {
        currentSheepAmount -= amount;
        currentSheepAmount = Mathf.Clamp(currentSheepAmount, 0, targetSheepAmount);
        EventHandler.WhenObjectiveChanged();
    }

    private void CheckObjectiveComplete()
    {
        bool objectivesComplete = currentSheepAmount >= targetSheepAmount;
        if (objectivesComplete) EventHandler.WhenGameEnded(objectivesComplete);
    }
}
