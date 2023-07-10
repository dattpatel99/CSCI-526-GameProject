using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelController : MonoBehaviour
{

    public GameObject shop;
    public GameObject timeStoredText;
    public PlayerController player;
    public Button closeButton;
    public GameObject warningText;
    public GameObject shieldRow;
    

    // Start is called before the first frame update
    void Start()
    {
        shop.gameObject.SetActive(false);
        // Grab the text controller
    }

    public void OpenShop()
    {
        timeStoredText.SetActive(false);
        shop.SetActive(true);
        warningText.SetActive(false);
    }

    public void CloseShop()
    {
        timeStoredText.SetActive(true);
        shop.SetActive(false);
    }

    public void BuyHeart()
    {
        // Player adds health
        if (player.getButterfliesCollected() >= 1)
        {
            player.spendButterfly(1);
            player.getHP().AddMax();
        }
        else
        {
            StartCoroutine(FlashWarningText());
        }
    }

    public void BuyShield()
    {
        if (player.getButterfliesCollected() >= 3)
        {
            player.spendButterfly(3);
            player.ActivateShield();
            shieldRow.SetActive(false);
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
