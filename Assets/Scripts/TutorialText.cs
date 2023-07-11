using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public Canvas targetCanvas;
    public string textOutput;
    public bool showAsAssistance = true;
    public Sprite button1;
    public Sprite button2;
    public Sprite button3;
    private TextBoxController textBox;
    private bool showB1 = false;
    private bool showB2 = false;
    private bool showB3 = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            textBox = targetCanvas.GetComponent<TextBoxController>();
            if (button1 != null)
            {
                showB1 = true;
                textBox.SetButton1(button1);
            }
            if (button2 != null)
            {
                showB2 = true;
                textBox.SetButton2(button2);
            }

            if (button3 != null && button1 == null && button2 == null)
            {
                showB3 = true;
                textBox.SetButton3(button3);
            }
            textBox.ShowText(textOutput, showAsAssistance, showB1, showB2, showB3);
            Destroy(this.gameObject);
        }
    }
}