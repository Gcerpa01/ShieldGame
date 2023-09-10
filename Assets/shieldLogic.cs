using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldLogic : MonoBehaviour
{
    public float speed = 13.8f;
    public Rigidbody2D rb;
    private enum MovementState {thrown, stop, release};
    MovementState shieldState = MovementState.thrown;
    private player_movement playerMovement;
    void Start()
    {
        // playerMovement = FindObjectOfType<player_movement>();
        
        // Vector2 launchDirection = playerMovement.IsFacingRight() ? Vector2.right : Vector2.left;
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
