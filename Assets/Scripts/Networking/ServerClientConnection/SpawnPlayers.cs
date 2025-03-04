using System;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform playerSpawnTransform;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject secondPlayerUIPrefab;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Spawn Player
            PhotonNetwork.Instantiate(playerPrefab.name, playerSpawnTransform.position, playerSpawnTransform.rotation);
        }
        else
        {
            // Spawn Camera
            PhotonNetwork.Instantiate(secondPlayerUIPrefab.name, Vector3.zero, Quaternion.identity);
        }
    }
}
