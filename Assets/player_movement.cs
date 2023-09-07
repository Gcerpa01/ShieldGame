using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float xDir = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xDir*8.5f,rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x,7);
        }

        if (xDir > 0){
            anim.SetBool("isRunning",true);
        }
        else if (xDir < 0){
            anim.SetBool("isRunning",true);
        }
        
        else anim.SetBool("isRunning",false);

    }
}
