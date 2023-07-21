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
    public GameObject mapIcon;
    public static bool shopMapIcon = false;
    private void Start()
    {
        mapIcon.SetActive(false);
        canvasShopController = mainCanvas.GetComponent<ShopPanelController>();
        merchantTextBox.SetActive(false);
        pController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        alterIcon(shopMapIcon);
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
        displayEnterText();
        shopAble = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            shopAble = false;
            displayLeaveText();
            if (shopOpen)
            {
                canvasShopController.CloseShop();
                shopOpen = false;
            }
        }
    }

    private void displayEnterText()
    {
        string textOutput = "Got any butterflies to shop with? (Press 'B')";
        merchantTextBox.SetActive(true);
        merchantTextBox.transform.GetChild(0).GetComponent<Text>().text = textOutput;
    }

    private void displayLeaveText()
    {
        merchantTextBox.SetActive(false);
    }
    
    private void alterIcon(bool show)
    {
        mapIcon.SetActive(show);
    }
}
