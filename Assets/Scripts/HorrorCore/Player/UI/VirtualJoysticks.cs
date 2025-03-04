using UnityEngine;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour
{
    [SerializeField] private RectTransform joystickBackground; // Background of the joystick
    [SerializeField] private RectTransform joystickHandle; // Handle of the joystick
    [SerializeField] private float maxJoystickDistance = 50f; // Maximum distance the handle can move
    [SerializeField] private float smoothness = 10f; // Smoothness of joystick movement, adjustable in Inspector

    private Vector3 defaultHandlePosition;

    void Start()
    {
        // Save the default position of the joystick handle
        defaultHandlePosition = joystickHandle.localPosition;
    }

    void Update()
    {
        // Get input values
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Create a movement vector based on input
        Vector3 inputVector = new Vector3(horizontal, vertical).normalized;

        // Calculate the new position of the joystick handle
        Vector3 handlePosition = inputVector * maxJoystickDistance;

        // Smoothly move the joystick handle to the new position
        joystickHandle.localPosition = Vector3.Lerp(joystickHandle.localPosition, defaultHandlePosition + handlePosition, Time.deltaTime * smoothness);

        // If no input, reset the handle to the default position
        if (inputVector == Vector3.zero)
        {
            joystickHandle.localPosition = Vector3.Lerp(joystickHandle.localPosition, defaultHandlePosition, Time.deltaTime * smoothness);
        }
    }
}
