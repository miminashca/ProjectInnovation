using UnityEngine;

public class RoamingState : IEnemyState
{
    public EnemyStateType enemyStateType { get; }
    public RoamingState()
    {
        this.enemyStateType = EnemyStateType.Roaming;
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