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
    }

    private void Start()
    {
        currentSheepAmount = 0;
        currentSacrificeAmount = 0;
    }

    private void CaptureAnimal()
    {
        currentSheepAmount++;
        CheckObjectiveComplete();
    }

    private void SacrificeAnimal()
    {
        if (currentSheepAmount <= 0) return;
        currentSheepAmount--;
        currentSacrificeAmount++;
        CheckObjectiveComplete();
    }

    private void CheckObjectiveComplete()
    {
        if (currentSheepAmount >= targetSheepAmount && currentSacrificeAmount >= targetSacrificeAmount)
        {
            //EventHandler.WhenGameEnded(true); //true = menang
        }
    }
}
