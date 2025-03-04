using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
            RotateCamera();
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
        Vector3 moveVector;
        if (Application.platform == RuntimePlatform.Android && joystick)
        {
            moveVector = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }
        else
        {
            moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
        
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
        float touchX = 0f;

        // 🖐️ Mobile: Use touch drag instead of mouse
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                touchX = touch.deltaPosition.x * horizontalSensitivity * Time.deltaTime;
            }
        }

        // 🎮 PC: Still support mouse input
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        
        yRotation += (mouseX + touchX);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void RotateCamera()
    {
        if(playerPlayerCameraController) playerPlayerCameraController.RotateCamera();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        //Debug.DrawRay(groundCheck.position, Vector3.down * groundCheckRadius, Color.magenta);
    }
}
