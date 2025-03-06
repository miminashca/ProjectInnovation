using UnityEngine;

public class InvestigatingState : IEnemyState
{
    private float investigateTimer = 0f;
    public EnemyStateMachine SM { get; }
    public EnemyStateType enemyStateType { get; }
    public InvestigatingState(EnemyStateMachine SM)
    {
        this.SM = SM;
        this.enemyStateType = EnemyStateType.Investigating;
    }

    public void Enter(EnemyContext context)
    {
        investigateTimer = 0f;
        context.navAgent.speed = context.chaseSpeed * 0.8f; // slightly faster than roaming, if desired
        context.animator.SetBool("IsInvestigating", true);

        // Move to lastHeardNoisePosition (or a random point within a radius).
        context.navAgent.SetDestination(context.lastHeardNoisePosition);
    }

    public void Execute(EnemyContext context)
    {
        investigateTimer += Time.deltaTime;

        // Check if we see the player => transition to Pursuing
        if (context.playerInVision)
        {
            SM.SetState(new PursuingState(SM));
            return;
        }

        // If we arrive near the noise position, either linger or do some searching pattern
        if (!context.navAgent.pathPending && context.navAgent.remainingDistance < 1f)
        {
            // Possibly pick a new random point within a certain “search radius” around lastHeardNoisePosition
        }

        // If we exceed the investigating timeout => revert to Roaming
        if (investigateTimer >= context.investigateTimeout)
        {
            SM.SetState(new RoamingState(SM));
        }
    }

    public void Exit(EnemyContext context)
    {
        context.animator.SetBool("IsInvestigating", false);
    }
}