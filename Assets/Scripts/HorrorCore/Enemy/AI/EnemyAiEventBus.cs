using UnityEngine;
using System;

public static class EnemyAiEventBus
{
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
