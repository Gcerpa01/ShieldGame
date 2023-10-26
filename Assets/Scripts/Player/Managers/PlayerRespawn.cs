using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip cpSound;

    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake(){
        playerHealth = GetComponent<Health>();
    }

    public void Respawn(){
        playerHealth.Respawn();
        transform.position = currentCheckpoint.position;
    }

    private void OnTriggerEnter2D(Collider2D coll){
        if(coll.transform.tag == "Checkpoint"){
            currentCheckpoint = coll.transform;
            coll.GetComponent<Collider2D>().enabled = false;
            coll.GetComponent<Animator>().SetTrigger("Activated");
        }
    }

}

