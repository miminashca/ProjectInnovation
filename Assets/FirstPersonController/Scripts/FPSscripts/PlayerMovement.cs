using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float moveForce = 5f;
    [SerializeField] private float moveSpeed = 5f;

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
    
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsGrounded()) {
            //Debug.Log("grounded");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.linearVelocity += new Vector3(0, jumpForce, 0);
            }
        }
        
        RotateY();
    }

    private void FixedUpdate()
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

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        //Debug.DrawRay(groundCheck.position, Vector3.down * groundCheckRadius, Color.magenta);
    }

    private void RotateY()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        yRotation += mouseX;
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
