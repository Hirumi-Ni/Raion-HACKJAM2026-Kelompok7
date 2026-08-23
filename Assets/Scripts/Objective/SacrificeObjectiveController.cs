using UnityEngine;

public class SacrificeObjectiveController : MonoBehaviour
{
    [SerializeField] private int targetSacrificeAmount;
    private int currentSacrificeAmount;
    
    private void OnEnable()
    {
        EventHandler.OnAnimalSacrificed += AnimalSacrifice;
    }
    
    private void OnDisable()
    {
        EventHandler.OnAnimalSacrificed -= AnimalSacrifice;
    }
    
    private void Start()
    {
        currentSacrificeAmount = 0;
    }

    private void AnimalSacrifice()
    {
        currentSacrificeAmount++;

        CheckObjectiveComplete();
    }

    private void CheckObjectiveComplete()
    {
        if (currentSacrificeAmount >= targetSacrificeAmount)
        {
            //kirim sinyal buat ngasi tau ke win objective condition script bahwa kondisinya udah terpenuhi
        }
    }
}
