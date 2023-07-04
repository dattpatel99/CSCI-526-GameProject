using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItemShopController : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyHeart()
    {
        // Player adds health
        if ( player.getButterfliesCollected() >= 1 )
        {
            player.spendButterfly();
            player.getHP().AddMax();
        }
    }
}
