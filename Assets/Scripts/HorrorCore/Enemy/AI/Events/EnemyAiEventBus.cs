using System;

public static class EnemyAiEventBus
{
    // listens to state events, and passes the result to listeners like: animator.
    public static event Action<EnemyStateType> OnEnterStateWithID;
    public static event Action<EnemyStateType> OnExitStateWithID;

    public static void EnterStateWithID(EnemyStateType type)
    {
        OnEnterStateWithID?.Invoke(type);
    }
    public static void ExitStateWithID(EnemyStateType type)
    {
        OnExitStateWithID?.Invoke(type);
    }

}
