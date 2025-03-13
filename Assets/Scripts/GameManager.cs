using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int minAmountOfPickups;
    public int currentAmountOfPickups = 0;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        EventBus.OnPickupCollected += UpdateAmountOfPickups;
    }

    private void OnDestroy()
    {
        EventBus.OnPickupCollected -= UpdateAmountOfPickups;
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
}
