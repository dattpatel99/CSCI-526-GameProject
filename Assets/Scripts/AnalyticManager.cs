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
    private string playID;
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
        playID = System.Guid.NewGuid().ToString();
        controller = player.GetComponent<PlayerController>();
        bank = player.GetComponent<TimeBank>();
    }

    public string GetPlayID()
    {
        return playID;
    }

    public long GetSessionID()
    {
        return this.sessionId;
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
            RestClient.Put(baseURL + "/alpha/GameAnalytic/" + sessionId.ToString() + '_' + playID + '_' + levelName + "/.json", json);
        }
        else
        {
            RestClient.Put(baseURL + "/testing/v2.0/GameAnalytic/" +  sessionId.ToString() + '_' + playID + '_' + levelName + "/.json", json);
        }
    }
}
