using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPodiumController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject podiumKey;
    public GameObject enemyTurtle;

    private bool activated = false;
    void Start()
    {
        podiumKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTurtle == null && !activated)
        {
            podiumKey.SetActive(true);
            activated = true;
        }
    }
}
