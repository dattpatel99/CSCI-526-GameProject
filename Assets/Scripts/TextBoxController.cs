using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour
{
    public GameObject basicTextBox;
    public GameObject assistantTextBox;
    public GameObject merchantTextBox;

    private Text basicTextComponent;
    private Text assistantTextComponent;
    private Text merchantTextComponent;

    private Image basicArrow;
    private Image assistantArrow;

    private bool assistantPrinting;
    private bool basicPrinting;
    private float timer;

    private string assistantText;
    private string basicText;
    private int index;
    private float textSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        merchantTextBox.SetActive(false);
        merchantTextComponent = merchantTextBox.transform.GetChild(0).GetComponent<Text>();
            
        assistantTextBox.SetActive(false);
        assistantTextComponent = assistantTextBox.transform.GetChild(0).GetComponent<Text>();
        assistantArrow = assistantTextBox.transform.GetChild(1).GetComponent<Image>();
        assistantArrow.enabled = false;

        basicTextBox.SetActive(false);
        basicTextComponent = basicTextBox.transform.GetChild(0).GetComponent<Text>();
        basicArrow = basicTextBox.transform.GetChild(1).GetComponent<Image>();
        basicArrow.enabled = false;
        
        timer = 0f;
        index = 0;
    }

    void Update()
    {
        if (assistantPrinting || basicPrinting)
        {
            timer += Time.deltaTime;
        }

        if (assistantPrinting)
        {
            if ((timer)/textSpeed >= (Time.deltaTime))
            {
                assistantTextComponent.text += assistantText[index];

                if (index == assistantText.Length - 1)
                {
                    assistantArrow.enabled = true;
                    assistantPrinting = false;
                    index = 0;
                }
                else
                {
                    index++;
                }
                
                timer = 0f;
            }
        }
        else if (basicPrinting)
        {
            if ((timer)/textSpeed >= Time.deltaTime)
            {
                basicTextComponent.text += basicText[index];

                if (index == basicText.Length - 1)
                {
                    basicArrow.enabled = true;
                    basicPrinting = false;
                    index = 0;
                }
                else
                {
                    index++;
                }
                
                timer = 0f;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopText(false);
            }
        }
    }

    public void ShowMerchantText(string text)
    {
        merchantTextComponent.text = text;
        merchantTextBox.SetActive(true);
    }
    
    public void StopMerchantText()
    {
        merchantTextComponent.text = "";
        merchantTextBox.SetActive(false);
    }

    public void ShowText(string text, bool isAssistant)
    {

        if (isAssistant)
        {
            assistantTextComponent.text = "";
            assistantText = text;
            assistantTextBox.SetActive(true);
            assistantPrinting = true;
            assistantArrow.enabled = false;
        }
        else
        {
            basicTextComponent.text = "";
            basicText = text;
            basicTextBox.SetActive(true);
            basicPrinting = true;
            basicArrow.enabled = false;
        }

        timer = 0f;
    }

    public void StopText(bool basicOnly)
    {
        
        basicTextBox.SetActive(false);
        basicTextComponent.text = "";
        basicArrow.enabled = false;

        if (!basicOnly)
        {
            assistantTextComponent.text = "";
            assistantTextBox.SetActive(false);
            assistantArrow.enabled = false;
        }
    }
}
