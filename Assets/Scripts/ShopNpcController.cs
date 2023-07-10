using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNpcController : MonoBehaviour
{
    public Canvas mainCanvas;
    public GameObject merchantTextBox;

    private bool shopOpen = false;
    private bool shopAble = false;

    //private TextBoxController canvasTextController;
    private ShopPanelController canvasShopController;

    private void Start()
    {
        canvasShopController = mainCanvas.GetComponent<ShopPanelController>();
        //canvasTextController = mainCanvas.GetComponent<TextBoxController>();
        merchantTextBox.SetActive(false);
    }

    private void Update()
    {
        if (shopAble && Input.GetKeyDown(KeyCode.B) && !shopOpen)
        {
            //canvasTextController.StopMerchantText();
            canvasShopController.OpenShop();
            shopOpen = true;
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
        string textOutput = "Want to exchange butterflies for upgrades? (Press 'B')";
        merchantTextBox.SetActive(true);
        merchantTextBox.transform.GetChild(0).GetComponent<Text>().text = textOutput;
        //canvasTextController.ShowMerchantText(textOutput);
        yield return new WaitForSeconds(6.0f);
        //merchantTextBox.SetActive(false);
        //canvasTextController.StopMerchantText();
    }

    IEnumerator displayLeaveText()
    {
        string textOutput = "Come again!";
        merchantTextBox.SetActive(true);
        merchantTextBox.transform.GetChild(0).GetComponent<Text>().text = textOutput;
        //canvasTextController.ShowMerchantText(textOutput);
        yield return new WaitForSeconds(1.0f);
        merchantTextBox.SetActive(false);
        //canvasTextController.StopMerchantText();
    }


}
