using System;
using UnityEngine;

public static class AIDirector
{
    // listens to AI events, and passes the result to state machine, or to listeners like: animator.
    
    //SENSORS EVENTS
    public static event Action<Vector3, float> OnNoiseEvent;
    public static event Action OnPlayerSpotted;
    public static void PlayerMadeNoise(Vector3 position, float loudness)
    {
        OnNoiseEvent?.Invoke(position, loudness);
    }
    public static void SpotPlayer()
    {
        OnPlayerSpotted?.Invoke();
        Debug.Log("Enemy detects player!");
    }
    
    //STATES EVENTS
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

