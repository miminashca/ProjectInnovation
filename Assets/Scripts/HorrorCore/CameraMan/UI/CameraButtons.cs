using UnityEngine;

public class CameraButtons : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        // Get the AudioSource component attached to this GameObject.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("CameraButtons: No AudioSource component found on this GameObject.");
        }
    }

    public void PressCameraButtonWithID(int ID)
    {
        PickupEventBus.PressCameraSwitchButton(ID);

        // Play the audio if the AudioSource is available.
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
