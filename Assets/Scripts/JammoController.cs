using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class JammoController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Field Boundaries")]
    public Transform centerPoint;
    private float maxDistanceX = 13f; // Chiều dài
    private float maxDistanceZ = 10f; // Chiều rộng

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

        if (centerPoint != null)
        {
            // Tính khoảng cách tương đối so với tâm sân trên từng trục
            Vector3 offset = transform.position - centerPoint.position;
            
            bool isOutsideX = Mathf.Abs(offset.x) >= maxDistanceX;
            bool isOutsideZ = Mathf.Abs(offset.z) >= maxDistanceZ;

            if (isOutsideX || isOutsideZ)
            {
                // Nếu chạm biên trục X và đang cố đi xa hơn khỏi tâm theo trục X thì chặn lại
                if (isOutsideX && (offset.x * moveDirection.x > 0))
                {
                    moveDirection.x = 0;
                }
                
                // Nếu chạm biên trục Z và đang cố đi xa hơn khỏi tâm theo trục Z thì chặn lại
                if (isOutsideZ && (offset.z * moveDirection.z > 0))
                {
                    moveDirection.z = 0;
                }

                // Chuẩn hóa lại vector sau khi sửa đổi để tốc độ di chuyển chéo không bị sai
                if (moveDirection != Vector3.zero)
                {
                    moveDirection.Normalize();
                }
            }
        }

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
