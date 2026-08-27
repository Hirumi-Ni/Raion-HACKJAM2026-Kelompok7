using UnityEngine;

public class Wolf : BaseAnimal
{
    [SerializeField] private float wolfDamage;
    [SerializeField] private int decreaseObjectiveAmount;
    public override void OnCapture()
    {
        EventHandler.WhenCapturedDecreaseTrailResource(wolfDamage);
        EventHandler.WhenCaptureDecreaseObjective(decreaseObjectiveAmount);
        Destroy(gameObject, .01f);
    }
}
