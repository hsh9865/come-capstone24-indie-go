using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
/*
public class PlayerController : MonoBehaviour
{
    Vector2 inputDir;
    public bool isJumping;
    public bool canJump;
    private bool isWalking;
    public float moveSpeed = 10;
    public int jumpCount = 2;
    public int jumpCountLeft;
    public float jumpForce = 5f;
    public GroundChecker groundChecker;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    
    private Animator animator;
    void Start()
    {
        jumpCountLeft = jumpCount;
        groundChecker = GetComponentInChildren<GroundChecker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckIfCanJump();
        UpdateAnimations();
        Flip();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public  void OnMovement(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isWalking = true;
        }
        inputDir = context.ReadValue<Vector2>();
        
        if(context.canceled)
        {
            isWalking = false;
        }
    }

    private void Move()
    {
        if(inputDir != Vector2.zero)
        {
            rb.velocity = new Vector2(moveSpeed * inputDir.x, rb.velocity.y);
        }
    }

    private void Flip()
    {
        bool flipSprite = (spriteRenderer.flipX ? (inputDir.x > 0f) : (inputDir.x < 0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Jump();
        }
    }

    private void CheckIfCanJump()
    {
        if (groundChecker.IsGrounded && !isJumping)
        {
            canJump = true;
            jumpCountLeft = jumpCount;
        }
        if(jumpCountLeft <= 0)
        {
            canJump = false;
        }
    }

    private void UpdateAnimations()
    {
        animator.SetFloat ("Jump", rb.velocity.y);
        animator.SetBool("isMoving", isWalking);
        animator.SetBool("isGround", groundChecker.IsGrounded);
    }
    private void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCountLeft--;
        }
    }
}
*/