using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private GroundCheck groundCheck;
    private bool jumping;
    private bool doubleJumpPerformed;
    private bool jumpReleased;

    Vector2 moveDirection = Vector2.zero;

    private GameObject playerVisual;

    [SerializeField] private AudioSource jumpSoundEffect;
    

    private enum MovementState
    {
        idle,
        running,
        jumping,
        falling
    }

    private void Awake()
    {
        playerVisual = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        jumpReleased = true;
        doubleJumpPerformed = false;
    }

    void Update()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        Jump();
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (playerVisual == null)
        {
            return;
        }

        MovementState state;

        if (moveDirection.x > 0f)
        {
            state = MovementState.running;
            playerVisual.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (moveDirection.x < 0f)
        {
            state = MovementState.running;
            playerVisual.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }

        if (animator != null)
        {
            animator.SetInteger("state", (int)state);
        }
    }

    
    private bool IsGrounded()
    {
        return groundCheck.isGrounded;
        //return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size - new Vector3(.1f, 0, 0), 0f, Vector2.down, .1f, jumpableGround);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            doubleJumpPerformed = false;
        }
        if (jumping)
        {
            if (IsGrounded())
            {
                if (IsGravityInverse())
                {
                    rb.velocity = new Vector2(rb.velocity.x, -jumpPower);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                }
                if (!jumpSoundEffect.isPlaying)
                    jumpSoundEffect.Play();
                jumpReleased = false;
            }
            else if (jumpReleased && !doubleJumpPerformed) 
            {
                // Perform Double Jump
                if (IsGravityInverse())
                {
                    return;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                }
                if (!jumpSoundEffect.isPlaying)
                    jumpSoundEffect.Play();
                doubleJumpPerformed = true;
                animator.SetTrigger("doubleJump");
            }
        }
    }

    public void SetJumping(bool jumping)
    {
        this.jumping = jumping;
        if (jumping == false)
        {
            jumpReleased = true;
        }
    }

    public void SetMovementVector(Vector2 movementDirection)
    {
        moveDirection = movementDirection;
        moveDirection.Normalize();
    }

    private bool IsGravityInverse()
    {
        return rb.gravityScale < 0;
    }
}
