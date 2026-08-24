using UnityEngine;

public class TrailController : MonoBehaviour
{
    private TrailRenderer trailRenderer;

    [Header("Trail Time")]
    [SerializeField] private float onHoldTrailTime = 6;
    [SerializeField] private float onReleaseTrailTime = 2;

    [Header("Trail Resource")]
    [field: SerializeField] public float maxTrailResource { get; private set; } = 100f; //max health
    public float currentTrailResource { get; private set; } //current health
    [SerializeField] private float depletionRate = 5f; //kecepatan depletion per detik (ex: 25f per detik)
    [SerializeField] private float endThreshold = 10f; //klo current nyentuh endThreshold maka trail mati
    [SerializeField] private float gainTrailResource = 10f;

    private void Awake()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        EventHandler.OnTrailResourceGain += GainTrailResource;
    }

    private void OnDisable()
    {
        EventHandler.OnTrailResourceGain -= GainTrailResource;
    }

    private void Start()
    {
        trailRenderer.emitting = false;
        currentTrailResource = maxTrailResource;
    }

    private void Update()
    {
        HandleTrailOnInput();
    }

    private void HandleTrailOnInput()
    {
        if (InputManager.instance.GetSpaceKeyPress() && currentTrailResource >= endThreshold)
        {
            trailRenderer.emitting = true;
            trailRenderer.time = onHoldTrailTime;

            currentTrailResource -= depletionRate * Time.deltaTime;

            Debug.Log(currentTrailResource);
        }
        else
        {
            trailRenderer.emitting = false;
            trailRenderer.time = onReleaseTrailTime;
        }
    }

    public void GainTrailResource()
    {
        currentTrailResource += gainTrailResource; //ntar diubah lagi biar gak hardcode
        currentTrailResource = Mathf.Clamp(currentTrailResource, 0, maxTrailResource);
    }
}
