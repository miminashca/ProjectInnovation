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
        if (PhotonNetwork.IsMasterClient)
        {
            // 1) Spawn the main player (the thief)
            playerInstance = PhotonNetwork.Instantiate(
                playerPrefab.name,
                playerSpawnTransform.position,
                playerSpawnTransform.rotation
            );

            // Ensure the player has been instantiated and networked
            PhotonView photonView = playerInstance.GetComponent<PhotonView>();
            Debug.Log("playeInstance" + playerInstance);
            if (photonView != null && photonView.IsMine)
            {
                // 2) Delay calling SetReferences until the player is instantiated
                if (chaseMusicController != null && playerInstance != null && enemyInScene != null)
                {
                    chaseMusicController.SetReferences(playerInstance, enemyInScene);
                    Debug.Log("SpawnPlayers: SetReferences called after instantiation.");
                }
                else
                {
                    Debug.LogWarning("SpawnPlayers: chaseMusicController or playerInstance or enemyInScene is null.");
                }
            }
            else
            {
                Debug.LogWarning("SpawnPlayers: PhotonView is null or this is not the local player.");
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
