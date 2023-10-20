using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLogic : MonoBehaviour
{
    public Sprite newSprite;
    private Sprite originalSprite;
    public bool clicked = false;

    private void Start()
    {
        // Store the original color of the object
        originalSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // This method is called when a trigger-based collision occurs.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            GetComponent<SpriteRenderer>().sprite = newSprite;
            clicked = true;
        }
    }
}
