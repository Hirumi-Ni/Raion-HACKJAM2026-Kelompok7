using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] public float playerMaxHealth { get; private set; }
    [SerializeField] private float moveSpeed = 8;
    private float defaultSpeed;
    private float acceleration = 50f;
    public Rigidbody2D rb { get; private set; }
    private Vector2 moveInput;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.OnChangePlayerSpeed += HandleSpeedChange;
    }

    private void OnDisable()
    {
        EventHandler.OnChangePlayerSpeed -= HandleSpeedChange;
    }

    private void Start()
    {
        defaultSpeed = moveSpeed;
    }

    private void Update()
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
        if (moveInput != Vector2.zero) animator.SetBool("isMoving", true);
        else animator.SetBool("isMoving", false);

        Vector2 targetVelocity = moveInput * moveSpeed;

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, acceleration * Time.deltaTime);
    }

    private void FlipSpriteOnMovement()
    {
        if (moveInput.x > .1f) transform.localScale = new Vector3(-1, 1, 1);
        else if (moveInput.x < -.1f) transform.localScale = new Vector3(1, 1, 1);
    }

    private void HandleSpeedChange(float amount, float duration)
    {
        StartCoroutine(SpeedChangeCoroutine(amount, duration));
    }

    private IEnumerator SpeedChangeCoroutine(float amount, float duration)
    {
        moveSpeed = amount;
        yield return new WaitForSeconds(duration);
        moveSpeed = defaultSpeed;
    }
}
