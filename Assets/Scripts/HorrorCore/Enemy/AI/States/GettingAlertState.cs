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
        //Debug.Log("Enter GettingAlert state");
        context.navAgent.speed = 0;
        context.navAgent.ResetPath();
        context.animator.SetBool("IsAlerting", true);
        alertTimeCounter = 0f;
    }

    public void Execute(EnemyContext context)
    {
        alertTimeCounter += Time.deltaTime;

        // If the enemy sees the player, transition immediately to pursuing.
        if (context.playerInVision)
        {
            SM.SetState(new PursuingState(SM));
            return;
        }

        // Wait for the alert animation to finish.
        if (alertTimeCounter >= context.alertDuration)
        {
            // After the alert animation, if noise threshold is met, go into investigating;
            // otherwise, return to roaming (or you could always choose investigating if that's preferred).
            if (context.accumulateLoudness >= context.alertThreshold)
            {
                SM.SetState(new InvestigatingState(SM));
            }
            else
            {
                SM.SetState(new RoamingState(SM));
            }
            return;
        }
    }

    public void Exit(EnemyContext context)
    {
        context.animator.SetBool("IsAlerting", false);
        context.navAgent.speed = context.roamSpeed;
    }
}
