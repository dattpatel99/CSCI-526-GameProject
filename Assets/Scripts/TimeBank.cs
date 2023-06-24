using UnityEngine;
using UnityEngine.UI;

/*
 * This class handles the players stored time and value updating
 */
public class TimeBank : MonoBehaviour
{
    public Text timeBankText;
    private static int _timeStored;
    private int _maximumTimeStored = 100000000; // TODO: In case we implement a maximum
    private int _minimumTimeStored = 0;

    void Start()
    {
        _timeStored = this._minimumTimeStored;
    }

    public int GetTimeStore()
    {
        return _timeStored;
    }

    // Ensure that there is enough time to subtract
    public bool CheckSubtract()
    {
        return _timeStored > this._minimumTimeStored;
    }

    public bool CheckAddition()
    {
        return _timeStored < this._maximumTimeStored;
    }

    // Add time to bank
    public void AddTime(int addedTime)
    {
        _timeStored += addedTime;
        UpdateTimeDisplay();
    }

    // Remove time from time bank
    public void SubtractTime(int subtractedTime)
    {
        _timeStored -= subtractedTime;
        UpdateTimeDisplay();

    }
    
    // Update the time
    void UpdateTimeDisplay()
    {
        timeBankText.text = "Time Stored: " + _timeStored;
    }
}