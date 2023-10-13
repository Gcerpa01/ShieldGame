using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject shieldPrefab;

    private player_movement playerMovement;
    private ShieldManager shieldManager; // Reference to the ShieldManager script

    void Start()
    {
        playerMovement = FindObjectOfType<player_movement>();
        shieldManager = FindObjectOfType<ShieldManager>(); // Find the ShieldManager in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && playerMovement.isGrounded())
        {
            throwShield();
        }
        else if (Input.GetButtonDown("Fire1") && shieldManager.GetShieldState() != ShieldManager.MovementState.idle)
        {
            throwShield();
        }

        else if (Input.GetButtonDown("Fire2") && shieldManager.canBlock() && playerMovement.isGrounded()){
            shieldManager.setBlocking(true);
            Debug.Log("Blocking");
        }
        else if (Input.GetButtonUp("Fire2")) {
            Debug.Log("Stopped Blocking");
            shieldManager.setBlocking(false);
        }
    }

    void throwShield()
    {
        switch (shieldManager.GetShieldState()) // Use the ShieldManager to get the state
        {
            case ShieldManager.MovementState.idle:
                if(shieldManager.canThrow()){
                    // Instantiate the shield.
                    Instantiate(shieldPrefab, launchPoint.position, launchPoint.rotation);
                    shieldManager.SetShieldState(ShieldManager.MovementState.thrown); // Update the state through the ShieldManager
                    Debug.Log("Weapon updated to thrown");
                }
                else Debug.Log("Weapon can not be thrown yet");
                break;
            case ShieldManager.MovementState.thrown:
                shieldManager.SetShieldState(ShieldManager.MovementState.stop); // Update the state through the ShieldManager
                Debug.Log("Weapon updated to stopped");
                break;
            case ShieldManager.MovementState.stop:
                Debug.Log("Weapon updated to idle");
                shieldManager.SetShieldState(ShieldManager.MovementState.idle); // Update the state through the ShieldManager
                break;
        }
    }


}
