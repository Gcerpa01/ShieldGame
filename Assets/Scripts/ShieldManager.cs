using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public enum MovementState {idle, thrown, stop};
    private MovementState shieldState = MovementState.idle;

    private float nextThrow;
    private float cooldown = 0.3f;

    public MovementState GetShieldState()
    {
        return shieldState;
    }

    public void SetShieldState(MovementState newState)
    {
        shieldState = newState;
        if(shieldState == MovementState.idle) updateThrowTime();
    }

    public bool canThrow(){
        return Time.time > nextThrow;
    }

    private void updateThrowTime(){
        nextThrow = Time.time + cooldown;
    }
}

