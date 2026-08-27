using UnityEngine;

public class GreenSheep : Sheep
{
    [SerializeField] private float playerBuffSpeed;
    [SerializeField] private float durationMoveSpeed;
    public override void OnCapture()
    {
        EventHandler.WhenChangePlayerSpeed(playerBuffSpeed,durationMoveSpeed);
        base.OnCapture();
    }
}
