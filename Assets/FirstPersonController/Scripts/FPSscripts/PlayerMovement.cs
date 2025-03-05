using System;
using System.Collections;
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
        
        if(playerCamera.gameObject.GetComponent<PlayerCameraConroller>()) playerPlayerCameraController = playerCamera.gameObject.GetComponent<PlayerCameraConroller>();
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
        if(!view || view.IsMine) Move();
    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }

    private void Jump()
    {
        if (IsGrounded()) {
            //Debug.Log("grounded");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.linearVelocity += new Vector3(0, jumpForce, 0);
            }
        }
    }
    
    private void Move()
    {
        Vector3 moveVector = new Vector3();
        if (joystick)
        {
            moveVector += new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }
        moveVector += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        control = IsGrounded() ? ControlType.Velocity : ControlType.Force;
        
        switch (control) {
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
        Vector2 touchVec = new Vector2(0f,0f);

        // 🖐️ Mobile: Use touch drag instead of mouse
        if (Input.touchCount > 0)
        {
            // 2) Check if we have a rotation finger
            if (rotationFingerId.HasValue)
            {
                int rotFinger = rotationFingerId.Value;

                // Find that finger in current touches
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch t = Input.GetTouch(i);
                    if (t.fingerId == rotFinger && t.phase == TouchPhase.Moved)
                    {
                        // Accumulate rotation from its delta
                        touchVec = horizontalSensitivity * Time.deltaTime * t.deltaPosition;
                        break;
                    }
                }
            }
        }

        // 🎮 PC: Still support mouse input
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        
        yRotation += (mouseX + touchVec.x);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        
        if(playerPlayerCameraController) playerPlayerCameraController.RotateCamera(touchVec.y);
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
                if (!rotationFingerId.HasValue && !isOverUI)
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
