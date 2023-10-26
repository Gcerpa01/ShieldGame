using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPitLogic : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player died");
            float fall_damage = collision.GetComponent<Health>().getCurrentHealth();
            Debug.Log("Current Health: " + fall_damage);
            collision.GetComponent<Health>().TakeDamage(fall_damage);
            // collision.GetComponent<Health>().TakeDamage(10);
        }
    }

}
