using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNpcController : MonoBehaviour
{
    public GameObject shop;
    public GameObject timeStoredText;
    public PlayerController player;
    public Button closeButton;
    public GameObject warningText;
    private bool shopOpen = false;
    private bool shopAble = false;
    
    // Target to canvas
    public GameObject canvas;
    private TextBoxController canvasTextController;

    // Start is called before the first frame update
    void Start()
    {
        shop.gameObject.SetActive(false);
        // Grab the text controller
        this.canvasTextController = canvas.GetComponent<TextBoxController>();
    }

    private void Update()
    {
        if (shopAble && Input.GetKeyDown(KeyCode.B) && !shopOpen)
        {
            canvasTextController.StopMerchantText();
            OpenShop();
        }
    }

    public void OpenShop()
    {
        shopOpen = true;
        timeStoredText.SetActive(false);
        shop.SetActive(true);
        warningText.SetActive(false);
    }

    public void CloseShop()
    {
        shopOpen = false;
        timeStoredText.SetActive(true);
        shop.SetActive(false);
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
                CloseShop();
            }
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
    
    IEnumerator displayEnterText()
    {
        string textOutput = "Want to buy anything? (Press 'B')";
        canvasTextController.ShowMerchantText(textOutput);
        yield return new WaitForSeconds(6.0f);
        canvasTextController.StopMerchantText();
    }
    
    IEnumerator displayLeaveText()
    {
        string textOutput = "Come again!";
        canvasTextController.ShowMerchantText(textOutput);
        yield return new WaitForSeconds(0.5f);
        canvasTextController.StopMerchantText();
    }
}
