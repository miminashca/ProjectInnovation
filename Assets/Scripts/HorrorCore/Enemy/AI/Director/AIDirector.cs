using System;
using UnityEngine;

public class AIDirector : MonoBehaviour
{
    // listens to sensors events, and passes the result to state machine.
    public static event Action<Vector3, float> OnNoiseEvent;
    public static void PlayerMadeNoise(Vector3 position, float loudness)
    {
        OnNoiseEvent?.Invoke(position, loudness);
    }
}

