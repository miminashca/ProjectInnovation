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
        Debug.Log("Enter Investigating state");
        // Reset the accumulated noise when starting investigation.
        context.accumulateLoudness = 0f;
        investigateTimer = 0f;
        // Use chaseSpeed (or you could use a multiplier if desired).
        context.navAgent.speed = context.chaseSpeed;
        context.animator.SetBool("IsInvestigating", true);

        // Set destination to the last heard noise position.
        context.navAgent.SetDestination(context.lastHeardNoisePosition);
    }

    public void Execute(EnemyContext context)
    {
        investigateTimer += Time.deltaTime;

        // If the player is spotted, switch to pursuing.
        if (context.playerInVision)
        {
            SM.SetState(new PursuingState(SM));
            return;
        }

        // Optionally, if the enemy reaches the destination and has looked around long enough, revert to roaming.
        if (!context.navAgent.pathPending && context.navAgent.remainingDistance < 1f)
        {
            // Here you could add “searching” behavior.
            if (investigateTimer >= context.investigateTimeout)
            {
                SM.SetState(new RoamingState(SM));
                return;
            }
        }
    }

    public void Exit(EnemyContext context)
    {
        context.animator.SetBool("IsInvestigating", false);
    }
}
