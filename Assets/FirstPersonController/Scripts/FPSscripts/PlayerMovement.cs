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
    
    private PlayerCameraConroller _playerPlayerCameraController;
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
        
        if(playerCamera.gameObject.GetComponent<PlayerCameraConroller>()) _playerPlayerCameraController = playerCamera.gameObject.GetComponent<PlayerCameraConroller>();
        else _playerPlayerCameraController = playerCamera.gameObject.AddComponent<PlayerCameraConroller>();
        
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            RotateCamera();
            RotatePlayer();
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if(view.IsMine) Move();
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
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
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
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void RotateCamera()
    {
        if(_playerPlayerCameraController) _playerPlayerCameraController.RotateCamera();
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        //Debug.DrawRay(groundCheck.position, Vector3.down * groundCheckRadius, Color.magenta);
    }
}
