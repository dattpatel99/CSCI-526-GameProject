using System;
using UnityEngine;

namespace Analytics
{
    public class Damaged
    {
        public bool died;
        public string damagingObject;
        public int prevHearts;
        public int afterHears;
        public int x;
        public int y;
        public string sectionName;

        public Damaged(bool dead, string objName, int prev, int cur, int locX, int locY, string sectionName)
        {
            this.sectionName = sectionName;
            this.died = dead;
            this.damagingObject = objName;
            this.prevHearts = prev;
            this.afterHears = cur;
            this.x = locX;
            this.y = locY;
        }
    }
    
}