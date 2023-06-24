using System;
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
        public string level;
        public string userId;
        public bool finished;
        public float timeToFinish;
        public int endLives;
        public int startingTimeStored;
        public int endingTimeStored;

        public AnalyticGameSession(long sesID, string uID)
        { 
            this.userId = uID;
            this.sessionId = sesID;
            this.finished = false;
            this.startingTimeStored = 0;
        }

        /**
         * This updates any remaining data that depends on the user reach  status to post the data
         */
        public void DataUpdate(bool fini, string level, float runTime, int livesRemain, int timeStored)
        {
            this.finished = fini;
            this.level = level;
            this.timeToFinish = runTime;
            this.endLives = livesRemain;
            this.endingTimeStored = timeStored;
        }
        
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}