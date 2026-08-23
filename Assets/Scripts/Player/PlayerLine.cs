using UnityEngine;

public class PlayerLine : MonoBehaviour
{
    private TrailRenderer trailRenderer;
    private Vector3[] trailPositions = new Vector3[0];
    
    private void Awake()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        trailPositions = new Vector3[trailRenderer.positionCount];
    }

    // Update is called once per frame
    void Update()
    {
        int currentCount = trailRenderer.positionCount;

        if (trailPositions.Length < currentCount)
        {
            trailPositions = new Vector3[currentCount];
        }

        int numPositions = trailRenderer.GetPositions(trailPositions);
        for (int i = 0; i < numPositions; i++)
        {
            Vector3 pos = trailPositions[i];
            Debug.Log($"Current Trail Count: {currentCount} at Position: {pos}");
        }
    }
}
