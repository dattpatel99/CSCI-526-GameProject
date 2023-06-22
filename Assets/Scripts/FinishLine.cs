using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public TextMeshProUGUI finishText;  // Finish Text
    public string sceneToLoad_name;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        finishText.text = "";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishText.text = "Congratulations!";
            Time.timeScale = 0f;
            StartCoroutine(WaitNLoad(2));
        }
    }
    
    private IEnumerator WaitNLoad(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        SceneManager.LoadScene(sceneToLoad_name);
        // Paul: the message below will never be displayed
        //      if timescale has been set to 0 before entering this coroutine
        // yield return new WaitForSeconds(seconds);
        // Debug.Log("You won't see this");
    }
}
