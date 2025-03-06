using UnityEngine;

public class InvestigatingState : IEnemyState
{
    public EnemyStateType enemyStateType { get; }
    public InvestigatingState()
    {
        this.enemyStateType = EnemyStateType.Investigating;
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