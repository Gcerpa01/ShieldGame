using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] private float startHealth;


    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private float curr_Health;
    private bool dead;

    private void Awake(){
        curr_Health = startHealth;
    }

    public void TakeDamage(float damage){
        curr_Health = Mathf.Clamp(curr_Health - damage,0,startHealth);

        
        if(!dead && curr_Health == 0){

            foreach(Behaviour component in components) component.enabled = false;
            dead = true;
        }

    }


}
