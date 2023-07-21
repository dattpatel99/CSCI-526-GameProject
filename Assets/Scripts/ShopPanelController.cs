using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Proyecto26;
using Analytics;
using UnityEngine.SceneManagement;

public class ShopPanelController : MonoBehaviour
{

    private static int purchaseNum = 0;
    public GameObject shop;
    public GameObject timeStoredText;
    public PlayerController player;
    public Button closeButton;
    public GameObject warningText;
    public GameObject shieldRow;
    public GameObject analytics;
    private AnalyticManager _analyticManager;
    private string levelName;


    // Start is called before the first frame update
    void Start()
    {
        shop.gameObject.SetActive(false);
        _analyticManager = analytics.GetComponent<AnalyticManager>();
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
            purchaseNum++;
            var temp = new ShopAnalytics("Heart", player.getButterfliesCollected());
            StoreData(JsonConvert.SerializeObject(temp));
        }
        else
        {
            StartCoroutine(FlashWarningText());
        }
    }

    public void BuyShield()
    {
        if (player.getButterfliesCollected() >= 1)
        {
            player.spendButterfly(1);
            player.ActivateShield();
            shieldRow.SetActive(false);
            purchaseNum++;
            var temp = new ShopAnalytics("Shield", player.getButterfliesCollected());
            StoreData(JsonConvert.SerializeObject(temp));

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
    private void StoreData(string json)
    {
        levelName = SceneManager.GetActiveScene().name;
        // Implements sending data when on WebGL Build
        if (!Application.isEditor)
        {
            RestClient.Put($"{_analyticManager.getDeployLink()}/ShopAnalytic/{_analyticManager.GetSessionID().ToString()}_{_analyticManager.GetPlayID()}_{levelName}/{purchaseNum}/.json", json);
        }
        else
        {
            RestClient.Put($"{_analyticManager.getEditLink()}/ShopAnalytic/{_analyticManager.GetSessionID().ToString()}_{_analyticManager.GetPlayID()}_{levelName}/{purchaseNum}/.json", json);
        }
    }
}
