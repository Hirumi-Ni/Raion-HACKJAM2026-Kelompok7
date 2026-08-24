using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float acceleration = 50f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        FlipSpriteOnMovement();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        moveInput = InputManager.instance.GetPlayerMovement().normalized;
    }
    
    private void HandleMovement()
    {
        Vector2 targetVelocity = moveInput * moveSpeed;

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, acceleration * Time.deltaTime);
    }

    private void FlipSpriteOnMovement()
    {
        if (moveInput.x > .1f) transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput.x < -.1f) transform.localScale = new Vector3(1, 1, 1);
    }
}
