using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        PhotonNetwork.LogLevel = PunLogLevel.Full; // More detailed logs
    }
    void Start()
    {
        Debug.Log("Connecting...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Joining Lobby...");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("Loading Lobby Scene...");
        SceneManager.LoadScene("LobbyScene");
    }
}
