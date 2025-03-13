using Photon.Pun;
using UnityEngine;

public class LeaveRoom : MonoBehaviour
{
    void Start()
    {
        PhotonNetwork.LeaveRoom();

        PhotonNetwork.LeaveLobby();
    }
}
