using System;
using UnityEngine;

namespace AnalyticsSection
{
public class Section
{
    public string sectionID;
    private DateTime enterTime;
    private DateTime leaveTime;
    private int enterHearts;
    private int leaveHearts;
    private int startTimeBank;
    private int leaveTimeBank;

    public Section(string id, DateTime startTime, int lives, int timeStore)
    {
        this.sectionID = id;
        this.enterHearts = lives;
        this.enterTime = startTime;
        this.startTimeBank = timeStore;
    }
    
    // Getters
    public DateTime getLeaveTime()
    {
        return this.leaveTime;
    }
    public int getLeaveTimeBank()
    {
        return this.leaveTimeBank;
    }
    public int getLeaveHearts()
    {
        return this.leaveHearts;
    }
    // Setters
    public void setLeaveTime(DateTime time)
    {
        this.leaveTime = time;
    }
    public void setLeaveTimeBank(int timeStore)
    {
        this.leaveTimeBank = timeStore;
    }
    public void setLeaveHearts(int hearts)
    {
        this.leaveHearts = hearts;
    }

    public void updateLeaveValues(DateTime time, int timeStore, int hearts)
    {
        this.setLeaveTime(time);
        this.setLeaveHearts(hearts);
        this.setLeaveTimeBank(timeStore);
    }
}

}