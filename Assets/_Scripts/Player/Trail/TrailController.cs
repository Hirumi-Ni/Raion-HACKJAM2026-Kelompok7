using UnityEngine;
using System.Collections;

public class TrailController : MonoBehaviour
{
    private PlayerController playerController;

    [Header("Trail Visual")]
    [SerializeField] private TrailRenderer captureTrail;
    [SerializeField] private TrailRenderer healTrail;
    [SerializeField] private float loopColorMultiplier = 0.4f;
    private TrailRenderer trailRenderer;

    [Header("Trail Time")]
    [SerializeField] private float onHoldTrailTime = 6;
    private float onReleaseTrailTime;

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
    private bool trailStoppedByLoop;

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
        onReleaseTrailTime = onHoldTrailTime * 0.20f;
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

        if (currentTrailResource <= 0f && !isTrailHabis) 
        { 
            isTrailHabis = true; 
            EventHandler.WhenGameEnded(false); 
        }

        bool spaceHeld = InputManager.instance.GetSpaceKeyPress();
        
        if (!spaceHeld) 
        { 
            trailStoppedByLoop = false; 
            isTrailActive = false; 
            if (trailRenderer != null) 
            { 
                trailRenderer.emitting = false; 
                trailRenderer.time = onReleaseTrailTime; 
                StartCoroutine(DestroyTrailAfterTime(trailRenderer)); 
                trailRenderer = null; 
            } 
            return; 
        } 
        
        if (trailStoppedByLoop) 
        { 
            isTrailActive = false; 
            return; 
        } 

        if (spaceHeld && currentTrailResource > 0) 
        { 
            if (playerController.rb.linearVelocity.magnitude > .01f && !isGoldRushActive) 
            { 
                currentTrailResource -= depletionRate * Time.deltaTime; 
            }
            
            if (trailRenderer == null) 
            { 
                StartNewTrail(); 
            } 

            isTrailActive = true;

        } 
    } 
    
    private void StartNewTrail() 
    { 
        TrailRenderer trailPrefab = isTrailHeal ? healTrail : captureTrail; 
        trailRenderer = Instantiate( trailPrefab, transform.position, Quaternion.identity, transform); 
        trailRenderer.emitting = true; 
        trailRenderer.time = onHoldTrailTime; 
    } 
    public void StopTrail() 
    { 
        if (trailRenderer == null) return; 
        
        trailStoppedByLoop = true; 
        isTrailActive = false; 
        
        trailRenderer.emitting = false; 
        trailRenderer.time = onReleaseTrailTime; 
        
        StartCoroutine(DestroyTrailAfterTime(trailRenderer)); 
        trailRenderer = null; 
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

    public void SetTrailLoopColor()
    {
        if (trailRenderer == null) return; 
        
        Gradient originalGradient = trailRenderer.colorGradient; 
        GradientColorKey[] colorKeys = originalGradient.colorKeys; 
        GradientAlphaKey[] alphaKeys = originalGradient.alphaKeys; 
        
        for (int i = 0; i < colorKeys.Length; i++) 
        { 
            Color color = colorKeys[i].color; 
            color.r *= loopColorMultiplier; 
            color.g *= loopColorMultiplier; 
            color.b *= loopColorMultiplier; 
            colorKeys[i].color = color; 
        } 
        
        Gradient darkerGradient = new Gradient(); 
        darkerGradient.SetKeys(colorKeys, alphaKeys); 
        trailRenderer.colorGradient = darkerGradient; 
    }
}
