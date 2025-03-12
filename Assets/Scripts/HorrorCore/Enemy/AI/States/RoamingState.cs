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

    public void Enter(EnemyContext context)
    {
        Debug.Log("Enter Roaming state");
        context.navAgent.speed = context.roamSpeed;
        SetNextPatrolPoint(context);
        context.animator.SetBool("IsRoaming", true);
    }

    public void Execute(EnemyContext context)
    {
        if (!context.navAgent.pathPending && context.navAgent.remainingDistance < 1f)
        {
            SetNextPatrolPoint(context);
        }

        if (context.playerInVision)
        {
            SM.SetState(new PursuingState(SM));
        }
    }

    public void Exit(EnemyContext context)
    {
        context.animator.SetBool("IsRoaming", false);
        context.navAgent.ResetPath();
    }

    private void SetNextPatrolPoint(EnemyContext context)
    {
        if (context.patrolPoints == null || context.patrolPoints.Length == 0)
            return;

        int oldIndex = context.currentPatrolIndex;
        int newIndex = oldIndex;

        while (newIndex == oldIndex)
        {
            newIndex = Random.Range(0, context.patrolPoints.Length);
        }

        context.currentPatrolIndex = newIndex;
        context.navAgent.SetDestination(context.patrolPoints[newIndex].position);
    }
}

