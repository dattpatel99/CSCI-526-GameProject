using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPodiumController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject podiumGun;
    public GameObject player;

    private GameObject playerGun;

    void Start()
    {
        playerGun = player.transform.GetChild(0).gameObject;
        playerGun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.name == "Player")
        {
            podiumGun.SetActive(false);
            playerGun.SetActive(true);
        }
    }
}
