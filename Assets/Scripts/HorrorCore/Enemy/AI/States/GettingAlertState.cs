using UnityEngine;

public class GettingAlertState : IEnemyState
{
    public EnemyStateType enemyStateType { get; }
    public GettingAlertState()
    {
        this.enemyStateType = EnemyStateType.GettingAlert;
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
