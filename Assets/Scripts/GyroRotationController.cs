using Unity.Mathematics;
using UnityEngine;

public class GyroRotationController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;
    private Vector3 rotation; 
    void Start()
    {
        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            
            return true;
        }
        return false;
    }
    void Update()
    {
        if (gyroEnabled)
        {
            rotation.y = - Input.gyro.rotationRateUnbiased.y;
            transform.Rotate(rotation);
        }
    }
}
