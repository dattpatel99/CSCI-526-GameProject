using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public Text finishText;  // Finish Text
    public string sceneToLoad_name;
    public GameObject analytics;
    public GameObject checkParent;
    public GameObject sectionParent;
    private AnalyticManager analyticManager;
    private CheckPointManager checkpointManager;
    private SectionManager sectionManager;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        finishText.text = "";
        analyticManager = analytics.GetComponent<AnalyticManager>();
        checkpointManager = checkParent.GetComponent<CheckPointManager>();
        sectionManager = sectionParent.GetComponent<SectionManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishText.text = "Level Complete!";
            Time.timeScale = 0f;
            checkpointManager.resetCrossedCheckPoints();
            sectionManager.resetCrossedSections();
            analyticManager.SendSessionInfo(true);
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
