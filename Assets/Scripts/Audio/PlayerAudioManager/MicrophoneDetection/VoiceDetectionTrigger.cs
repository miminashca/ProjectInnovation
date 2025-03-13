using UnityEngine;

public class VoiceDetectionTrigger : MonoBehaviour
{
    public SoundMeter soundMeter;           // Reference to the SoundMeter component.
    public float baseRadius = 1f;           // Base radius for detection when amplitude is low.
    public float radiusMultiplier = 5f;     // How much the amplitude scales the radius.

    public LayerMask enemyLayer;            // LayerMask for filtering enemy colliders.

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
                float distance = Vector2.Distance(new Vector2(enemySM.context.navAgent.transform.position.x,enemySM.context.navAgent.transform.position.z),new Vector2(transform.position.x,transform.position.z));
                //Debug.Log($"[VoiceDetectionTrigger] Detected player at distance: {distance:F2}");

                // Record the noise position of the player
                enemySM.context.lastHeardNoisePosition = transform.position;

                // Immediate pursuit if very close
                if (distance <= enemySM.context.immediatePursuitDistance)
                {
                    AIDirector.SpotPlayer();
                    //Debug.Log("[VoiceDetectionTrigger] Player is close! Triggering Pursuing state.");
                }
                else
                {
                    // Accumulate noise over time.
                    enemySM.context.accumulateLoudness += amplitude * Time.deltaTime;
                    //Debug.Log($"[VoiceDetectionTrigger] Enemy noise accumulation: {enemySM.context.accumulateLoudness:F2}");

                    // When noise threshold is reached, trigger an alert event.
                    if (enemySM.context.accumulateLoudness >= enemySM.context.alertThreshold)
                    {
                        AIDirector.ALert();
                        //Debug.Log("[VoiceDetectionTrigger] Noise threshold reached! Triggering Noise Alert.");
                    }
                }
            }
        }
    }

/*    private void OnDrawGizmos()
    {
        if (soundMeter == null)
            return;
        float currentRadius = baseRadius + soundMeter.currentFill * radiusMultiplier;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }*/
}
