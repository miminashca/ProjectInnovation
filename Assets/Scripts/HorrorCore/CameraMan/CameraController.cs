using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private float yClamp = 30f;
    
    private float currentYAngle;
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        float delta = -Input.gyro.rotationRateUnbiased.y * Mathf.Rad2Deg * Time.deltaTime;
        currentYAngle += delta;
        currentYAngle = Mathf.Clamp(currentYAngle, -yClamp, yClamp);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, currentYAngle, transform.localEulerAngles.z);
    }
}
