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
}
