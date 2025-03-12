using UnityEngine;

public class VoiceDetectionTrigger : MonoBehaviour
{
    public SoundMeter soundMeter;       // Reference to the SoundMeter component.
    public float baseRadius = 1f;         // Base radius for detection when amplitude is low.
    public float radiusMultiplier = 5f;   // How much the amplitude scales the radius.

    // LayerMask for filtering enemy colliders.
    public LayerMask enemyLayer;

    // How long (in accumulated noise units) before an enemy is alerted.
    public float alertThreshold = 10f;

    // Internal accumulator that tracks the �noise exposure.�
    private float noiseAccumulator = 0f;

    void Update()
    {
        // Calculate the current detection radius based on the SoundMeter amplitude.
        float currentRadius = baseRadius + soundMeter.currentFill * radiusMultiplier;

        // Check for enemies within the dynamic detection circle.
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, currentRadius, enemyLayer);
        foreach (var hit in hitColliders)
        {
            // Try to get the enemy's state machine component.
            EnemyStateMachine enemySM = hit.GetComponent<EnemyStateMachine>();
            if (enemySM != null)
            {
                // Increase the accumulator based on the amplitude and time.
                noiseAccumulator += soundMeter.currentFill * Time.deltaTime;

                // If the accumulated noise exceeds the threshold, alert the enemy.
                if (noiseAccumulator >= alertThreshold)
                {
                    // Set the enemy�s last-heard noise position to the player's position.
                    enemySM.context.lastHeardNoisePosition = transform.position;

                    // Change the enemy's state � for example, go to the GettingAlert state.
                    //enemySM.SetState(new GettingAlertState(enemySM));

                    // Reset the accumulator once the alert is triggered.
                    noiseAccumulator = 0f;
                }
            }
        }
    }

    // Visualize the detection sphere in the Scene view.
    private void OnDrawGizmosSelected()
    {
        if (soundMeter == null)
            return;

        float currentRadius = baseRadius + soundMeter.currentFill * radiusMultiplier;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }
}
