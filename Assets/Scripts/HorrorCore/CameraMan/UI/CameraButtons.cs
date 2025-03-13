using UnityEngine;
public class CameraButtons : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PressCameraButtonWithID(int ID)
    {
        EventBus.PressCameraSwitchButton(ID);

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
