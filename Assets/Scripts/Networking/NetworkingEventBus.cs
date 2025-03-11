using System;
using UnityEngine;

public static class NetworkingEventBus
{
    public static event Action<Transform> OnThiefSpawned;
    public static void SpawnThief(Transform thiefTransform)
    {
        OnThiefSpawned?.Invoke(thiefTransform);
        //Debug.Log("Spawn thief");
    }
    
}
