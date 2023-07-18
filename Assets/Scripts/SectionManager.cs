using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Analytics;
using UnityEngine.SceneManagement;
using Proyecto26;
using Newtonsoft.Json;
using UnityEngine.Analytics;

public class SectionManager : MonoBehaviour
{
    private static int crossSections = 0;
    public GameObject analytics;
    private AnalyticManager _analyticManager;
    private string levelName;
    private List<Transform> _sections = new List<Transform>();

    void Start()
    {
        _analyticManager = analytics.GetComponent<AnalyticManager>();
        Transform parentTransform = gameObject.transform;
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            _sections.Add(parentTransform.GetChild(i).gameObject.transform);
        }
    }

    public void setCurrentSection(string secName)
    {
        _analyticManager.setCurrentSection(secName);
    }
    
    public string getCurrentSection()
    {
        return _analyticManager.getCurrentSection();
    }

    public int GetNumberDeaths()
    {
        return _analyticManager.GetNumberDeaths();
    }
    
    public void SendData(SectionAnalytics sectionPoint)
    {
        crossSections++;
        StoreData(JsonConvert.SerializeObject(sectionPoint));
    }

    public void resetCrossedSections()
    {
        crossSections = 0;
    }
    
    private void StoreData(string json)
    {
        levelName = SceneManager.GetActiveScene().name;
        
        // Implements sending data when on WebGL Build
        if (!Application.isEditor)
        {
            RestClient.Put($"{_analyticManager.getDeployLink()}/SectionGraph/{_analyticManager.GetSessionID().ToString()}_{_analyticManager.GetPlayID()}_{levelName}/{crossSections}/.json", json);
        }
        else
        {
            RestClient.Put($"{_analyticManager.getEditLink()}/SectionGraph/{_analyticManager.GetSessionID().ToString()}_{_analyticManager.GetPlayID()}_{levelName}/{crossSections}/.json", json);
        }
    }
}
