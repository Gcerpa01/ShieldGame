using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField] private float forwardDist;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;

        private void Start()
    {
        lookAhead = forwardDist * player.localScale.x;
    }
    
    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.position.x+lookAhead, player.position.y,transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead,(forwardDist*player.localScale.x),Time.deltaTime * cameraSpeed);
    }
}
