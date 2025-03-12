using UnityEngine;
public class CameraButtons : MonoBehaviour
{
    public void PressCameraButtonWithID(int ID)
    {
        EventBus.PressCameraSwitchButton(ID);
    }
}
