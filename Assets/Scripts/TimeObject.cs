using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public int startingTimeValue;
    public int highestTimeValue;

    public List<Sprite> phaseSprites;

    int currentTimeValue;

    void Start()
    {
        currentTimeValue = Mathf.Clamp(startingTimeValue, 0, highestTimeValue);
        GetComponent<SpriteRenderer>().sprite = phaseSprites[currentTimeValue];
    }

    void AddTime(int addedTime)
    {
        int newValue = currentTimeValue + addedTime;
        currentTimeValue = Mathf.Clamp(newValue, 0, highestTimeValue);
    }

    void SubtractTime(int subtractedTime)
    {
        int newValue = currentTimeValue - subtractedTime;
        currentTimeValue = Mathf.Clamp(newValue, 0, highestTimeValue);
    }
}
