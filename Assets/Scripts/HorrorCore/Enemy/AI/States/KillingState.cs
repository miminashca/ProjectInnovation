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
        // Trigger a kill animation
        context.animator.SetTrigger("Kill");
        // Possibly disable player movement, etc.
    }

    public void Execute(EnemyContext context)
    {
        // The kill animation is playing. Once done, you might:
        // - Trigger game over screen
        // - Or if the game has multiple lives, do something else
    }

    public void Exit(EnemyContext context)
    {
        // Clean up if needed
    }
}