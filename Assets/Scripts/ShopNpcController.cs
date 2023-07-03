using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNpcController : MonoBehaviour
{
    public Canvas shop;
    public Canvas gameUI;
    public PlayerController player;
    public Button closeButton;
    public GameObject warningText;

    // Start is called before the first frame update
    void Start()
    {
        shop.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShop()
    {
        Debug.Log("Opening shop");
        gameUI.gameObject.SetActive(false);
        shop.gameObject.SetActive(true);
        warningText.SetActive(false);
    }

    public void CloseShop()
    {
        Debug.Log("Closing shop");
        gameUI.gameObject.SetActive(true);
        shop.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Triggering");
        if ( collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                OpenShop();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Triggering");
        if (collision.gameObject.name == "Player")
        {
            CloseShop();
        }
    }

    public void BuyHeart()
    {
        // Player adds health
        if (player.getButterfliesCollected() >= 1)
        {
            player.spendButterfly();
            player.getHP().AddMax();
        } 
        else
        {
            StartCoroutine(FlashWarningText());
        }
    }

    IEnumerator FlashWarningText()
    {
        warningText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningText.SetActive(false);
    }
}
