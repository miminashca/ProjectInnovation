using UnityEngine;

public class VoiceDetectionVisualizer : MonoBehaviour
{
    // Reference to the SoundMeter or a similar component that provides amplitude.
    public SoundMeter soundMeter;

    // Base radius (when amplitude is low)
    public float baseRadius = 1f;

    // Multiplier to scale the amplitude into a detection range.
    public float radiusMultiplier = 5f;

    // Gizmo color for visualization.
    public Color gizmoColor = Color.cyan;

    private void OnDrawGizmos()
    {
        if (soundMeter == null)
            return;

        // Calculate current radius based on the SoundMeter’s amplitude.
        float currentRadius = baseRadius + soundMeter.currentFill * radiusMultiplier;
        Gizmos.color = gizmoColor;
        DrawCircle(transform.position, currentRadius, 32);
    }

    // Helper function to draw a circle with a given number of segments.
    private void DrawCircle(Vector3 center, float radius, int segments)
    {
        float angle = 0f;
        Vector3 prevPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        for (int i = 1; i <= segments; i++)
        {
            angle += (2 * Mathf.PI) / segments;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
}
