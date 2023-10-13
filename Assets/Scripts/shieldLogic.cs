using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldLogic : MonoBehaviour
{
    public float speed = 15f;
    public Rigidbody2D rb;
    private player_movement playerMovement;

    public GameObject platformPrefab;
    private GameObject currentPlatform;
    private Vector3 platformLocation;

    private CircleCollider2D coll;
    [SerializeField] private LayerMask terrain;
    [SerializeField] private LayerMask enemies;

    private ShieldManager shieldManager; // Reference to the ShieldManager script

    void Start()
    {
        rb.velocity = transform.right * speed;
        coll = GetComponent<CircleCollider2D>();
        shieldManager = FindObjectOfType<ShieldManager>(); // Find the ShieldManager in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckCollision()){
            Destroy(gameObject);
            shieldManager.SetShieldState(ShieldManager.MovementState.idle);
            Debug.Log("Collision encounterd");
        }
        else if(!CheckCollision() && Input.GetButtonDown("Fire1")){
            switch (shieldManager.GetShieldState()) // Use the ShieldManager to get the state
            {   
                case ShieldManager.MovementState.stop:
                    rb.velocity = Vector2.zero;
                    platformLocation = transform.position;
                    makePlatform();
                    Destroy(gameObject);
                    break;
            }

        }

    }

    void makePlatform()
    {
        currentPlatform = Instantiate(platformPrefab, platformLocation, Quaternion.identity);
    }

    bool CheckCollision()
    {
        return Physics2D.CircleCast(transform.position, coll.radius/2, Vector2.right, 0.01f, terrain);
    }
}
