using UnityEngine;

public class Wolf : BaseAnimal
{
    [SerializeField] private float wolfDamage;
    [SerializeField] private int decreaseObjectiveAmount;
    public override void OnCapture()
    {
        EventHandler.WhenDecreaseTrailResource(wolfDamage);
        EventHandler.WhenDecreaseObjective(decreaseObjectiveAmount);
        Destroy(gameObject, .01f);
    }
}
