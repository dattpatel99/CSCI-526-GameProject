using UnityEngine;
using Analytics;
using Proyecto26;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;


/**
 * This is the main analysis manager from which all analysis objects can be accessed
 */
public class AnalyticManager : MonoBehaviour
{
    private AnalyticGameSession session;
    public GameObject player;
    private PlayerController controller;
    private TimeBank bank;
    private float rt = 0.0f;
    private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
    private long sessionId;
    private string levelName;
    private string userId;

    private void Awake()
    {
        userId = AnalyticsSessionInfo.userId;
        sessionId = AnalyticsSessionInfo.sessionId;
    }

    // Start is called before the first frame update
    void Start()
    {
        session = new AnalyticGameSession(sessionId, userId);
        controller = player.GetComponent<PlayerController>();
        bank = player.GetComponent<TimeBank>();
    }

    // Update is called once per frame
    void Update()
    {
        rt += Time.deltaTime;
    }

    public string getCurrentData()
    {
        return this.session.ToString();
    }

    public void SendSessionInfo(bool finished)
    {
        levelName = SceneManager.GetActiveScene().name;
        session.DataUpdate(finished, levelName, this.rt, controller.getHP().GetHP(), bank.GetTimeStore());
        StoreData(session.ToString());
    }
    
    private void StoreData(string json)
    {
        // Implements sending data when on WebGL Build
        if (!Application.isEditor)
        {
            RestClient.Post(baseURL + "/alpha/GameAnalytic/" + sessionId.ToString() + '/' + levelName + "/.json", json);
        }
        else
        {
            RestClient.Post(baseURL + "/testing/GameAnalytic/" + sessionId.ToString() + '/' + levelName + "/.json", json);
        }
    }
}
