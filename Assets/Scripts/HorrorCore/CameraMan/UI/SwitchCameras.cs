using System;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameras : MonoBehaviour
{
    [SerializeField] private CameraTransformsData cameraTransformsData;
    private void OnEnable()
    {
        PickupEventBus.OnCameraSwitchButtonPressedWithID += SetCameraTransformByID;
    }
    void Start()
    {
        SetCameraTransformByID(0);
    }
    private void SetCameraTransformByID(int ID)
    {
        transform.position = GetCamTransformByID(ID).position;
        transform.rotation = GetCamTransformByID(ID).rotation;
        PickupEventBus.ChangeCameraView();
    }
    private Transform GetCamTransformByID(int ID)
    {
        if (cameraTransformsData.cameraTransforms[ID]) return cameraTransformsData.cameraTransforms[ID];
        Debug.Log($"No target camera transform at ID: {ID}.");
        return transform;
    }
    private void OnDisable()
    {
        PickupEventBus.OnCameraSwitchButtonPressedWithID -= SetCameraTransformByID;
    }
}
