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

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // 1) Spawn the main player
            GameObject playerInstance = PhotonNetwork.Instantiate(
                playerPrefab.name,
                playerSpawnTransform.position,
                playerSpawnTransform.rotation
            );

            // 2) Hook up chase music references on the MasterClient side
            if (chaseMusicController != null)
            {
                chaseMusicController.SetTargets(playerInstance, enemyInScene);
            }
            else
            {
                Debug.LogWarning("ChaseMusicController reference is missing!");
            }
        }
        else
        {
            // If not the MasterClient, spawn the second player (the camera man)
            PhotonNetwork.Instantiate(
                secondPlayerUIPrefab.name,
                Vector3.zero,
                Quaternion.identity
            );
        }
    }
}
