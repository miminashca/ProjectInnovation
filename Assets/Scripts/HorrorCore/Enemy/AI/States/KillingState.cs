using UnityEngine;

public class KillingState : IEnemyState
{
    public EnemyStateMachine SM { get; }
    public EnemyStateType enemyStateType { get; }
    public KillingState(EnemyStateMachine SM)
    {
        this.SM = SM;
        this.enemyStateType = EnemyStateType.Killing;
    }
    public void Enter(EnemyContext context)
    {
        Debug.Log("Enter killing state!");
        // Trigger a kill animation
        context.animator.SetBool("IsKilling", true);

        // Possibly disable player movement, etc.
        AIDirector.KillPlayer();
        OnKillAnimationEnd();
    }

    public void Execute(EnemyContext context)
    {
    }

    public void Exit(EnemyContext context)
    {
    }
    
    public void OnKillAnimationEnd()
    {
        Debug.Log("Kill animation ended, triggering LoseGame event.");
        EventBus.LoseGame();
    }
}