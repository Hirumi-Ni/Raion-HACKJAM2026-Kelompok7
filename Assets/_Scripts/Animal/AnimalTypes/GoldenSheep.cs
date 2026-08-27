using UnityEngine;

public class GoldenSheep : Sheep
{
    [SerializeField] private float goldRushDuration;
    [SerializeField] private float playerBuffSpeed;
    public override void OnCapture()
    {
        EventHandler.WhenCaptureGoldRush(goldRushDuration);
        EventHandler.WhenCaptureGoldRush(playerBuffSpeed);
        base.OnCapture();
    }
}
