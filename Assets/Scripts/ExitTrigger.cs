using System;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
        EventBus.OnMinAmountOfPickupsCollected += EnableTrigger;
    }
    void OnDestroy()
    {
        EventBus.OnMinAmountOfPickupsCollected -= EnableTrigger;
    }

    // Update is called once per frame
    void EnableTrigger()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Thief")) EventBus.FinishGame();
    }
}
