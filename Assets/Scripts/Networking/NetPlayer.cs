using UnityEngine;
using Photon.Pun;
public class NetPlayer : MonoBehaviour
{
    private PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();
        // If this Player object is not owned by me, disable its camera & listener
        Camera camera = GetComponentInChildren<Camera>();
        AudioListener listener = GetComponentInChildren<AudioListener>();
        if (!view.IsMine)
        {
            if (camera) camera.enabled = false;
            if (listener) listener.enabled = false;
        }
    }
}
