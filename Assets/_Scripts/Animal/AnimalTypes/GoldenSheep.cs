using UnityEngine;

public class GoldenSheep : Sheep
{
    [SerializeField] private float goldRushDuration;
    [SerializeField] private float playerBuffSpeed;
    public override void OnCapture()
    {
        EventHandler.WhenGoldRush(goldRushDuration);
        EventHandler.WhenChangePlayerSpeed(playerBuffSpeed, goldRushDuration);
        EventHandler.WhenIncreaseTrailResource(1000f);
        base.OnCapture();
    }
}
