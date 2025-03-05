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

    public void RotateCamera(float touchY)
    {
        // 🎮 PC: Still support mouse input
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;
        
        xRotation -= (mouseY + touchY);
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
        
}
