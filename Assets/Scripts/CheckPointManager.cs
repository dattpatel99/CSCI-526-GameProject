using System.Collections.Generic;
using Analytics;
using UnityEngine.SceneManagement;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using UnityEngine.Analytics;

public class CheckPointManager : MonoBehaviour
{
    private static int crossedCheckPoints = 0;
    private long sessionID;
    private static List<Transform> _checkpoints = new List<Transform>();
    private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
    private string levelName;

    void Start()
    {
        sessionID = AnalyticsSessionInfo.sessionId;
        Transform parentTransform = gameObject.transform;
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            _checkpoints.Add(parentTransform.GetChild(i).gameObject.transform);
        }
    }

    public void SendData(CheckPointAnalytics crossedPoint)
    {
        crossedCheckPoints++;
        StoreData(JsonConvert.SerializeObject(crossedPoint));
    }

    public void resetCrossedCheckPoints()
    {
        crossedCheckPoints = 0;
    }
    
        
    private void StoreData(string json)
    {
        levelName = SceneManager.GetActiveScene().name;
        
        // Implements sending data when on WebGL Build
        if (!Application.isEditor)
        {
            RestClient.Post(baseURL + "/alpha/checkpointGraph/" + sessionID.ToString() + '/' + levelName + '/' + crossedCheckPoints.ToString() + "/.json", json);
        }
        else
        {
            RestClient.Post(baseURL + "/testing/checkpointGraph/" + sessionID.ToString() + '/' + levelName + '/' + crossedCheckPoints.ToString() + "/.json", json);
        }
    }
    
}
