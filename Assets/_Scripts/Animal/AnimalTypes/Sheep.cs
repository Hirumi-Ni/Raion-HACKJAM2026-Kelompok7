using UnityEngine;

public class Sheep : BaseAnimal
{
    [SerializeField] private int objectiveAmount;
    public override void OnCapture()
    {
        EventHandler.WhenCaptureIncreaseObjective(objectiveAmount);
        Destroy(gameObject, .01f);
    }
}
