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

    private Image button1;
    private Image button2;
    private Image button3;

    private bool assistantPrinting;
    private bool basicPrinting;
    private float timer;

    private string assistantText;
    private string basicText;
    private int index;
    private float textSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        assistantTextBox.SetActive(false);
        assistantTextComponent = assistantTextBox.transform.GetChild(0).GetComponent<Text>();
        assistantArrow = assistantTextBox.transform.GetChild(1).GetComponent<Image>();
        assistantArrow.enabled = false;
        
        button1 = assistantTextBox.transform.GetChild(2).GetComponent<Image>();
        button1.enabled = false;
        button2 = assistantTextBox.transform.GetChild(3).GetComponent<Image>();
        button2.enabled = false;
        button3 = assistantTextBox.transform.GetChild(4).GetComponent<Image>();
        button3.enabled = false;

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

    public void ShowText(string text, bool isAssistant, bool showB1, bool showB2, bool showB3)
    {

        if (isAssistant)
        {
            button1.enabled = showB1;
            button2.enabled = showB2;
            button3.enabled = showB3;
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

        index = 0;
    }

    public void SetButton1(Sprite img)
    {
        button1.sprite = img;
    }

    public void SetButton2(Sprite img)
    {
        button2.sprite = img;
    }
    public void SetButton3(Sprite img)
    {
        button3.sprite = img;
    }
}
