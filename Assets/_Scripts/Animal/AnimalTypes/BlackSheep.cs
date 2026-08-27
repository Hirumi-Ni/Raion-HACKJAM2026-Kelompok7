using UnityEngine;

public class BlackSheep : Sheep
{
    [SerializeField] private float blindDuration;
    public override void OnCapture()
    {
        EventHandler.WhenCaptureBlindPlayer(blindDuration);
        base.OnCapture();
    }
}
