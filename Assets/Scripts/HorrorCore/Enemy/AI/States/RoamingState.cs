using UnityEngine;

public class RoamingState : IEnemyState
{
    public EnemyStateMachine SM { get; }
    public EnemyStateType enemyStateType { get; }
    public RoamingState(EnemyStateMachine SM)
    {
        this.SM = SM;
        this.enemyStateType = EnemyStateType.Roaming;
    }
    public void Enter(EnemyContext context)
    {
        // E.g. set speed, set random or patrol destination
        context.navAgent.speed = context.roamSpeed;
        SetNextPatrolPoint(context);
        context.animator.SetBool("IsRoaming", true);
    }

    public void Execute(EnemyContext context)
    {
        // Move along route or random positions on the NavMesh
        if (!context.navAgent.pathPending && context.navAgent.remainingDistance < 1f)
        {
            SetNextPatrolPoint(context);
        }

        // Check if there's a loud sound above threshold => transition
        if (context.audioSensor.CurrentLoudness >= context.alertThreshold)
        {
            // Switch to GettingAlert state
            context.navAgent.ResetPath(); 
            // Access your StateMachine (often via a reference or an event).
            // For brevity, we’ll assume we have a static reference or pass it in:
            SM.SetState(new GettingAlertState(SM));
        }

        // Or if the player is directly seen for a “considerable amount of time”:
        if (context.playerInVision)
        {
            // Possibly accumulate time in vision and then transition to Pursuing:
            // (Implement the "spotted for X seconds" check)
            SM.SetState(new PursuingState(SM));
        }
    }

    public void Exit(EnemyContext context)
    {
        // Clean up or reset flags
        context.animator.SetBool("IsRoaming", false);
    }
    
    private void SetNextPatrolPoint(EnemyContext context)
    {
        if (context.patrolPoints == null || context.patrolPoints.Length == 0)
            return;

        context.currentPatrolIndex = (context.currentPatrolIndex + 1) % context.patrolPoints.Length;
        context.navAgent.SetDestination(context.patrolPoints[context.currentPatrolIndex].position);
    }
}