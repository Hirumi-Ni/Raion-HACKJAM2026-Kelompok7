using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAnimal : MonoBehaviour, ICapturable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private NavMeshAgent agent;

    public abstract void OnCapture();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FlipSpriteOnMovement();
    }

    private void FlipSpriteOnMovement()
    {
        if (agent.velocity.x > .1f) transform.localScale = new Vector3(-1f, 1, 1);
        else if (agent.velocity.x < -.1f) transform.localScale = new Vector3(1f, 1, 1);
    }
}
