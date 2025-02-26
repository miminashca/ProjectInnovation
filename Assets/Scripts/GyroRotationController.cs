using UnityEngine;

public class GyroRotationController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Vector3 rotation;
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotation.y = - Input.gyro.rotationRateUnbiased.y;
        transform.Rotate(rotation);
    }
}
