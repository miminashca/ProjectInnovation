using UnityEngine;

public class GettingAlertState : IEnemyState
{
    private float alertTimeCounter = 0f;
    private const float MAX_ALERT_DURATION = 2f; // Example: how long we remain in this "startled" reaction
    public EnemyStateMachine SM { get; }
    public EnemyStateType enemyStateType { get; }
    public GettingAlertState(EnemyStateMachine SM)
    {
        this.SM = SM;
        this.enemyStateType = EnemyStateType.GettingAlert;
    }

    public void Enter(EnemyContext context)
    {
        // Possibly play a “startled” animation
        context.animator.SetTrigger("Alerted");
        alertTimeCounter = 0f;
    }

    public void Execute(EnemyContext context)
    {
        alertTimeCounter += Time.deltaTime;

        // For example, transition to Investigating if repeated or stronger noise
        // or if we have “recent noise location” from the AI Director
        if (context.hasRecentNoise || context.accumulateLoudness >= context.alertThreshold)
        {
            SM.SetState(new InvestigatingState(SM));
            return;
        }

        // If we remain “GettingAlert” beyond a small duration, revert to Roaming
        if (alertTimeCounter >= MAX_ALERT_DURATION)
        {
            SM.SetState(new RoamingState(SM));
        }
    }

    public void Exit(EnemyContext context)
    {
        // Reset the loudness for next time (optional design choice)
        context.accumulateLoudness = 0f;
        context.hasRecentNoise = false;
    }
}
