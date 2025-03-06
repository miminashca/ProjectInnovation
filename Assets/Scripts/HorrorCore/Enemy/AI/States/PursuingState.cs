using UnityEngine;

public class PursuingState : IEnemyState
{
    public EnemyStateType enemyStateType { get; }
    public PursuingState()
    {
        this.enemyStateType = EnemyStateType.Pursuing;
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