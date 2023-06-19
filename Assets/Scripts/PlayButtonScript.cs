using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour
{
    
    public void PlayGame () // Loads Next Scene
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame() //quits game when quit button pressed
{
    Debug.Log("Quitting Game...");
    Application.Quit();
}
}


