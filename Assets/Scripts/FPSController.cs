using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float mouseSensitivity = 2f; // Mouse sensitivity

    public Transform cameraTransform; // Reference to the player's camera
    private CharacterController controller;

    private float xRotation = 0f; // Keeps track of vertical rotation (up/down)

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D movement
        float moveZ = Input.GetAxis("Vertical");   // W/S movement

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void RotatePlayer()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player body left/right (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera up/down (pitch) and clamp to prevent flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}