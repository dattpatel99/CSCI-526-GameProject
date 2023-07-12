using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ComputerController : MonoBehaviour
{   
    public GameObject intialTextBox;
    private Text initialText;
    
    // Instruction Box Objects
    public GameObject instructionTextBox;
    private Image button1;
    private Image button2;
    private Text text;
    public string instructionText;
    public Sprite spriteButton1;
    public Sprite spriteButton2;

    public bool instructionTypeSingleButton = false;
    private void Start()
    {
        //canvasTextController = mainCanvas.GetComponent<TextBoxController>();
        intialTextBox.SetActive(false);
        initialText = intialTextBox.transform.GetChild(0).GetComponent<Text>();

        instructionTextBox.SetActive(false);
        if (instructionTypeSingleButton)
        {
            text = instructionTextBox.transform.GetChild(0).GetComponent<Text>();
            button1 = instructionTextBox.transform.GetChild(1).GetComponent<Image>();
            button1.sprite = spriteButton1;
            text.text = instructionText;
        }
        else
        {
            text = instructionTextBox.transform.GetChild(0).GetComponent<Text>();
            button1 = instructionTextBox.transform.GetChild(1).GetComponent<Image>();
            button2 = instructionTextBox.transform.GetChild(2).GetComponent<Image>();
            button1.sprite = spriteButton1;
            button2.sprite = spriteButton2;
            text.text = instructionText;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            displayEnterText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StopAll();
        }
    }

    private void displayEnterText()
    {
        instructionTextBox.SetActive(true);
    }

    private void StopAll()
    {
        instructionTextBox.SetActive(false);
    }
}
