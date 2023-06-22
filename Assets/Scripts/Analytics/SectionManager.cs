using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnalyticsSection;
using Proyecto26;

[ Serializable ]
public class SectionsManager
{
    public int sessionID;
    public List<Section> sections;
}

public class SectionManager : MonoBehaviour
{
    private string baseURL = "https://naturemorph-default-rtdb.firebaseio.com";
    private SectionsManager manager;
    void Start()
    {
        manager = new SectionsManager();
        manager.sessionID = 1;
        manager.sections = new List<Section>();
    }

    public void addSection(Section section)
    {
        manager.sections.Add(section);
 
        string json = JsonUtility.ToJson(manager);
       
    }
}

/*
 *  RestClient.Post(baseURL + "/.json", json);
        Debug.Log("Should have sent data");
 */