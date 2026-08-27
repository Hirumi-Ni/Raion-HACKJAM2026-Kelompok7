using UnityEngine;

public class Sheep : BaseAnimal
{
    [SerializeField] private int objectiveAmount;
    public override void OnCapture()
    {
        EventHandler.WhenIncreaseObjective(objectiveAmount);
        Destroy(gameObject, .01f);
    }
}
