using System;
using UnityEngine;

public static class EventBus
{
    public static event Action<int> OnCameraSwitchButtonPressedWithID;
    public static event Action OnCameraViewChanged;
    
    public static void PressCameraSwitchButton(int ID)
    {
        OnCameraSwitchButtonPressedWithID?.Invoke(ID);
    }
    public static void ChangeCameraView()
    {
        OnCameraViewChanged?.Invoke();
    }
}
