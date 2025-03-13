using System;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        EventBus.OnMinAmountOfPickupsCollected += EnableTrigger;
    }
    void OnDisable()
    {
        EventBus.OnMinAmountOfPickupsCollected -= EnableTrigger;
    }

    // Update is called once per frame
    void EnableTrigger()
    {
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Thief")) EventBus.FinishGame();
    }
}
