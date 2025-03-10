using UnityEngine;

public enum AudioPlaybackTarget
{
    Everyone,         // Play for everyone
    MainPlayerOnly,   // Only for the main player
    CameraManOnly     // Only for the camera man
}

[RequireComponent(typeof(AudioSource))]
public class RoleBasedAudioFilter : MonoBehaviour
{
    [Tooltip("Which role(s) should hear this AudioSource?")]
    public AudioPlaybackTarget playbackTarget = AudioPlaybackTarget.Everyone;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Call this method from Start and whenever the local role might change.
    public void ApplyFilter()
    {
        PlayerRole localRole = LocalRoleManager.LocalRole;

        switch (playbackTarget)
        {
            case AudioPlaybackTarget.Everyone:
                audioSource.mute = false;
                break;

            case AudioPlaybackTarget.MainPlayerOnly:
                audioSource.mute = (localRole != PlayerRole.MainPlayer);
                break;

            case AudioPlaybackTarget.CameraManOnly:
                audioSource.mute = (localRole != PlayerRole.CameraMan);
                break;
        }
    }

    void Start()
    {
        ApplyFilter();
    }
}
