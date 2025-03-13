using System;
using Photon.Pun;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.OnPickupCollected += Collect;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Thief"))
        {
            EventBus.DetectPickup(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Thief"))
        {
            EventBus.UndetectPickup(this);
        }
    }

    private void Collect(PickUp pickUp)
    {
        if(pickUp == this) PhotonNetwork.Destroy(this.gameObject);
    }
}
