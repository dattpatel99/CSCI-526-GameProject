using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    private float offset = 6; 
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + offset, 0, -10);
        
    }
}
