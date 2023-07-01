using System.Collections.Generic;
using UnityEngine;
using Analytics;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;


/**
 * This is the main analysis manager from which all analysis objects can be accessed
 */
public class AnalyticManager : MonoBehaviour
{
    // Objects
    public GameObject player;
    private PlayerController controller;
    private TimeBank bank;
    
    // Session objects
    private AnalyticGameSession session;
    private string playID;
    private float rt = 0.0f;
    private long sessionId;
    private string levelName;
    private string userId;
    
    // Analytics
    private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
    private static int shotID;

    private void Awake()
    {
        userId = AnalyticsSessionInfo.userId;
        sessionId = AnalyticsSessionInfo.sessionId;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
        
        // avoid too many get Components
        controller = player.GetComponent<PlayerController>();
        bank = player.GetComponent<TimeBank>();

        // For session Analytics
        session = new AnalyticGameSession(sessionId, userId);
        playID = System.Guid.NewGuid().ToString();
        shotID = 0;
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
    public void SendShootInfo(int x, int y, int z, int timeStored, int age, int health, string clickType, string interactionType, string objectInteracted)
    {
        shotID++;
        var info = new ShootMapping(x, y, z,timeStored, age, health, clickType, interactionType, objectInteracted, rt);
        StoreData(JsonConvert.SerializeObject(info), "GunDetail", shotID);
    }

    public void SendSessionInfo(bool finished)
    {
        session.DataUpdate(finished, levelName, this.rt, controller.getHP().GetHP(), bank.GetTimeStore(), controller.getNumberDeaths());
        StoreData(session.ToString(), "GameAnalytic");
    }
    
    private void StoreData(string json, string location)
    {
        // Implements sending data when on WebGL Build
        if (!Application.isEditor)
        {
            RestClient.Put($"{baseURL}/Beta/{location}/{sessionId.ToString()}_{playID}_{levelName}/.json", json);
        }
        else
        {
            RestClient.Put($"{baseURL}/preBetaTesting/{location}/{sessionId.ToString()}_{playID}_{levelName}/.json", json);
        }
    }
    
    private void StoreData(string json, string location, int id)
    {
        // Implements sending data when on WebGL Build
        if (!Application.isEditor)
        {
            RestClient.Put($"{baseURL}/Beta/{location}/{sessionId.ToString()}_{playID}_{levelName}/{id}/.json", json);
        }
        else
        {
            RestClient.Put($"{baseURL}/preBetaTesting/{location}/{sessionId.ToString()}_{playID}_{levelName}/{id}/.json", json);
        }
    }
}
