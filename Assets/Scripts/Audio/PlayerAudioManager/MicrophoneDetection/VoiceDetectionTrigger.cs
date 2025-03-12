using UnityEngine;

public class VoiceDetectionTrigger : MonoBehaviour
{
    public SoundMeter soundMeter;       // Reference to the SoundMeter component.
    public float baseRadius = 1f;         // Base radius for detection when amplitude is low.
    public float radiusMultiplier = 5f;   // How much the amplitude scales the radius.

    // LayerMask for filtering enemy colliders.
    public LayerMask enemyLayer;

    void Update()
    {
        float amplitude = soundMeter.currentFill;
        float currentRadius = baseRadius + amplitude * radiusMultiplier;

        // Check for enemy colliders within the dynamic detection sphere.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, currentRadius, enemyLayer);
        foreach (var hit in hitColliders)
        {
            EnemyStateMachine enemySM = hit.GetComponent<EnemyStateMachine>();
            if (enemySM != null)
            {
                float distance = Vector3.Distance(transform.position, enemySM.transform.position);
                Debug.Log($"[VoiceDetectionTrigger] Detected player at distance: {distance:F2}");

                // Record the noise position for the enemy.
                enemySM.context.lastHeardNoisePosition = transform.position;

                // Immediate pursuit if very close.
                if (distance <= enemySM.context.immediatePursuitDistance)
                {
                    Debug.Log("[VoiceDetectionTrigger] Player is close! Triggering Pursuing state.");
                    enemySM.SetState(new PursuingState(enemySM));
                }
                else
                {
                    // Accumulate noise using the context’s accumulator.
                    enemySM.context.accumulateLoudness += amplitude * Time.deltaTime;
                    Debug.Log($"[VoiceDetectionTrigger] Enemy noise accumulation: {enemySM.context.accumulateLoudness:F2}");

                    // If accumulated noise reaches or exceeds threshold and enemy is not already alerting/investigating:
                    if (enemySM.context.accumulateLoudness >= enemySM.context.alertThreshold)
                    {
                        // Trigger Getting Alert state.
                        Debug.Log("[VoiceDetectionTrigger] Noise threshold reached! Triggering GettingAlert state.");
                        enemySM.SetState(new GettingAlertState(enemySM));
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (soundMeter == null)
            return;
        float currentRadius = baseRadius + soundMeter.currentFill * radiusMultiplier;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }
}
