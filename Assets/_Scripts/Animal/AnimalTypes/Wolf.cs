using UnityEngine;

public class Wolf : BaseAnimal
{
    [SerializeField] private float wolfDamage;
    public override void OnCapture()
    {
        EventHandler.WhenWolfCaptured(wolfDamage);
        Destroy(gameObject, .01f);
    }
}
