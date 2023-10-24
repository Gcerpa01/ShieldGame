using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;

    private float xDir;
    private bool faceDir = true;
    private bool hasJumped = false;
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask jumpGround;

    private enum MovementState { Idle, Running, Jumping, Falling };
    private MovementState currentState;

    private ShieldManager shieldManager; // Reference to the ShieldManager script

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        shieldManager = FindObjectOfType<ShieldManager>(); // Find the ShieldManager in the scene
    }

    private void Update()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump")) hasJumped = true;
    }

    private void FixedUpdate()
    {
        Move();
        changeAnimation();
    }

    private void Move()
    {
        if(shieldManager.GetBlockingState()) rb.velocity = new Vector2(xDir * movementSpeed/2, rb.velocity.y);
        else rb.velocity = new Vector2(xDir * movementSpeed, rb.velocity.y);

        if (hasJumped && isGrounded() && shieldManager.GetShieldState() != ShieldManager.MovementState.thrown)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            hasJumped = false;
        }    
    }

    private void changeAnimation()
    {
        MovementState newState;

        if (xDir > 0)
        {
            newState = MovementState.Running;
            if (!faceDir) Flip();
        }
        else if (xDir < 0)
        {
            newState = MovementState.Running;
            if (faceDir) Flip();
        }
        else
        {
            newState = MovementState.Idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            newState = MovementState.Jumping;
        }
        else if (rb.velocity.y < -0.1f)
        {
            newState = MovementState.Falling;
        }

        if (newState != currentState)
        {
            anim.SetInteger("actionState", (int)newState);
            currentState = newState;
        }
    }

    private void Flip()
    {
        faceDir = !faceDir;
        transform.Rotate(0, 180, 0);
    }

    public bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpGround);
    }


}
