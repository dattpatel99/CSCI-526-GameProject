using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxController : MonoBehaviour
{
    public GameObject basicTextBox;
    public GameObject assistantTextBox;

    private Text basicText;
    private Text assistantText;

    private Image basicArrow;
    private Image assistantArrow;

    private bool textDisplaying;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        basicTextBox.SetActive(false);
        assistantTextBox.SetActive(false);

        basicText = basicTextBox.transform.GetChild(0).GetComponent<Text>();
        assistantText = assistantTextBox.transform.GetChild(0).GetComponent<Text>();

        basicArrow = basicTextBox.transform.GetChild(1).GetComponent<Image>();
        assistantArrow = assistantTextBox.transform.GetChild(1).GetComponent<Image>();

        basicArrow.enabled = false;
        assistantArrow.enabled = false;
        timer = 0f;
    }

    void Update()
    {
        if (textDisplaying)
        {
            if (timer >= 3.0f)
            {
                basicArrow.enabled = true;
                assistantArrow.enabled = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    StopText();
                }
            }

            timer += Time.deltaTime;
        }
    }

    public void ShowText(string text, bool isAssistant)
    {

        if (isAssistant)
        {
            assistantText.text = text;
            assistantTextBox.SetActive(true);
        }
        else
        {
            basicText.text = text;
            basicTextBox.SetActive(true);
        }

        textDisplaying = true;
        timer = 0f;
        basicArrow.enabled = false;
        assistantArrow.enabled = false;
    }

    private void StopText()
    {
        textDisplaying = false;

        basicTextBox.SetActive(false);
        assistantTextBox.SetActive(false);

        basicArrow.enabled = false;
        assistantArrow.enabled = false;
    }
}
