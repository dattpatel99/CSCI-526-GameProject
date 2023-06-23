using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/*
 * This is the main game session
 * This analysis is used to store all analysis of the game
 */
namespace Analytics{
    [Serializable]
    public class AnalyticGameSession
    {
        public long sessionId;
        public string userId;
        public bool finished;
        public float timeToFinish;
        public int endLives;
        public int startingTimeStored;
        public int endingTimeStored;
        public Dictionary<string, CheckPointAnalytics> checkPointGraph;

        public AnalyticGameSession(long sesID, string uID)
        { 
            this.userId = uID;
            this.sessionId = sesID;
            this.checkPointGraph = new Dictionary<string, CheckPointAnalytics>();
            this.finished = false;
            this.startingTimeStored = 0;
        }

        /**
         * This updates any remaining data that depends on the user reach  status to post the data
         */
        public void DataUpdate(bool fini, float runTime, int livesRemain, int timeStored)
        {
            this.finished = fini;
            this.timeToFinish = runTime;
            this.endLives = livesRemain;
            this.endingTimeStored = timeStored;
        }
        
        /**
         * This is used to add a new checpointObject
         */
        public void AddCheckPoint(CheckPointAnalytics checkPoint)
        {
            this.checkPointGraph.Add("CrossedCheckPoint" + (this.checkPointGraph.Count + 1), checkPoint);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}