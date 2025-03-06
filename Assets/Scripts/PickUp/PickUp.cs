using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnEnable()
    {
        PickupEventBus.OnPickupCollected += Collect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupEventBus.DetectPickup(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickupEventBus.UndetectPickup(this);
        }
    }

    private void Collect(PickUp pickUp)
    {
        if(pickUp == this) Destroy(this.gameObject);
    }
}
