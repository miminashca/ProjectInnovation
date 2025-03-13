using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int minAmountOfPickups;
    public int currentAmountOfPickups = 0;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        EventBus.OnPickupCollected += UpdateAmountOfPickups;
        EventBus.OnGameFinished += LoadWinGameScene;
    }

    private void OnDestroy()
    {
        EventBus.OnPickupCollected -= UpdateAmountOfPickups;
        EventBus.OnGameFinished -= LoadWinGameScene;
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
        currentAmountOfPickups++;
        Debug.Log(currentAmountOfPickups);
        if(currentAmountOfPickups==minAmountOfPickups) EventBus.MinPickupsCollected();
    }

    void LoadWinGameScene()
    {
        PhotonNetwork.LoadLevel("Win");
    }
}
