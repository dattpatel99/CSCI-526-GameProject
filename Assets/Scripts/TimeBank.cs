using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeBank : MonoBehaviour
{
    public TextMeshProUGUI timeBankText;

    int timeStored;

    void AddTime(int addedTime)
    {
        timeStored += addedTime;
        UpdateTimeDisplay();
    }

    void SubtractTime(int subtractedTime)
    {
        timeStored -= subtractedTime;
        UpdateTimeDisplay();
    }

    void UpdateTimeDisplay()
    {
        timeBankText.text = "Time Stored: " + timeStored;
    }
}
