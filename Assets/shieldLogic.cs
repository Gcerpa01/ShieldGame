using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldLogic : MonoBehaviour
{
    public float speed = 13.8f;
    public Rigidbody2D rb;
    private enum MovementState {thrown, stop, release};
    MovementState shieldState = MovementState.thrown;

    void Start()
    {
        // playerMovement = GetComponent<player_movement>();
        
        // rb.velocity = playerMovement.IsFacingRight? transform.right * speed : transform.right * speed * -1;
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch (shieldState)
            {
                case MovementState.thrown:
                    rb.velocity = Vector2.zero;
                    shieldState = MovementState.stop;
                    break;
                case MovementState.stop:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
