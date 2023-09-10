using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject shieldPrefab;

    private enum MovementState {thrown,stop,release};
    
    MovementState shieldState = MovementState.thrown;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            throwShield();
        }

    }

    void throwShield(){
        switch(shieldState){
            case MovementState.thrown:
                Instantiate(shieldPrefab,launchPoint.position,launchPoint.rotation);
                shieldState = MovementState.stop;
                break;
            case MovementState.stop:
                shieldState = MovementState.release;
                break;
            case MovementState.release:
                shieldState = MovementState.thrown;
                break;
        }
    }
}
