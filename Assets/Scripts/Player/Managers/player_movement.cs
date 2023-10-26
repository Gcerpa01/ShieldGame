using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;

    private float xDir;
    private float yDir;
    private bool faceDir = true;
    private bool hasJumped = false;
    private bool GodMode = false;
    
    [Header("Player Stats")]
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Components")]
    [SerializeField] private LayerMask jumpGround;

    private enum MovementState { Idle, Running, Jumping, Falling };
    private MovementState currentState;

    public ShieldManager shieldManager; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        shieldManager = FindObjectOfType<ShieldManager>(); 
    }

    private void Update()
    {
        xDir = Input.GetAxisRaw("Horizontal");
        yDir = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump")) hasJumped = true;
        if (Input.GetButtonDown("Fire3")) GodMode = !GodMode;
    }

    private void FixedUpdate()
    {
        if (!GodMode) Move();
        else GodMove();

        changeAnimation();
    }

    private void Move()
    {
        if (shieldManager.GetBlockingState()) rb.velocity = new Vector2(xDir * movementSpeed / 2, rb.velocity.y);
        else rb.velocity = new Vector2(xDir * movementSpeed, rb.velocity.y);

        if (hasJumped && isGrounded() && shieldManager.GetShieldState() != ShieldManager.MovementState.thrown)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            hasJumped = false;
        }
    }

    private void GodMove()
    {
        float xMove = xDir * movementSpeed;
        float yMove = yDir * movementSpeed;
        rb.velocity = new Vector2(xMove, yMove);
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

    public bool isGodMode(){
        return GodMode;
    }

}
