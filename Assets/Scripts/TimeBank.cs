using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeBank : MonoBehaviour
{
    public TextMeshProUGUI timeBankText;

    private static int timeStored;

    void Start()
    {
        timeStored = 0;
    }

    // Ensure that there is enough time to subtract
    public bool checkSubtract()
    {
        return timeStored > 0;
    }
    
    // Add time to bank
    public void AddTime(int addedTime)
    {
        timeStored += addedTime;
        UpdateTimeDisplay();
    }

    // Remove time from time bank
    public void SubtractTime(int subtractedTime)
    {
        timeStored -= subtractedTime;
        UpdateTimeDisplay();
    }
    
    // Update thte time
    void UpdateTimeDisplay()
    {
        timeBankText.text = "Time Stored: " + timeStored;
    }
}