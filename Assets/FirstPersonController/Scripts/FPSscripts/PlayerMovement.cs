using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float moveForce = 5f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Joystick joystick;

    private PlayerCameraConroller playerPlayerCameraController;
    private Camera playerCamera;
    private PhotonView view;
    private int? rotationFingerId = null;
    // For each finger, store whether it STARTED on UI or not
    private Dictionary<int, bool> fingerStartedOnUI = new Dictionary<int, bool>();


    private enum ControlType
    {
        Force,
        Velocity
    }
    private ControlType control;
    private float groundCheckRadius = 0.5f;
    private Rigidbody playerRigidbody;

    private float yRotation = 0f;
    public float horizontalSensitivity = 500f;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        if (playerCamera.gameObject.GetComponent<PlayerCameraConroller>()) playerPlayerCameraController = playerCamera.gameObject.GetComponent<PlayerCameraConroller>();
        else playerPlayerCameraController = playerCamera.gameObject.AddComponent<PlayerCameraConroller>();

        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!view || view.IsMine)
        {
            HandleTouches();
            RotatePlayer();
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (!view || view.IsMine) Move();
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            //Debug.Log("grounded");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.linearVelocity += new Vector3(0, jumpForce, 0);
            }
        }
    }

    private void Move()
    {
        Vector3 moveVector = Vector3.zero;
        if (joystick)
        {
            moveVector = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }
        // Optionally, you can add a preprocessor check if you want to support keyboard input on other platforms.
#if UNITY_EDITOR
        else
        {
            moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
#endif

        control = IsGrounded() ? ControlType.Velocity : ControlType.Force;

        switch (control)
        {
            case ControlType.Force:
                playerRigidbody.AddRelativeForce(moveVector * moveForce);
                break;
            case ControlType.Velocity:
                Vector3 newVelocity = playerRigidbody.transform.right * moveVector.x +
                                      playerRigidbody.transform.forward * moveVector.z;
                newVelocity *= moveSpeed;
                newVelocity.y = playerRigidbody.linearVelocity.y;
                playerRigidbody.linearVelocity = newVelocity;
                break;
        }
    }

    private void RotatePlayer()
    {
        Vector2 touchVec = Vector2.zero;

        if (Input.touchCount > 0 && rotationFingerId.HasValue)
        {
            foreach (Touch t in Input.touches)
            {
                if (t.fingerId == rotationFingerId.Value && t.phase == TouchPhase.Moved)
                {
                    touchVec = horizontalSensitivity * Time.deltaTime * t.deltaPosition;
                    break;
                }
            }
        }

        // For mobile, you may want to disable the mouse input
#if UNITY_EDITOR
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
#else
    float mouseX = 0;
#endif

        yRotation += (mouseX + touchVec.x);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        if (playerPlayerCameraController)
            playerPlayerCameraController.RotateCamera(touchVec.y);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        //Debug.DrawRay(groundCheck.position, Vector3.down * groundCheckRadius, Color.magenta);
    }
    /// <summary>
    /// Returns true if the specified touch is over any UI element, false otherwise.
    /// </summary>
    private bool IsTouchOverUI(Touch touch)
    {
        // Make sure we have a current EventSystem and use fingerId
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId);
    }
    private void HandleTouches()
    {
        // Check all touches this frame
        foreach (Touch touch in Input.touches)
        {
            // 1) If it's a new touch (Began), record whether it started on UI
            if (touch.phase == TouchPhase.Began)
            {
                bool isOverUI = IsTouchOverUI(touch);
                fingerStartedOnUI[touch.fingerId] = isOverUI;

                // If no rotation finger yet, and it started OFF UI, pick it for rotation
                if (!rotationFingerId.HasValue && !isOverUI && touch.position.x > Screen.width / 2)
                {
                    rotationFingerId = touch.fingerId;
                }
            }

            // 2) If it's ended/canceled, remove from dictionary
            //    If this was our rotation finger, free it up
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (fingerStartedOnUI.ContainsKey(touch.fingerId))
                    fingerStartedOnUI.Remove(touch.fingerId);

                if (rotationFingerId.HasValue && rotationFingerId.Value == touch.fingerId)
                {
                    rotationFingerId = null;
                }
            }
        }
    }
}
