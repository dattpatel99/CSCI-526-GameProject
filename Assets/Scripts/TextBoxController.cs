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
    }

    void Update()
    {
        if (textDisplaying)
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
            assistantText.text = text;
            assistantTextBox.SetActive(true);
        }
        else
        {
            basicText.text = text;
            basicTextBox.SetActive(true);
        }

        StartCoroutine(WaitForRead());
    }

    private void StopText()
    {
        textDisplaying = false;

        basicTextBox.SetActive(false);
        assistantTextBox.SetActive(false);

        basicArrow.enabled = false;
        assistantArrow.enabled = false;
    }

    IEnumerator WaitForRead()
    {
        yield return new WaitForSeconds(3);

        basicArrow.enabled = true;
        assistantArrow.enabled = true;

        textDisplaying = true;
    }
}
