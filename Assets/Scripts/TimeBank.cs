using System.Collections;
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
        timeBankText.color = Color.white;
    }

    public int GetTimeStore()
    {
        return _timeStored;
    }

    // Ensure that there is enough time to subtract
    public bool CheckSubtract()
    {
        bool eval = _timeStored - 1 >= this._minimumTimeStored;

        if (!eval)
        {
            StartCoroutine(DisfunctionalFlash());
        }

        return eval;
    }

    public bool CheckAddition()
    {
        return _timeStored < this._maximumTimeStored;
    }

    // Add time to bank
    public void AddTime(int addedTime)
    {
        _timeStored += addedTime;
        StartCoroutine(UpdateTimeAddedDisplay());
    }

    // Remove time from time bank
    public void SubtractTime(int subtractedTime)
    {
        _timeStored -= subtractedTime;
        StartCoroutine(UpdateTimeDecreasedDisplay());
    }

    public void AlterTimeStored(int deltaTime)
    {
        _timeStored += deltaTime;
        if (deltaTime < 0 )
        {
            StartCoroutine(UpdateTimeDecreasedDisplay());
        }
        else
        {
            StartCoroutine(UpdateTimeAddedDisplay());
        }
    }
    
    // Update the time
    void UpdateTimeDisplay()
    {
        timeBankText.text = "Time Stored: " + _timeStored;
    }

    IEnumerator UpdateTimeAddedDisplay()
    {
        timeBankText.color = Color.white;
        timeBankText.color = Color.green;
        UpdateTimeDisplay();
        yield return new WaitForSeconds(0.2f);
        timeBankText.color = Color.white;
    }

    IEnumerator UpdateTimeDecreasedDisplay()
    {
        timeBankText.color = Color.white;
        timeBankText.color = Color.red;
        UpdateTimeDisplay();
        yield return new WaitForSeconds(0.2f);
        timeBankText.color = Color.white;
    }

    IEnumerator DisfunctionalFlash ()
    {
        timeBankText.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        timeBankText.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        timeBankText.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        timeBankText.color = Color.white;
    }
}