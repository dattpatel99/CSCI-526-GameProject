using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateController : MonoBehaviour
{
    public PlayerController player;
    public GameObject opened;
    public GameObject closed;
    public int required;
    public Text keyReq;

    private bool isOpen;

    private void Start()
    {
        isOpen = false;
        closed.SetActive(true);
        opened.SetActive(false);
        keyReq.text = "x" + required.ToString();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isOpen)
        {
            isOpen = true;
        }
        else if (other.gameObject.CompareTag("Player") && (player.getKeyCollected() >= required))
        {
            player.spendKey(required);
            gameObject.GetComponent<Collider2D>().enabled = false;
            closed.SetActive(false);
            opened.SetActive(true); 
        }
    }
}
