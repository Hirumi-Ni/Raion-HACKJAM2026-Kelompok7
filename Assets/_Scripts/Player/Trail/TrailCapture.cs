using UnityEngine;
using System.Collections.Generic;

public class TrailCapture : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D captureCollider;
    private List<Collider2D> results = new();

    private void Start()
    {
        captureCollider.enabled = false;
    }

    public void Capture(Vector2[] polygon)
    {
        results.Clear();
        
        captureCollider.SetPath(0, polygon);
        captureCollider.enabled = true;
        captureCollider.Overlap(results);

        foreach (Collider2D collider in results)
        {
            Debug.Log($"Detected: {collider.name}");

            if (!collider.CompareTag("Captureable")) return;
            
            ICapturable capturable = collider.GetComponent<ICapturable>();
            capturable?.OnCapture();
        }

        captureCollider.enabled = false;
    }
}
