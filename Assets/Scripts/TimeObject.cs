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

    public void AddTime(int addedTime)
    {
        int newValue = currentTimeValue + addedTime;
        currentTimeValue = Mathf.Clamp(newValue, 0, highestTimeValue);
        UpdateSprite();
    }

    public void SubtractTime(int subtractedTime)
    {
        int newValue = currentTimeValue - subtractedTime;
        currentTimeValue = Mathf.Clamp(newValue, 0, highestTimeValue);
        UpdateSprite();
    }

    void UpdateSprite()
    {
        GetComponent<SpriteRenderer>().sprite = phaseSprites[currentTimeValue];
    }

    public int getCurrentTimeValue()
    {
        return currentTimeValue;
    }
}
