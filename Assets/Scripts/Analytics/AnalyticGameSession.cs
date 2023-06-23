using System;
using UnityEngine;

[Serializable]
public class AnalyticGameSession
{
    public string sessionID;
    public string level;
    public bool finished;
    public float timeToFinish;
    public int livesLost;
    public int startingTimeStored;
    public int endingTimeStored;

    public override string ToString()
    {
        return JsonUtility.ToJson(this, true);
    }
}