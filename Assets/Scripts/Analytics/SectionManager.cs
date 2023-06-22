using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnalyticsSection;
using Proyecto26;

public class AnalyticSectionManager
{
    private static int _sessionID;
    private static List<Section> _sections;

    public AnalyticSectionManager(int id)
    {
        _sessionID = id;
        _sections = new List<Section>();
    }

    public void AddSection(Section aSection)
    {
        _sections.Add(aSection);
    }    
}

public class SectionManager : MonoBehaviour
{
    private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
    private AnalyticSectionManager manager; 
    void Start()
    {
        manager = new AnalyticSectionManager(1);
    }

    public void addSection(Section section)
    {
        manager.AddSection(section);
        string json = JsonUtility.ToJson(manager);
        RestClient.Post(baseURL + "/test" + ".json", json);
    }
}
