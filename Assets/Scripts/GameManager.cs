using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int minAmountOfPickups;
    public int currentAmountOfPickups = 0;
    public static GameManager Instance { get; private set; }
    
    public Transform enemySpawnTransform;
    public GameObject enemyPrefab;
    private GameObject enemy;

    private void Awake()
    {
        EventBus.OnPickupCollected += UpdateAmountOfPickups;
        EventBus.OnGameFinished += LoadWinGameScene;
        EventBus.OnGameLost += LoadLoseGameScene;
    }

    private void OnDestroy()
    {
        EventBus.OnPickupCollected -= UpdateAmountOfPickups;
        EventBus.OnGameFinished -= LoadWinGameScene;
        EventBus.OnGameLost -= LoadLoseGameScene;
    }

    void Start()
    {
        if(Instance != null) Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void UpdateAmountOfPickups(PickUp pickUp)
    {
        if (!enemy) enemy = Instantiate(enemyPrefab, enemySpawnTransform.position, Quaternion.identity);
        
        currentAmountOfPickups++;
        Debug.Log(currentAmountOfPickups);
        if(currentAmountOfPickups==minAmountOfPickups) EventBus.MinPickupsCollected();
    }

    void LoadWinGameScene()
    {
        PhotonNetwork.LoadLevel("Win");
    }
    void LoadLoseGameScene()
    {
        PhotonNetwork.LoadLevel("Win");
    }
}
