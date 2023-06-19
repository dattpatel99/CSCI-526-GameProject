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

    private bool textDisplaying;

    // Start is called before the first frame update
    void Start()
    {
        basicTextBox.SetActive(false);
        assistantTextBox.SetActive(false);

        basicText = basicTextBox.transform.GetChild(0).GetComponent<Text>();
        assistantText = assistantTextBox.transform.GetChild(0).GetComponent<Text>();
    }

    void Update()
    {
        if (textDisplaying)
        {
            if (Input.anyKey)
            {
                StopText();
            }
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
        Time.timeScale = 0.0f;
    }

    private void StopText()
    {
        textDisplaying = false;
        Time.timeScale = 1.0f;

        basicTextBox.SetActive(false);
        assistantTextBox.SetActive(false);
    }
}
