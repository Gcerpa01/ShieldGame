using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public enum MovementState {idle, thrown, stop};
    private MovementState shieldState = MovementState.idle;


    public MovementState GetShieldState()
    {
        return shieldState;
    }

    public void SetShieldState(MovementState newState)
    {
        shieldState = newState;
    }
}

