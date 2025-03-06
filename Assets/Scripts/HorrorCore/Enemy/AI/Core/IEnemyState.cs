using System;

public enum EnemyStateType
{
    Roaming,
    Investigating,
    Pursuing,
    Killing,
    GettingAlert,
}
public interface IEnemyState
{
    EnemyStateMachine SM { get; }
    EnemyStateType enemyStateType { get; }
    void Enter(EnemyContext context);
    void Execute(EnemyContext context);
    void Exit(EnemyContext context);
}