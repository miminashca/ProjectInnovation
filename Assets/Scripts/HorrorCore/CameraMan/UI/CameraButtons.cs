using UnityEngine;
public class CameraButtons : MonoBehaviour
{
    public void PressCameraButtonWithID(int ID)
    {
        PickupEventBus.PressCameraSwitchButton(ID);
    }
}
