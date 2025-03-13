using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.OnPlayerCrouch += Crouch;
        playerMovement.OnPlayerDie += Die;
        playerMovement.OnPlayerStartMove += StartMove;
        playerMovement.OnPlayerStopMove += StopMove;
    }

    private void OnDestroy()
    {
        playerMovement.OnPlayerCrouch -= Crouch;
        playerMovement.OnPlayerDie -= Die;
        playerMovement.OnPlayerStartMove -= StartMove;
        playerMovement.OnPlayerStopMove -= StopMove;
    }

    private void Crouch()
    {
        Debug.Log("crouch");
        animator.SetBool("IsCrouching", !animator.GetBool("IsCrouching"));
    }
    private void Die()
    {
        Debug.Log("die");
        animator.SetBool("IsDieing", !animator.GetBool("IsDieing"));
    }
    private void StartMove()
    {
        Debug.Log("start move");
        animator.SetBool("IsMoving", true);
    }
    private void StopMove()
    {
        Debug.Log("stop move");
        animator.SetBool("IsMoving", false);
    }
}
