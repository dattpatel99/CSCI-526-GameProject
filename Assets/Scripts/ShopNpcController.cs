using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNpcController : MonoBehaviour
{
    public Canvas mainCanvas;
    public GameObject merchantTextBox;
    public GameObject noButterFlyTextBox;

    private bool shopOpen = false;
    private bool shopAble = false;
    
    private ShopPanelController canvasShopController;

    public GameObject player;
    private PlayerController pController;

    private void Start()
    {
        canvasShopController = mainCanvas.GetComponent<ShopPanelController>();
        merchantTextBox.SetActive(false);
        noButterFlyTextBox.SetActive(false);
        pController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (shopAble && Input.GetKeyDown(KeyCode.B))
        {
            if (shopOpen)
            {
                canvasShopController.CloseShop();
                shopOpen = false;
            }
            else
            {
                canvasShopController.OpenShop();
                shopOpen = true;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(displayEnterText());
        shopAble = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            shopAble = false;
            StartCoroutine(displayLeaveText());
            if (shopOpen)
            {
                canvasShopController.CloseShop();
                shopOpen = false;
            }
        }
    }

    IEnumerator displayEnterText()
    {
        if (pController.getButterfliesCollected() == 0)
        {
            noButterFlyTextBox.SetActive(true);
        }
        else
        {
            string textOutput = "Want to exchange butterflies for upgrades? (Press 'B')";
            merchantTextBox.SetActive(true);
            merchantTextBox.transform.GetChild(0).GetComponent<Text>().text = textOutput;
            yield return new WaitForSeconds(6.0f);   
        }
    }

    IEnumerator displayLeaveText()
    {
        if (pController.getButterfliesCollected() == 0)
        {
            noButterFlyTextBox.SetActive(false);
        }
        else
        {
            string textOutput = "Come again!";
            merchantTextBox.SetActive(true);
            merchantTextBox.transform.GetChild(0).GetComponent<Text>().text = textOutput;
            yield return new WaitForSeconds(1.0f);
            merchantTextBox.SetActive(false);
        }
    }
}
