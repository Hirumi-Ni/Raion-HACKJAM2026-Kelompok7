using UnityEngine;

public class Sheep : BaseAnimal
{
    public override void OnCapture()
    {
        EventHandler.WhenAnimalCaptured();
        Destroy(gameObject, .01f);
    }
}
