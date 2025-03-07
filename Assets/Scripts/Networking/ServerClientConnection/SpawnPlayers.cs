using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject secondPlayerUIPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform playerSpawnTransform;
    [SerializeField] private Transform enemySpawnTransform;

    // If you placed your AudioManager in the same scene,
    // you can drag it here from the hierarchy.
    [Header("Audio Manager (Scene Object)")]
    [SerializeField] private ChaseMusicController chaseMusicController;

    private void Start()
    {
        // The MasterClient spawns the monster and the local player
        if (PhotonNetwork.IsMasterClient)
        {
            // 1) Spawn the monster
            GameObject enemyInstance = PhotonNetwork.Instantiate(
                enemyPrefab.name,
                enemySpawnTransform.position,
                enemySpawnTransform.rotation
            );

            // 2) Spawn the local player
            GameObject playerInstance = PhotonNetwork.Instantiate(
                playerPrefab.name,
                playerSpawnTransform.position,
                playerSpawnTransform.rotation
            );

            // 3) Wire them up to the chase music
            if (chaseMusicController != null)
            {
                chaseMusicController.SetTargets(playerInstance, enemyInstance);
            }
            else
            {
                Debug.LogWarning("ChaseMusicController reference is missing!");
            }
        }
        else
        {
            // If not the MasterClient,
            // just spawn whatever the second player needs:
            PhotonNetwork.Instantiate(
                secondPlayerUIPrefab.name,
                Vector3.zero,
                Quaternion.identity
            );
        }
    }
}
