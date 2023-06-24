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
    private long sessionID;
    private static int crossSections = 0;
    private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
    private string levelName;
    private List<Transform> _sections = new List<Transform>();

    void Start()
    {
        sessionID = AnalyticsSessionInfo.sessionId;
        Transform parentTransform = gameObject.transform;
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            _sections.Add(parentTransform.GetChild(i).gameObject.transform);
        }
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
            RestClient.Post(baseURL + "/alpha/sectionGraph/" + sessionID.ToString() + '/' + levelName + '/' + crossSections.ToString() + "/.json", json);
        }
        else
        {
            RestClient.Post(baseURL + "/testing/sectionGraph/" + sessionID.ToString() + '/' + levelName + '/' + crossSections.ToString() + "/.json", json);
        }
    }
}
