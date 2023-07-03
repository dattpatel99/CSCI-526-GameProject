using System;
using UnityEditor.Experimental.GraphView;
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

        public Damaged(bool dead, string objName, int prev, int cur, int locX, int locY)
        {
            this.died = dead;
            this.damagingObject = objName;
            this.prevHearts = prev;
            this.afterHears = cur;
            this.x = locX;
            this.y = locY;
        }
    }
    
}