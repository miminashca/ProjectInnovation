using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    
    [SerializeField] private Joystick joystick;

    private PlayerCameraConroller playerPlayerCameraController;
    private Camera playerCamera;
    private PhotonView view;
    private int? rotationFingerId = null;
    // For each finger, store whether it STARTED on UI or not
    private Dictionary<int, bool> fingerStartedOnUI = new Dictionary<int, bool>();

    private bool crouched = false;
    
    private float groundCheckRadius = 0.5f;
    private Rigidbody playerRigidbody;

    private float yRotation = 0f;
    public float horizontalSensitivity = 500f;
    float moveThreshold = 0.5f;

    public event Action OnPlayerStartMove;
    public event Action OnPlayerStopMove;
    public event Action OnPlayerCrouch;

    private float lastVelocity = 0f;
    bool isMoving = false;
    bool isDead = false;

    private void OnEnable()
    {
        EventBus.OnPlayerCrouch += ChangeSpeed;
        EventBus.OnPlayerDie += Die;
        
    }
    private void OnDisable()
    {
        EventBus.OnPlayerCrouch -= ChangeSpeed;
        EventBus.OnPlayerDie -= Die;
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();

        playerRigidbody.freezeRotation = true;

        if (playerCamera.gameObject.GetComponent<PlayerCameraConroller>()) playerPlayerCameraController = playerCamera.gameObject.GetComponent<PlayerCameraConroller>();
        else playerPlayerCameraController = playerCamera.gameObject.AddComponent<PlayerCameraConroller>();

        view = GetComponent<PhotonView>();

    }

    private void Update()
    {
        if ((!view || view.IsMine) && !isDead)
        {
            HandleTouches();
            RotatePlayer();
        }
    }

    private void FixedUpdate()
    {
        if ((!view || view.IsMine) && !isDead) Move();
    }

    private void Move()
    {
        float currentVelocity = playerRigidbody.linearVelocity.magnitude;

        // Check if the player has started moving.
        if (!isMoving && currentVelocity > moveThreshold)
        {
            isMoving = true;
            OnPlayerStartMove?.Invoke();
        }
        // Check if the player has stopped moving.
        else if (isMoving && currentVelocity <= moveThreshold)
        {
            isMoving = false;
            OnPlayerStopMove?.Invoke();
        }

        
        
        Vector3 moveVector = Vector3.zero;
        if (joystick)
        {
            moveVector += new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        }
        
#if UNITY_EDITOR
        moveVector += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
#endif

        Vector3 newVelocity = playerRigidbody.transform.right * moveVector.x +
                              playerRigidbody.transform.forward * moveVector.z;
        newVelocity *= moveSpeed;
        newVelocity.y = playerRigidbody.linearVelocity.y;
        playerRigidbody.linearVelocity = newVelocity;
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

#if UNITY_EDITOR
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity * Time.deltaTime;
        yRotation += mouseX;
#endif
        
        yRotation += touchVec.x;

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);

        if (playerPlayerCameraController)
            playerPlayerCameraController.RotateCamera(touchVec.y);
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

    void ChangeSpeed()
    {
        OnPlayerCrouch?.Invoke();
        
        if (!crouched)
        {
            moveSpeed *= .5f;
            crouched = true;
        }
        else
        {
            moveSpeed *= 2f;
            crouched = false;
        }
    }

    void Die()
    { 
        isDead = true;
    }
}