using UnityEngine;

/// <summary>
/// States for the Enemy.
/// </summary>
public enum EnemyStateType
{
    Roaming,
    Investigating,
    Pursuing,
    Killing,
    GettingAlert,
}

/// <summary>
/// Interface that each Enemy State must implement.
/// Ensures a common contract with Enter, Execute, and Exit methods.
/// </summary>
public interface IEnemyState
{
    /// <summary>
    /// Reference to the EnemyStateMachine that owns this state.
    /// </summary>
    EnemyStateMachine SM { get; }

    /// <summary>
    /// The type of the current state (used for logging, transitions, etc.).
    /// </summary>
    EnemyStateType enemyStateType { get; }

    /// <summary>
    /// Called once when we first enter the state.
    /// </summary>
    void Enter(EnemyContext context);

    /// <summary>
    /// Called every frame while this state is active.
    /// </summary>
    void Execute(EnemyContext context);

    /// <summary>
    /// Called once when we exit the state.
    /// </summary>
    void Exit(EnemyContext context);
}
