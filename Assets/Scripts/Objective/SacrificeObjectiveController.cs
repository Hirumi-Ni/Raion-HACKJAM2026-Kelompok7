using UnityEngine;

public class SacrificeObjectiveController : MonoBehaviour
{
    [field: SerializeField] public int targetSacrificeAmount { get; private set; }
    public int currentSacrificeAmount { get; private set; }
    
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
