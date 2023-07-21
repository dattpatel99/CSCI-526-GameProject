using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPodiumController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject podiumKey;
    public GameObject enemyTurtle;

    public GameObject mapIcon;
    public static bool keyMapIcon = false;

    private bool activated = false;
    void Start()
    {
        podiumKey.SetActive(false);
        mapIcon.SetActive(false);
    }

    private void Update()
    {
        alterIcon(keyMapIcon);

        if (enemyTurtle == null && !activated)
        {
            podiumKey.SetActive(true);
            activated = true;
        }
    }

    private void alterIcon(bool show)
    {
        if (podiumKey == null)
        {
            mapIcon.SetActive(false);
        }
        else
        {
            mapIcon.SetActive(show);
        }
        
    }
}
