using UnityEngine;

public class GettingAlertState : IEnemyState
{
    private float alertTimeCounter = 0f;
    public EnemyStateMachine SM { get; }
    public EnemyStateType enemyStateType { get; }

    public GettingAlertState(EnemyStateMachine SM)
    {
        this.SM = SM;
        this.enemyStateType = EnemyStateType.GettingAlert;
    }

    public void Enter(EnemyContext context)
    {
        Debug.Log("Enter GettingAlert state");
        context.animator.SetTrigger("Alerted");
        alertTimeCounter = 0f;
    }

    public void Execute(EnemyContext context)
    {
        alertTimeCounter += Time.deltaTime;

        // If the enemy sees the player at any point, switch to pursuing.
        if (context.playerInVision)
        {
            SM.SetState(new PursuingState(SM));
            return;
        }

        // If the accumulated noise has reached the threshold, transition to investigating.
        if (context.accumulateLoudness >= context.alertThreshold)
        {
            SM.SetState(new InvestigatingState(SM));
            return;
        }

        // After the alert duration, return to roaming while preserving the accumulated noise.
        if (alertTimeCounter >= context.alertDuration)
        {
            SM.SetState(new RoamingState(SM));
            return;
        }
    }

    public void Exit(EnemyContext context)
    {
        // Do NOT reset accumulateLoudness here so that the enemy “remembers” the noise.
        // It will be reset only when entering the Investigating state.
    }
}
