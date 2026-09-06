using NavMeshPlus.Extensions;
using UnityEngine;

public class Wendigo : BaseAnimal
{
    [Header("Attribute")]
    [SerializeField] private float wendigoDamage;
    [SerializeField] private int decreaseObjectiveAmount;
    [SerializeField] private float slowAmount;
    [SerializeField] private float slowDuration;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite wendigoFormSprite;
    private int count = 0;
    private bool isTransformed = false;
    private AnimalMovementBehaviour movementTargetPlayer;

    public override void OnCapture()
    {
        count++;

        if (!isTransformed) TransformWendigo();

        if (count >= 2)
        {
            base.OnCapture();
            Destroy(gameObject, .9f);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        movementTargetPlayer = GetComponent<AnimalMovementBehaviour>();
    }

    private void TransformWendigo()
    {
        isTransformed = true;

        movementTargetPlayer.isWendigoTransformed = isTransformed;

        spriteRenderer.sprite = wendigoFormSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTransformed) return;

        if (other.CompareTag("Player"))
        {
            EventHandler.WhenChangePlayerSpeed(slowAmount, slowDuration);
            EventHandler.WhenDecreaseObjective(decreaseObjectiveAmount);
            EventHandler.WhenDecreaseTrailResource(wendigoDamage);
            Destroy(gameObject, .01f);
        }
    }
}
