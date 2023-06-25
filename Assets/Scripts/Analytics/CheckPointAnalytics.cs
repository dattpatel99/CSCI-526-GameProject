﻿using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * This is the information that will be stored about a checkpoint
 */
namespace Analytics {
    public class CheckPointAnalytics
    {
        public string checkpointID;
        public DateTime crossTime;
        public int curHeart;
        public int curTimeBank;
        public int curPlayerAge;
        public CheckPointAnalytics(string checkPointName, PlayerController playerController, TimeBank bank)
        {
            this.checkpointID = checkPointName;
            this.crossTime = DateTime.Now;
            this.curHeart = playerController.getHP().GetHP();
            this.curPlayerAge = playerController.getAge();
            this.curTimeBank = bank.GetTimeStore();
        }
    }
}