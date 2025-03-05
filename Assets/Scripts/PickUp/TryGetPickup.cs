using System;
using UnityEngine;

public class TryGetPickup : MonoBehaviour
{
    private Camera cam;
    private PickUp currentPickup;
    private void OnEnable()
    {
        EventBus.OnPickupDetected += PickupDetected;
        EventBus.OnPickupUndetected += PickupUndetected;
    }
    private void OnDisable()
    {
        EventBus.OnPickupDetected -= PickupDetected;
        EventBus.OnPickupUndetected -= PickupUndetected;
    }

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        if (!currentPickup || !cam) return;
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
        
            // We only care about the moment a finger first touches the screen
            if (touch.phase == TouchPhase.Began)
            {
                // Convert touch position on screen into a ray from the main camera
                Ray ray = cam.ScreenPointToRay(touch.position);

                // Perform a 3D physics raycast
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    // Here, we assume the pickup object has a "Pickup" script
                    // or some component that identifies it
                    PickUp pickup = hitInfo.collider.GetComponent<PickUp>();

                    if (pickup == currentPickup)
                    {
                        EventBus.CollectPickup(currentPickup); // Your custom pickup handling
                        currentPickup = null;
                        Debug.Log("picked up");
                    }
                }
            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) // 0 = left mouse button or first touch
        {
            // ScreenPointToRay: from the main camera, create a ray from the tap/click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit is where the ray hits
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                // If the collider we hit has a "Pickup" script (or tag named "Pickup"):
                // (Here, we assume you have a "Pickup" script or a Tag to identify pickups)
                PickUp pickup = hitInfo.collider.GetComponent<PickUp>();
                if (pickup == currentPickup)
                {
                    EventBus.CollectPickup(currentPickup); // Your custom pickup handling
                    currentPickup = null;
                    Debug.Log("picked up");
                }
            }
        }
#endif
        
    }

    void PickupDetected(PickUp pickUp)
    {
        currentPickup = pickUp;
        Debug.Log("pickup det");
    }
    void PickupUndetected(PickUp pickUp)
    {
        currentPickup = null;
        Debug.Log("pickup undet");
    }
}
