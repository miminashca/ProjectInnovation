using UnityEngine;

public class AIDirector : MonoBehaviour
{
    public delegate void NoiseEvent(Vector3 position, float loudness);
    public static event NoiseEvent OnNoiseEvent;

    public static void PlayerMadeNoise(Vector3 position, float loudness)
    {
        OnNoiseEvent?.Invoke(position, loudness);
    }
}

