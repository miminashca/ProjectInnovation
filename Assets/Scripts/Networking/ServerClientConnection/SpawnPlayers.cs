using System;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject secondPlayerUIPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform playerSpawnTransform;

    [Header("Scene References")]
    [Tooltip("Drag the pre-placed Enemy from the scene here.")]
    [SerializeField] private GameObject enemyInScene;
    [Tooltip("Drag the AudioManager with ChaseMusicController here.")]
    [SerializeField] private ChaseMusicController chaseMusicController;

    private GameObject playerInstance;
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("SpawnPlayers: Starting. IsMasterClient = " + PhotonNetwork.IsMasterClient);

            // 1) Spawn the main player (the thief)
            playerInstance = PhotonNetwork.Instantiate(
                playerPrefab.name,
                playerSpawnTransform.position,
                playerSpawnTransform.rotation
            );

            chaseMusicController = playerInstance.GetComponentInChildren<ChaseMusicController>();

            // Instead of checking for PhotonView.IsMine, call SetReferences immediately.
            if (chaseMusicController != null && playerInstance != null && enemyInScene != null)
            {
                Debug.Log("SpawnPlayers: About to call SetReferences on ChaseMusicController.");
                chaseMusicController.SetReferences(playerInstance, enemyInScene);
                //Debug.Log("SpawnPlayers: SetReferences called after instantiation.");
            }
            else
            {
                Debug.LogWarning("SpawnPlayers: chaseMusicController or playerInstance or enemyInScene is null.");
            }
        }
        else
        {
            // If not the MasterClient, spawn the second player (the cameraman)
            PhotonNetwork.Instantiate(
                secondPlayerUIPrefab.name,
                Vector3.zero,
                Quaternion.identity
            );
        }
    }
}