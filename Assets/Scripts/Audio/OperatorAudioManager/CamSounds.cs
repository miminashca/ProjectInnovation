using Photon.Pun;
using UnityEngine;

public class CamSounds : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (!PhotonNetwork.IsMasterClient)
        {
            audioSource.Play();
        }

    }
}
