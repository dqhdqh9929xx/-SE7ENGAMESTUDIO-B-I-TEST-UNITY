using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class JammoController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    private CharacterController characterController;
    private Animator animator;
    private bool wasMoving = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
        bool isMoving = moveDirection.magnitude >= 0.1f;
        if (isMoving)
        {
            transform.forward = moveDirection;
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
        if (isMoving && !wasMoving)
        {
            animator.Play("Run");
        }
        else if (!isMoving && wasMoving)
        {
            animator.Play("Idle");
        }
        wasMoving = isMoving;
    }
}
