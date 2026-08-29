using UnityEngine;
using System.Collections.Generic;

//shoutout to these honorable mention:
// https://medium.com/@markomeara98/calculating-intersection-points-in-unity-cf010c155491
// https://discussions.unity.com/t/allowing-player-to-draw-circle-around-enemies-best-method/557016/6
// https://discussions.unity.com/t/detecting-loops/513976/8
// https://discussions.unity.com/t/how-to-check-if-an-object-is-surrounded/100612
// and not but not least cet jipiti incognito mode
public class TrailLoopDetector : MonoBehaviour
{
    [SerializeField] private TrailCapture trailCapture;
    private TrailPathRecorder pathRecorder;
    private TrailController trailController;
    private bool wasTrailActive;

    #region DebugAttribute
    // Debug
    private Vector2 debugA;
    private Vector2 debugB;
    private Vector2 debugC;
    private Vector2 debugD;
    private Vector2 debugIntersection;
    private bool showDebugSegments;
    private bool showIntersection;
    private List<Vector2> debugLoop;
    #endregion

    private void Awake()
    {
        pathRecorder = GetComponent<TrailPathRecorder>();
        trailController = GetComponent<TrailController>();
    }

    private void Update()
    {
        bool isTrailActive = trailController.isTrailActive;

        if (isTrailActive && !wasTrailActive)
        {
            OnTrailStart();
        }

        if (!isTrailActive && wasTrailActive)
        {
            OnTrailEnd();
        }

        wasTrailActive = isTrailActive;
    }

    private void OnTrailStart()
    {
        pathRecorder.points.Clear();

        showDebugSegments = false;
        showIntersection = false;
    }

    private void OnTrailEnd()
    {
        List<Vector2> points = pathRecorder.points;

        if (points.Count < 4) return;

        if (CheckClosedLoop(points, out List<Vector2> loop))
        {
            Debug.Log("Loop successfully detected!");
            debugLoop = loop;

            if (trailController.isTrailHeal)
            {
                trailController.GainTrailResourceFromArea(CalculateLoopArea(loop));
            }
            else
            {
                trailCapture.Capture(loop.ToArray());
            }
        }
        else
        {
            Debug.Log("No Loop Detected");
        }

        points.Clear();
    }

    private bool CheckClosedLoop(List<Vector2> points, out List<Vector2> loop)
    {
        loop = null;

        // Check every segment against every non-adjacent segment
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 a = points[i];
            Vector2 b = points[i + 1];

            for (int j = i + 2; j < points.Count - 1; j++)
            {
                Vector2 c = points[j];
                Vector2 d = points[j + 1];

                // Debug: show the segments being tested
                debugA = a;
                debugB = b;
                debugC = c;
                debugD = d;
                showDebugSegments = true;

                if (TryGetIntersection(a, b, c, d, out Vector2 intersection))
                {
                    debugIntersection = intersection;
                    showIntersection = true;

                    Debug.Log($"Intersection Found!\n" + $"Segment A: {a} -> {b}\n" + $"Segment B: {c} -> {d}\n" + $"Intersection: {intersection}");

                    loop = new List<Vector2>();

                    // Add the actual intersection point first
                    loop.Add(intersection);

                    // Add the trail points between the two
                    // intersecting segments.
                    for (int k = i + 1; k <= j; k++)
                    {
                        loop.Add(points[k]);
                    }
                    return true;
                }
            }
        }
        return false;
    }

    private bool TryGetIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 intersection)
    {
        intersection = Vector2.zero;

        float denominator = (b.x - a.x) * (d.y - c.y) - (b.y - a.y) * (d.x - c.x);

        if (Mathf.Abs(denominator) < 0.0001f) return false;

        float t = ((c.x - a.x) * (d.y - c.y) - (c.y - a.y) * (d.x - c.x)) / denominator;
        float u = ((c.x - a.x) * (b.y - a.y) - (c.y - a.y) * (b.x - a.x)) / denominator;

        if (t < 0f || t > 1f || u < 0f || u > 1f) return false;

        // Get the actual intersection position
        intersection = a + t * (b - a);

        return true;
    }

    private float CalculateLoopArea(List<Vector2> loop) //buat ngukur luas polygon buat heal doang // shoelace formula
    {
        float area = 0f;

        for (int i = 0; i < loop.Count; i++)
        {
            Vector2 current = loop[i];
            Vector2 next = loop[(i + 1) % loop.Count];

            area += current.x * next.y;
            area -= next.x * current.y;
        }

        return Mathf.Abs(area) * 0.5f;
    }

    private void OnDrawGizmos()
    {
        // Tested segments
        if (showDebugSegments)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(debugA, debugB);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(debugC, debugD);
        }

        // Intersection
        if (showIntersection)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(debugIntersection, 0.1f);
        }

        // Generated loop
        if (debugLoop != null && debugLoop.Count >= 2)
        {
            Gizmos.color = Color.magenta;

            for (int i = 0; i < debugLoop.Count; i++)
            {
                Vector2 a = debugLoop[i];
                Vector2 b = debugLoop[(i + 1) % debugLoop.Count];

                Gizmos.DrawLine(a, b);
            }
        }
    }
}

