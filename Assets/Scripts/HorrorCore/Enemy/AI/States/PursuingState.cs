using UnityEngine;

public class PursuingState : IEnemyState
{
    private float lostPlayerTimer = 0f;
    public EnemyStateMachine SM { get; }
    public EnemyStateType enemyStateType { get; }
    public PursuingState(EnemyStateMachine SM)
    {
        this.SM = SM;
        this.enemyStateType = EnemyStateType.Pursuing;
    }

    public void Enter(EnemyContext context)
    {
        context.animator.SetBool("IsPursuing", true);
        context.navAgent.speed = context.chaseSpeed;
        lostPlayerTimer = 0f;
    }

    public void Execute(EnemyContext context)
    {
        if (context.playerTransform != null)
        {
            // Move towards the player's current position:
            context.navAgent.SetDestination(context.playerTransform.position);

            // Check distance for Killing
            float distance = Vector3.Distance(
                context.navAgent.transform.position,
                context.playerTransform.position
            );
            if (distance <= context.killDistance)
            {
                SM.SetState(new KillingState(SM));
                return;
            }
        }

        // If we lose vision, increment lostPlayerTimer
        if (!context.playerInVision)
        {
            lostPlayerTimer += Time.deltaTime;
        }
        else
        {
            lostPlayerTimer = 0f; // reset if we see the player again
        }

        // If we lost the player for too long => go Investigate or revert to Roaming
        if (lostPlayerTimer >= context.pursuingVisionLostTime)
        {
            // Move to last known location and investigate
            context.lastHeardNoisePosition = context.playerTransform.position;
            SM.SetState(new InvestigatingState(SM));
        }
    }

    public void Exit(EnemyContext context)
    {
        context.animator.SetBool("IsPursuing", false);
    }
}