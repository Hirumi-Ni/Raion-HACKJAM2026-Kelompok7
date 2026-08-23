using Unity.VisualScripting;
using UnityEngine;

public class LoopController : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;

    [Header("Loop Detection")]
    [SerializeField] private int ignoreLastPoints = 4;
    [SerializeField] private float intersectionTolerance = .05f;

    [Header("Capture")]
    [SerializeField] private LayerMask captureLayer;

    private Vector3[] trailPoints;

    private void Update()
    {
        if (!trailRenderer.emitting) return;
    }

    private bool FindLoop(
    Vector3[] points,
    out int startIndex)
    {
        startIndex = -1;

        Vector2 current =
            points[points.Length - 1];

        for (int i = 0;
             i < points.Length - ignoreLastPoints;
             i++)
        {
            if (Vector2.Distance(
                    current,
                    points[i])
                < intersectionTolerance)
            {
                startIndex = i;
                return true;
            }
        }

        return false;
    }
}
