using UnityEngine;

public class SheepObjectiveController : MonoBehaviour
{
    [SerializeField] private int targetSheepAmount;
    private int currentSheepAmount;

    private void OnEnable()
    {
        EventHandler.OnAnimalCaptured += AnimalCapture;
    }

    private void OnDisable()
    {
        EventHandler.OnAnimalCaptured -= AnimalCapture;
    }

    private void Start()
    {
        currentSheepAmount = 0;
    }

    private void AnimalCapture()
    {
        currentSheepAmount++;

        CheckObjectiveComplete();
    }

    private void AnimalSacrificeExchange()
    {
        if (currentSheepAmount <= 0) return;

        currentSheepAmount--;

        EventHandler.WhenAnimalSacrificed();

        CheckObjectiveComplete();
    }

    private void CheckObjectiveComplete()
    {
        if (currentSheepAmount >= targetSheepAmount)
        {
            //kirim sinyal buat ngasi tau ke win objective condition script bahwa kondisinya udah terpenuhi
        }
    }
}
