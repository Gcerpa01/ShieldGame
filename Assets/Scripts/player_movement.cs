using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer playerSprite;

    float xDir;
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float jumpDistance = 10f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        xDir = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xDir * movementSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpDistance);
        }
        changeAnimation();
    }

    private void changeAnimation()
    {
        if (xDir > 0)
        {
            anim.SetBool("isRunning", true);
            playerSprite.flipX = false;
        }
        else if (xDir < 0)
        {
            anim.SetBool("isRunning", true);
            playerSprite.flipX = true;
        }

        else anim.SetBool("isRunning", false);
    }
}
