using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().name == "Player")
        {
            Vector3 respawnPost = new Vector3(this.GameObject().transform.position.x, other.GameObject().transform.position.y, this.GameObject().transform.position.z);
            other.GameObject().GetComponent<PlayerController>().setRespwan(respawnPost);
        }
    }
}
