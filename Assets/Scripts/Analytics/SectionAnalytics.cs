using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/**
 * This is the information that can be stored for each checkpoint
 */
namespace Analytics{
    public class SectionAnalytics
    {
        public string sectionID;
        public float enterTime;
        public float leaveTime;
        public int enterHearts;
        public int leaveHearts;
        public int startTimeBank;
        public int leaveTimeBank;
        public int numberPuzzles;
        public float timeSpent;
        
        public SectionAnalytics(string sectionName, float timeEnter, PlayerController control, TimeBank bank, int puzzleNum)
        {
            this.sectionID = sectionName;
            this.enterHearts = control.getHP().GetHP();
            this.enterTime = timeEnter;
            this.startTimeBank = bank.GetTimeStore();
            this.numberPuzzles = puzzleNum;
        }

        public void UpdateLeaving(float timeLeave,PlayerController control, TimeBank bank)
        {
            this.leaveHearts = control.getHP().GetHP();
            this.leaveTime = timeLeave;
            this.leaveTimeBank = bank.GetTimeStore();
            this.timeSpent = this.leaveTime - this.enterTime;
        }
    }
}