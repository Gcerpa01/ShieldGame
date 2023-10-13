using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public enum MovementState {idle, thrown, stop};
    private MovementState shieldState = MovementState.idle;

    private float nextThrow;
    private float cooldown = 0.3f;
    private bool isBlocking = false;

    public MovementState GetShieldState()
    {
        return shieldState;
    }

    public void SetShieldState(MovementState newState) 
    {
        shieldState = newState;
        if(shieldState == MovementState.idle) updateThrowTime(); //cooldown
    }

    //check if cooldown is met and character is not blocking
    public bool canThrow(){
        return (Time.time > nextThrow) && !isBlocking; 
    }

    //check if can block
    public bool canBlock(){
        return shieldState == MovementState.idle;
    }
    

    //update cooldown
    private void updateThrowTime(){
        nextThrow = Time.time + cooldown;
    }

    public void setBlocking(bool newState){
        isBlocking = newState;
    }

    public bool GetBlockingState(){
        return isBlocking;
    } 
    
}

