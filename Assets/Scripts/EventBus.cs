using System;
using UnityEngine;

public static class EventBus
{
    public static event Action<int> OnCameraSwitchButtonPressedWithID;
    public static event Action OnCameraViewChanged;
    
    public static event Action<PickUp> OnPickupDetected;
    public static event Action<PickUp> OnPickupUndetected;
    public static event Action<PickUp> OnPickupCollected;
    
    public static void PressCameraSwitchButton(int ID)
    {
        OnCameraSwitchButtonPressedWithID?.Invoke(ID);
    }
    public static void ChangeCameraView()
    {
        OnCameraViewChanged?.Invoke();
    }
    
    public static void DetectPickup(PickUp pickUp)
    {
        OnPickupDetected?.Invoke(pickUp);
    }
    public static void UndetectPickup(PickUp pickUp)
    {
        OnPickupUndetected?.Invoke(pickUp);
    }
    public static void CollectPickup(PickUp pickUp)
    {
        OnPickupCollected?.Invoke(pickUp);
    }
}
