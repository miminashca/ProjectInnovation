using System;
using UnityEngine;

public static class AIDirector
{
    // listens to AI events, and passes the result to state machine, or to listeners like: animator.



    //SENSORS EVENTS
    public static event Action OnPlayerSpotted;
    public static event Action OnPlayerLost;

    public static event Action<Vector3, float> OnNoiseEvent;
    public static event Action<EnemyStateMachine> OnNoiseAlert;

    public static void SpotPlayer()
    {
        OnPlayerSpotted?.Invoke();
    }
    public static void LosePlayer()
    {
        OnPlayerLost?.Invoke();
    }

    public static void PlayerMadeNoise(Vector3 position, float loudness)
    {
        OnNoiseEvent?.Invoke(position, loudness);
    }
    public static void TriggerNoiseAlert(EnemyStateMachine enemySM)
    {
        OnNoiseAlert?.Invoke(enemySM);
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
    
    //KILL
    public static event Action OnEnemyKilledPlayer;
    public static void KillPlayer()
    {
        OnEnemyKilledPlayer?.Invoke();
    }

}

