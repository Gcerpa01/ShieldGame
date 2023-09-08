using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject shieldPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) throwShield();
    }

    void throwShield(){
        Instantiate(shieldPrefab,launchPoint.position,launchPoint.rotation);
    }
}
