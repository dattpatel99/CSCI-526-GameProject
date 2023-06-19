using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    public GameObject heartContainer;
    private int damageValue = 1;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.name == "Tilemap_Water")
            {
                damageValue = 3;
            }
            else
            {
                damageValue = 1;
            }
            heartContainer.GetComponent<PlayerHealth>().DamagePlayer(damageValue);
            if (heartContainer.GetComponent<PlayerHealth>().GetHP() == 0)
            {
                StartCoroutine(RestartPlayer(other.gameObject, heartContainer));
            }
        }
    }

    IEnumerator RestartPlayer(GameObject player, GameObject hearts)
    {
        // Respawn at last checkpoint
        Time.timeScale = 0; // Pause movement
        yield return new WaitForSecondsRealtime(2); // Wait 2 seconds to restart  
        player.GetComponent<Transform>().position = player.GetComponent<PlayerController>().getRespwan();
        hearts.GetComponent<PlayerHealth>().HealPlayer(3);
        Time.timeScale = 1; // Continue movement 
    }
}
