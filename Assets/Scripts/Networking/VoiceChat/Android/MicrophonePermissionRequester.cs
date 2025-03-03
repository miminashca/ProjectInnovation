using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

public class MicrophonePermissionRequester : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID
        // Check if the user already granted microphone permission
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // If not, request it
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif
    }
}
