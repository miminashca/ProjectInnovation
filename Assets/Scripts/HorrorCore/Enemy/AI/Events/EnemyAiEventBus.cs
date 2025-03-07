using System;

public static class EnemyAiEventBus
{
    public static event Action<EnemyStateType> OnEnterStateWithID;
    public static event Action<EnemyStateType> OnExitStateWithID;

    public static void EnterStateWithEnum(EnemyStateType type)
    {
        OnEnterStateWithID?.Invoke(type);
    }
    public static void ExitStateWithEnum(EnemyStateType type)
    {
        OnExitStateWithID?.Invoke(type);
    }

}
