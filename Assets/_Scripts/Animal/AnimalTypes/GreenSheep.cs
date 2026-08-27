using UnityEngine;

public class GreenSheep : Sheep
{
    [SerializeField] private float playerBuffSpeed;
    public override void OnCapture()
    {
        EventHandler.WhenCaptureGoldRush(playerBuffSpeed);
        base.OnCapture();
    }
}
