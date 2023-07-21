using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject mapIcon;
    public static bool keyMapIcon = false;

    public PlayerController player;

    private void Start()
    {
        mapIcon.SetActive(false);
    }

    private void Update()
    {
        alterIcon(keyMapIcon);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.addKey();
            Destroy(gameObject);
        }
    }

    private void alterIcon(bool show)
    {
        mapIcon.SetActive(show);
    }
}
