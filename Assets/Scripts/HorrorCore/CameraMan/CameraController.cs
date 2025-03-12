using System;
using System.Collections;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private float yClamp = 30f;

    private bool gyroAvailable = false;
    private float initYAngle;
    private float currentYAngle;

    private void OnEnable()
    {
        EventBus.OnCameraViewChanged += ResetCamera;
    }

    void Start()
    {
        initYAngle = transform.localEulerAngles.y;
        currentYAngle = initYAngle;
        
        // Only enable the gyroscope on real mobile devices
        if (Application.platform == RuntimePlatform.Android)
        {
            if (SystemInfo.supportsGyroscope)
            {
                Input.gyro.enabled = true;
                gyroAvailable = true;
            }
            else
            {
                Debug.LogWarning("Gyroscope not available on this device. Disabling rotation.");
                gyroAvailable = false;
            }
        }
        else
        {
            Debug.LogWarning("Gyroscope is disabled on non-mobile platforms.");
            gyroAvailable = false;
        }
    }

    void FixedUpdate()
    {
        if(gyroAvailable) Rotate();
    }

    private void Rotate()
    {
        float delta = -Input.gyro.rotationRateUnbiased.y * Mathf.Rad2Deg * Time.deltaTime;
        
        currentYAngle = Mathf.Clamp(currentYAngle + delta, initYAngle-yClamp, initYAngle+yClamp);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, currentYAngle, transform.localEulerAngles.z);
    }

    private void ResetCamera()
    {
        initYAngle = transform.localEulerAngles.y;
        currentYAngle = initYAngle;
    }
    
    private void OnDisable()
    {
        EventBus.OnCameraViewChanged -= ResetCamera;
    }
}
