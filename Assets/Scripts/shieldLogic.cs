using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldLogic : MonoBehaviour
{

    private player_movement playerMovement;
    private GameObject currentPlatform;
    private Vector3 platformLocation;

    private CircleCollider2D coll;

    [Header("Shield Specs")]
    public float speed = 15f;
    public Rigidbody2D rb;
    public GameObject platformPrefab;
    



    [Header("Layer Masks")]
    [SerializeField] private LayerMask terrain;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private LayerMask switches;


    private bool returnShield = false;
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

        if(Input.GetButtonDown("Fire1")){
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

        else if(returnShield){
            rb.velocity = -transform.right * speed; // move back towards player
            if(CheckPlayerReturn()){
                Destroy(gameObject);
                shieldManager.SetShieldState(ShieldManager.MovementState.idle);
                Debug.Log("Shield returned");
                returnShield = false;
            } 
        }
        else if(CheckCollisions()){
            Debug.Log("Collision encounterd");
            returnShield = true;
        }

    }

    void makePlatform()
    {
        currentPlatform = Instantiate(platformPrefab, platformLocation, Quaternion.identity);
    }

    bool CheckTerrainCollision()
    {
        return Physics2D.CircleCast(transform.position, coll.radius/2, Vector2.right, 0.01f, terrain);
    }

    bool CheckSwitchCollision(){
        return Physics2D.CircleCast(transform.position, coll.radius/2, Vector2.right, 0.01f, switches);
    }

    bool CheckEnemyCollision(){
        return Physics2D.CircleCast(transform.position, coll.radius/2, Vector2.right, 0.01f, enemies);
    }

    bool CheckCollisions(){
        return CheckSwitchCollision() || CheckTerrainCollision() || CheckEnemyCollision();
    }

    bool CheckPlayerReturn(){
        return Physics2D.CircleCast(transform.position, coll.radius/2, Vector2.left, 0.01f, player);
    }

}
