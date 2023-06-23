using System;
using System.Collections.Generic;
using UnityEngine;


[ Serializable ]
public class Section
{
    public string sectionID;
    public DateTime enterTime;
    public DateTime leaveTime;
    public int enterHearts;
    public int leaveHearts;
    public int startTimeBank;
    public int leaveTimeBank;
    public Dictionary<string, int> sectionPuzzles;
}