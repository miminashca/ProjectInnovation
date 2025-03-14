using UnityEngine;

public class PlayerCameraConroller : MonoBehaviour
{
    [SerializeField] private float topClamp = -90f;
    [SerializeField] private float bottomClamp = 90f;
    [SerializeField] public float verticalSensitivity = 500f;
    private bool crouched = false;
    private Vector3 camInitialPos;
    
    private float xRotation = 0f;
    void Start()
    {
        camInitialPos = gameObject.transform.localPosition;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void RotateCamera(float touchY)
    {
        // 🎮 PC: Still support mouse input
        //float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity * Time.deltaTime;
        
        //xRotation -= (mouseY + touchY);
        xRotation -= touchY;
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    public void Crouch()
    {
        if (!crouched)
        {
            gameObject.transform.localPosition =
                new Vector3(gameObject.transform.localPosition.x, 0f, gameObject.transform.localPosition.z);
            crouched = true;
        }
        else
        {
            gameObject.transform.localPosition = camInitialPos;
            crouched = false;
        }
        EventBus.Crouch();
    }
        
}