using System;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform playerSpawnTransform;
    [SerializeField] private Transform cameraSpawnTransform;
    [SerializeField] private string playerPrefabName = "Player_Networked";
    [SerializeField] private string secondPlayerUIPrefabName = "CameraUI"; 

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Spawn Player
            PhotonNetwork.Instantiate(playerPrefabName, playerSpawnTransform.position, playerSpawnTransform.rotation);
        }
        else
        {
            // Spawn Camera
            PhotonNetwork.Instantiate(secondPlayerUIPrefabName, cameraSpawnTransform.position, cameraSpawnTransform.rotation);
        }
    }
}
