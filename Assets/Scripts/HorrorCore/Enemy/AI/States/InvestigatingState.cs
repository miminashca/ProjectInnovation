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
        Debug.Log("Enter investigating state");

        investigateTimer = 0f;
        context.navAgent.speed = context.chaseSpeed * 0.8f; // adjust speed as needed
        context.animator.SetBool("IsInvestigating", true);

        // Use the last heard noise position to set the destination
        context.navAgent.SetDestination(context.lastHeardNoisePosition);
    }

    public void Execute(EnemyContext context)
    {
        investigateTimer += Time.deltaTime;

        if (context.playerInVision)
        {
            SM.SetState(new PursuingState(SM));
            return;
        }

        // If reached the destination, you might add logic to search nearby
        if (!context.navAgent.pathPending && context.navAgent.remainingDistance < 1f)
        {
            // Search behavior can be added here.
        }

        // if (investigateTimer >= context.investigateTimeout)
        // {
        //     SM.SetState(new RoamingState(SM));
        // }
    }

    public void Exit(EnemyContext context)
    {
        context.animator.SetBool("IsInvestigating", false);
    }
}
