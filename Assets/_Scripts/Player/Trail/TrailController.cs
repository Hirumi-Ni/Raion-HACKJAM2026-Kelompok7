using UnityEngine;
using System.Collections;

public class TrailController : MonoBehaviour
{
    private PlayerController playerController;

    [Header("Trail Visual")]
    [SerializeField] private TrailRenderer captureTrail;
    [SerializeField] private TrailRenderer healTrail;
    private TrailRenderer trailRenderer;

    [Header("Trail Time")]
    [SerializeField] private float onHoldTrailTime = 6;
    [SerializeField] private float onReleaseTrailTime = 2;

    [Header("Trail Resource")]
    [SerializeField] private float depletionRate = 5f; //kecepatan depletion per detik (ex: 25f per detik)
    [SerializeField] private float gainTrailPerArea = 1.5f;
    [SerializeField] private float maxGainTrailPerArea = 40f;

    public float maxTrailResource { get; private set; } //max health
    public float currentTrailResource { get; private set; } //current health
    public bool isTrailActive { get; private set; }
    public bool isTrailHeal { get; private set; } = false;
    private bool isTrailHabis = false; //lupa bingnya habis apa
    private bool isGoldRushActive = false;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        EventHandler.OnIncreaseTrailResource += GainTrailResource;
        EventHandler.OnDecreaseTrailResource += DecreaseTrailResource;
        EventHandler.OnGoldRush += HandleGoldRush;
    }

    private void OnDisable()
    {
        EventHandler.OnIncreaseTrailResource -= GainTrailResource;
        EventHandler.OnDecreaseTrailResource -= DecreaseTrailResource;
        EventHandler.OnGoldRush -= HandleGoldRush;
    }

    private void Start()
    {
        maxTrailResource = playerController.playerMaxHealth;

        trailRenderer = null;

        currentTrailResource = maxTrailResource;
    }

    private void Update()
    {
        HandleTrailOnInput();
    }

    private void HandleTrailOnInput()
    {
        if (InputManager.instance.GetSwitchKeyPress())
        {
            isTrailHeal = !isTrailHeal;
        }

        if (InputManager.instance.GetSpaceKeyPress() && currentTrailResource > 0)
        {
            if (trailRenderer == null)
            {
                StartNewTrail();
            }

            isTrailActive = true;

            if (playerController.rb.linearVelocity.magnitude > .01f || !isGoldRushActive)
            {
                currentTrailResource -= depletionRate * Time.deltaTime;
            }

            if (currentTrailResource <= 0f && !isTrailHabis)
            {
                isTrailHabis = true;
                EventHandler.WhenGameEnded(false);
            }
        }
        else
        {
            isTrailActive = false;

            if (trailRenderer != null)
            {
                trailRenderer.emitting = false;
                trailRenderer.time = onReleaseTrailTime;

                StartCoroutine(DestroyTrailAfterTime(trailRenderer));

                trailRenderer = null;
            }
        }
    }

    private void StartNewTrail()
    {
        TrailRenderer trailPrefab = isTrailHeal ? healTrail : captureTrail;

        trailRenderer = Instantiate(trailPrefab, transform.position, Quaternion.identity, transform);

        trailRenderer.emitting = true;
        trailRenderer.time = onHoldTrailTime;
    }

    public void GainTrailResourceFromArea(float area)
    {
        float amount = Mathf.Min(area * gainTrailPerArea, maxGainTrailPerArea);
        GainTrailResource(amount);
    }

    public void GainTrailResource(float amount)
    {
        currentTrailResource += amount; 
        currentTrailResource = Mathf.Clamp(currentTrailResource, 0, maxTrailResource);
    }

    public void DecreaseTrailResource(float amount)
    {
        currentTrailResource -= amount;
        currentTrailResource = Mathf.Clamp(currentTrailResource, 0, maxTrailResource);
    }

    private IEnumerator DestroyTrailAfterTime(TrailRenderer trail)
    {
        yield return new WaitForSeconds(onReleaseTrailTime);
        Destroy(trail.gameObject);
    }
    
    private void HandleGoldRush(float duration)
    {
        isGoldRushActive = true;
        StartCoroutine(GoldRushCoroutine(duration));
    }

    private IEnumerator GoldRushCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isGoldRushActive = false;
    }
}
