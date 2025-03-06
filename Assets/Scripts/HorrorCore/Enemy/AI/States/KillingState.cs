using UnityEngine;

public class KillingState : IEnemyState
{
    public EnemyStateType enemyStateType { get; }
    public KillingState()
    {
        this.enemyStateType = EnemyStateType.Killing;
    }
    public void Enter(EnemyContext context)
    {
        throw new System.NotImplementedException();
    }

    public void Execute(EnemyContext context)
    {
        throw new System.NotImplementedException();
    }

    public void Exit(EnemyContext context)
    {
        throw new System.NotImplementedException();
    }
}