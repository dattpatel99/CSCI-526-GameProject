using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    // Loads Next Scene
    public void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() //quits game when quit button pressed
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}


