using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape)) //pause or resume using esc key
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume() //resume function for esc key or resume button
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause () //pause function for esc key
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true; 
    }

    public void GoToMainMenu() //if go to main menu button is pressed, redirects to main menu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame() //quits game when quit button pressed
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void SkipTutorial() //if go to main menu button is pressed, redirects to main menu
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}