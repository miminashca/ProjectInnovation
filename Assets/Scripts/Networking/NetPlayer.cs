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
        Canvas[] canvases = GetComponentsInChildren<Canvas>();
        if (!view.IsMine)
        {
            if (camera) camera.enabled = false;
            //if (listener) listener.enabled = false;
            if (canvases.Length > 0)
            {
                foreach (Canvas canvas in canvases)
                {
                    canvas.gameObject.SetActive(false);
                }
            }
        }
    }
}
