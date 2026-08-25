using UnityEngine;
using System.Collections.Generic;

public class TrailPathRecorder : MonoBehaviour
{
    [SerializeField] private float pointDistance = .1f;
    private TrailController trailController;
    public List<Vector2> points { get; private set; } = new();

    private void Awake()
    {
        trailController = GetComponent<TrailController>();
    }

    private void Update()
    {
        if (trailController.isTrailActive) RecordPoint();
    }

    private void RecordPoint()
    {
        Vector2 currentPos = transform.position;

        if (points.Count == 0)
        {
            points.Add(currentPos);
            return;
        }

        float distance = Vector2.Distance(points[points.Count - 1], currentPos);

        if (distance >= pointDistance)
        {
            points.Add(currentPos);
        }
    }

    private void OnDrawGizmos()
    {
        if (points == null || points.Count < 2)
            return;

        Gizmos.color = Color.green;

        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }

        foreach (Vector2 point in points)
        {
            Gizmos.DrawSphere(point, 0.05f);
        }
    }
}
