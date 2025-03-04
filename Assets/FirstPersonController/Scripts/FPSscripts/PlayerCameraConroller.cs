using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerCameraConroller : MonoBehaviour
{
    [SerializeField] private float topClamp = -90f;
    [SerializeField] private float bottomClamp = 90f;
    [SerializeField] private float verticalSensitivity = 500f;
    
    private float xRotation = 0f;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void RotateCamera()
    {
        float touchY = 0f;

        // 🖐️ Mobile: Use touch drag instead of mouse
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                // If this touch is over a UI element (e.g. joystick), skip rotation
                if (IsTouchOverUI(touch)) 
                    continue;
            
                // Otherwise, accumulate rotation from this swipe
                if (touch.phase == TouchPhase.Moved)
                {
                    touchY = touch.deltaPosition.y * verticalSensitivity * Time.deltaTime;
                }
            }
            // Touch touch = Input.GetTouch(0);
            // if (touch.phase == TouchPhase.Moved && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            // {
            //     touchY = touch.deltaPosition.y * verticalSensitivity * Time.deltaTime;
            // }
        }

        // 🎮 PC: Still support mouse input
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;
        
        xRotation -= (mouseY + touchY);
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    private bool IsTouchOverUI(Touch touch)
    {
        // Make sure we have a current EventSystem and use fingerId
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }
}
