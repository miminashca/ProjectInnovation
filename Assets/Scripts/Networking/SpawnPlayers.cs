using System;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPrefab;
    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = transform.position;
        PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPosition, quaternion.identity);
    }
}
