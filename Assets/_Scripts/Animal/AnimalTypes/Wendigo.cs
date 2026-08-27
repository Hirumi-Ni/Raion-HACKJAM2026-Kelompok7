using NavMeshPlus.Extensions;
using UnityEngine;

public class Wendigo : BaseAnimal
{
    [SerializeField] private float wendigoDamage;
    [SerializeField] private int decreaseObjectiveAmount;
    [SerializeField] private float stunDuration;
    [SerializeField] private Sprite wendigoFormSprite;
    private int count = 0;
    private bool isTransformed = false;
    private SpriteRenderer spriteRenderer;
    private RandomMovement randomMovement;

    public override void OnCapture()
    {
        count++;

        if (!isTransformed) TransformWendigo();

        if (count >= 2)
        {
            Destroy(gameObject, .01f);
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        randomMovement = GetComponent<RandomMovement>();
    }

    private void TransformWendigo()
    {
        isTransformed = true;

        Destroy(randomMovement);
        spriteRenderer.sprite = wendigoFormSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTransformed) return;

        if (other.CompareTag("Player"))
        {
            EventHandler.WhenCaptureDecreasePlayerSpeed(stunDuration);
            EventHandler.WhenCaptureDecreaseObjective(decreaseObjectiveAmount);
            EventHandler.WhenCapturedDecreaseTrailResource(wendigoDamage);
        }
    }
}
