using UnityEngine;

public class Wolf : BaseAnimal
{
    [SerializeField] private float wolfDamage;
    [SerializeField] private int decreaseObjectiveAmount;
    public override void OnCapture()
    {
        EventHandler.WhenDecreaseTrailResource(wolfDamage);
        EventHandler.WhenDecreaseObjective(decreaseObjectiveAmount);
        base.OnCapture();
        Destroy(gameObject, .9f);
    }
}
