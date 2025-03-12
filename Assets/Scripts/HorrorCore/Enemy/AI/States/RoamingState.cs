using UnityEngine;

/// <summary>
/// Enemy moves randomly among given patrol points without returning
/// immediately to the same waypoint. Also listens for audio or vision to transition.
/// </summary>
public class RoamingState : IEnemyState
{
    public EnemyStateMachine SM { get; }
    public EnemyStateType enemyStateType { get; }

    public RoamingState(EnemyStateMachine SM)
    {
        this.SM = SM;
        this.enemyStateType = EnemyStateType.Roaming;
    }

    /// <summary>
    /// Called once when we first enter the RoamingState.
    /// </summary>
    public void Enter(EnemyContext context)
    {
        Debug.Log("Enter roaming state");
        // Set the navAgent speed to roamSpeed
        context.navAgent.speed = context.roamSpeed;

        // Choose the first patrol point to move to
        SetNextPatrolPoint(context);

        // Tell the animator we are roaming (e.g., triggers a walk animation)
        context.animator.SetBool("IsRoaming", true);
    }

    /// <summary>
    /// Called every frame while in RoamingState.
    /// </summary>
    public void Execute(EnemyContext context)
    {
        // Check if we have reached our current patrol destination
        if (!context.navAgent.pathPending && context.navAgent.remainingDistance < 1f)
        {
            // Pick the next patrol point
            SetNextPatrolPoint(context);
        }

        // // Transition check #1: If loud sound is detected above threshold
        // if (context.audioSensor.CurrentLoudness >= context.alertThreshold)
        // {
        //     // Reset path and go to GettingAlertState
        //     context.navAgent.ResetPath();
        //     SM.SetState(new GettingAlertState(SM));
        //     return;
        // }

        // Transition check #2: If we see the player
        if (context.playerInVision)
        {
            // Go straight to PursuingState
            SM.SetState(new PursuingState(SM));
        }
    }

    /// <summary>
    /// Called once when we exit the RoamingState.
    /// </summary>
    public void Exit(EnemyContext context)
    { 
        // Reset the IsRoaming animator bool so we don't continue the roaming animation
        context.animator.SetBool("IsRoaming", false);
        context.navAgent.ResetPath();
    }

    /// <summary>
    /// Picks a new patrol point at random that is NOT the same as the current one.
    /// </summary>
    private void SetNextPatrolPoint(EnemyContext context)
    {
        // If no patrol points, just return
        if (context.patrolPoints == null || context.patrolPoints.Length == 0)
            return;

        int oldIndex = context.currentPatrolIndex;
        int newIndex = oldIndex;

        // Ensure we pick a new index that is different from the old one
        while (newIndex == oldIndex)
        {
            newIndex = Random.Range(0, context.patrolPoints.Length);
        }

        // Assign and move to the new patrol point
        context.currentPatrolIndex = newIndex;
        context.navAgent.SetDestination(context.patrolPoints[newIndex].position);
    }

}
