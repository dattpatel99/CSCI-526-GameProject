using System;
using System.Collections.Generic;
using Analytics;
using UnityEngine.SceneManagement;
using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class CheckPointManager : MonoBehaviour
{
    private static int crossedCheckPoints = 0;
    public GameObject analytics;
    private AnalyticManager _analyticManager;
    private static List<Transform> _checkpoints = new List<Transform>();
    private string levelName;
    private float rt;

    void Start()
    {
        rt = 0;
        _analyticManager = analytics.GetComponent<AnalyticManager>();
        Transform parentTransform = gameObject.transform;
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            _checkpoints.Add(parentTransform.GetChild(i).gameObject.transform);
        }
    }

    private void Update()
    {
        rt += Time.deltaTime;
    }

    public void SendData(CheckPointAnalytics crossedPoint)
    {
        crossedPoint.UpdateTime(rt);
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
            RestClient.Put($"{_analyticManager.getDeployLink()}/CheckPointGraph/{_analyticManager.GetSessionID().ToString()}_{_analyticManager.GetPlayID()}_{levelName}/{crossedCheckPoints}/.json", json);
        }
        else
        {
            RestClient.Put($"{_analyticManager.getEditLink()}/CheckPointGraph/{_analyticManager.GetSessionID().ToString()}_{_analyticManager.GetPlayID()}_{levelName}/{crossedCheckPoints}/.json", json);
        }
    }
    
}
