using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyController : MonoBehaviour
{

    public PlayerController player;
    public GameObject butterfly;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.name == "Player")
        {
            player.addButterfly();
            butterfly.SetActive(false);
        }
    }
}
