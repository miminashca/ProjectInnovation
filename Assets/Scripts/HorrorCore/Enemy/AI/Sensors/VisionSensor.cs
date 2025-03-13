using System;
using UnityEditor;
using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    // vision sensor reads enemies vision raycast input, and checks if it passes the threshold for enemy to see the player.
    // if that is true - it passes this information to AI director, which in its turn passes this event to state machine.
    
    [SerializeField] private float visionAngle = 60f;
    [SerializeField] private float visionRange = 10f;
    private EnemyStateMachine SM;
    private bool executed = false;
    
    private void Start()
    {
        SM = GetComponent<EnemyStateMachine>();
    }
   
    private void Update()
    {
        if (SM.context.playerTransform)
        {
            SM.context.playerInVision = CheckPlayerInVision(SM.context.playerTransform);

            if (!executed)
            {
                if (CheckPlayerInVision(SM.context.playerTransform))
                {
                    AIDirector.SpotPlayer();
                    executed = true;
                }
            }
            else
            {
                if (!CheckPlayerInVision(SM.context.playerTransform))
                {
                    AIDirector.LosePlayer();
                    executed = false;
                }
            }
        }
        
    }
    
    public bool CheckPlayerInVision(Transform playerTransform)
    {
        Vector3 playerHeadPosition = playerTransform.position + SM.context.playerHeadOffset;
        Vector3 enemyHeadPosition = transform.position + SM.context.enemyHeadOffset;
        
        Vector3 directionToPlayer = (playerHeadPosition - enemyHeadPosition).normalized;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (angle < visionAngle * 0.5f)
        {
            // Check if there’s a line of sight
            if (Physics.Raycast(enemyHeadPosition, directionToPlayer, out RaycastHit hit, visionRange, ~(1<<LayerMask.NameToLayer("Enemy"))))
            {
                if (hit.transform == playerTransform)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    // Draw the vision cone and ray in the Scene view using Gizmos
    /*private void OnDrawGizmos()
    {
        //if (!SM.context.playerTransform) return;
        // Calculate enemy head position based on offset
        Vector3 enemyHeadPosition = transform.position + SM.context.enemyHeadOffset;

        // Draw the vision cone boundaries
        Quaternion leftRayRotation = Quaternion.AngleAxis(-visionAngle * 0.5f, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(visionAngle * 0.5f, Vector3.up);
        Quaternion upRayRotation = Quaternion.AngleAxis(-visionAngle * 0.5f, Vector3.right);
        Quaternion downRayRotation = Quaternion.AngleAxis(visionAngle * 0.5f, Vector3.right);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Vector3 upRayDirection = upRayRotation * transform.forward;
        Vector3 downRayDirection = downRayRotation * transform.forward;

        if (CheckPlayerInVision(SM.context.playerTransform))
        {
            Gizmos.color = Color.red;
            //Handles.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
            //Handles.color = Color.green;
        }
        Gizmos.DrawRay(enemyHeadPosition, leftRayDirection * visionRange);
        Gizmos.DrawRay(enemyHeadPosition, rightRayDirection * visionRange);
        Gizmos.DrawRay(enemyHeadPosition, upRayDirection * visionRange);
        Gizmos.DrawRay(enemyHeadPosition, downRayDirection * visionRange);
        
        //Handles.DrawWireArc(enemyHeadPosition, Vector3.up, leftRayDirection, visionAngle, visionRange);
        //Handles.DrawWireArc(enemyHeadPosition, Vector3.right, upRayDirection, visionAngle, visionRange);
        
        Vector3 playerHeadPosition = SM.context.playerTransform.position + SM.context.playerHeadOffset;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(enemyHeadPosition, playerHeadPosition);
    }*/

}