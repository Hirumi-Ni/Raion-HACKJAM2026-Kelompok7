using UnityEngine;

public class BlackSheep : BaseAnimal
{
    [SerializeField] private float blindDuration;
    [SerializeField] private int decreaseObjectiveAmount;
    public override void OnCapture()
    {
        EventHandler.WhenBlindPlayer(blindDuration);
        EventHandler.WhenDecreaseObjective(decreaseObjectiveAmount);
        base.OnCapture();
        Destroy(gameObject, .9f);
    }
}
