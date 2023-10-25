using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected int OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){

            player_movement playerMovement = collision.GetComponent<player_movement>();

            //player deflected
            if (playerMovement != null && playerMovement.shieldManager != null && playerMovement.shieldManager.GetBlockingState())return 0;

            Debug.Log("Player got hit");
            //player did not deflect
            collision.GetComponent<Health>().TakeDamage(damage);
            return 1;
        }
        if(collision.tag == "Enemy"){
            collision.GetComponent<Health>().TakeDamage(damage);
            return  2;
        }
        return 3;
    }

}
