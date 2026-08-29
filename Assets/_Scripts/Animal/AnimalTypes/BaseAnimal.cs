using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAnimal : MonoBehaviour, ICapturable
{
    [SerializeField] private float moveSpeed = 3.5f;
    private NavMeshAgent agent;
    private Animator animalAnimator;

    public virtual void OnCapture()
    {
        animalAnimator.SetTrigger("isCaptured");
    }

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animalAnimator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        agent.speed = moveSpeed;
        var state = animalAnimator.GetCurrentAnimatorStateInfo(0);
        animalAnimator.Play(state.fullPathHash, 0, Random.Range(0f, 1f));
    }

    private void Update()
    {
        FlipSpriteOnMovement();
    }

    private void FlipSpriteOnMovement()
    {
        if (agent.velocity.x > .1f) transform.localScale = new Vector3(-1f, 1, 1);
        else if (agent.velocity.x < -.1f) transform.localScale = new Vector3(1f, 1, 1);

        if (agent.velocity.sqrMagnitude > .01f) animalAnimator.SetBool("isMoving", true);
        else animalAnimator.SetBool("isMoving", false);
    }
}
