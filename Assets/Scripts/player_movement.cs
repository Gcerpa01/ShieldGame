using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    // private SpriteRenderer playerSprite;
    private BoxCollider2D coll;

    float xDir;
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float jumpDistance = 10f;

    [SerializeField] private LayerMask jumpGround;

    private enum MovementState {idle,running,jumping,falling};

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // playerSprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        xDir = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xDir * movementSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpDistance);
        }
        changeAnimation();
    }

    private void changeAnimation()
    {
        MovementState state;

        if (xDir > 0)
        {
            state = MovementState.running;
            transform.localScale = new Vector3(1,1,1);
        }
        else if (xDir < 0)
        {
            state = MovementState.running;
            transform.localScale = new Vector3(-1,1,1);
        }

        else state = MovementState.idle;


        if(rb.velocity.y > .1f){
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f) state = MovementState.falling;

        anim.SetInteger("actionState",(int)state);
    }

    private bool isGrounded(){
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down,.1f,jumpGround);
    }
}
