using System.Collections.Generic;
using Analytics;
using UnityEngine.SceneManagement;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;

public class CheckPointManager : MonoBehaviour
{
    private static int crossedCheckPoints = 0;
    public GameObject analytics;
    private AnalyticManager _analyticManager;
    private static List<Transform> _checkpoints = new List<Transform>();
    private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
    private string levelName;

    void Start()
    {
        _analyticManager = analytics.GetComponent<AnalyticManager>();
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
            RestClient.Post(baseURL + "/alpha/checkpointGraph/" + _analyticManager.GetSessionID().ToString() + '/' + levelName + '/' + _analyticManager.GetPlayID() + '/'+ crossedCheckPoints.ToString() + "/.json", json);
        }
        else
        {
            RestClient.Post(baseURL + "/testing/checkpointGraph/" + _analyticManager.GetSessionID().ToString() + '/' + levelName + '/' +  _analyticManager.GetPlayID() + '/' + crossedCheckPoints.ToString() + "/.json", json);
        }
    }
    
}
