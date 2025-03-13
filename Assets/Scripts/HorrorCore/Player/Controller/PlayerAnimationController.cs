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
        animator.SetBool("IsCrouching", !animator.GetBool("IsCrouching"));
    }
    private void Die()
    {
        animator.SetBool("IsDieing", !animator.GetBool("IsDieing"));
    }
    private void StartMove()
    {
        animator.SetBool("IsMoving", true);
    }
    private void StopMove()
    {
        animator.SetBool("IsMoving", false);
    }
}
