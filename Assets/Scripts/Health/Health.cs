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
    private Animator anim;

    private void Awake(){
        curr_Health = startHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage){
        curr_Health = Mathf.Clamp(curr_Health - damage,0,startHealth);

        if(!dead && curr_Health == 0){
            foreach(Behaviour component in components) component.enabled = false;
            anim.SetTrigger("PlayerDied");
            dead = true;
        }

    }

    public float getCurrentHealth(){
        return curr_Health;
    }

    public void Respawn(){
        anim.ResetTrigger("PlayerDied");
        anim.Play("Character Idle Rig 48x48_Clip");

        curr_Health = startHealth;
        foreach(Behaviour component in components) component.enabled = true;
        dead = false;
    }
    
}
