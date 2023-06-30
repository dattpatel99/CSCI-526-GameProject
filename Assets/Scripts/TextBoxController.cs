using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour
{
    public GameObject basicTextBox;
    public GameObject assistantTextBox;

    private Text basicTextComponent;
    private Text assistantTextComponent;

    private Image basicArrow;
    private Image assistantArrow;

    private bool assistantPrinting;
    private bool basicPrinting;
    private float timer;

    private string assistantText;
    private string basicText;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        basicTextBox.SetActive(false);
        assistantTextBox.SetActive(false);

        basicTextComponent = basicTextBox.transform.GetChild(0).GetComponent<Text>();
        assistantTextComponent = assistantTextBox.transform.GetChild(0).GetComponent<Text>();

        basicArrow = basicTextBox.transform.GetChild(1).GetComponent<Image>();
        assistantArrow = assistantTextBox.transform.GetChild(1).GetComponent<Image>();

        basicArrow.enabled = false;
        assistantArrow.enabled = false;
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
            if (timer >= Time.deltaTime)
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
            if (timer >= Time.deltaTime)
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
                StopText();
            }
        }
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

    private void StopText()
    {
        assistantTextComponent.text = "";
        basicTextComponent.text = "";

        basicTextBox.SetActive(false);
        assistantTextBox.SetActive(false);

        basicArrow.enabled = false;
        assistantArrow.enabled = false;
    }
}
