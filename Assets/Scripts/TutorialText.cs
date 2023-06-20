using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public Canvas targetCanvas;
    public string textOutput;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            targetCanvas.GetComponent<TextBoxController>().ShowText(textOutput, true);
            Destroy(this.gameObject);
        }
    }
}
