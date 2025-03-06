using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    [SerializeField] private float visionAngle = 60f;
    [SerializeField] private float visionRange = 10f;

    public bool CheckPlayerInVision(Transform playerTransform)
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < visionAngle * 0.5f)
        {
            // Check if there’s a line of sight
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, visionRange))
            {
                if (hit.transform == playerTransform)
                {
                    return true;
                }
            }
        }
        return false;
    }
}