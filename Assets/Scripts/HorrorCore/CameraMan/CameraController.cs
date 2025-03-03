using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private float yClamp = 30f;

    private bool gyroAvailable;
    private float initYAngle;
    private float currentYAngle;
    
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            gyroAvailable = true;

            initYAngle = transform.localEulerAngles.y;
            currentYAngle = initYAngle;
        }
        else
        {
            Debug.LogWarning("Gyroscope not available on this device. Disabling rotation.");
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
}
