using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCaseController : TimeObject
{
    public GameObject timeCaseParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        //Update text;
        timeCaseParent.GetComponent<TMP_Text>().text = "Time Needed: " + currentPhase_i + "/" + maxTimePhase_i;
        if ( base.currentPhase_i == base.maxTimePhase_i )
        {
            timeCaseParent.SetActive(false);
        }
    }


    public override void AddTime(int addedTime)
    {
        int newPhase = Mathf.Clamp(base.currentPhase_i + addedTime, 0, base.maxTimePhase_i);
        currentPhase_i = newPhase;
        // currentPhase = Mathf.Clamp(newValue, 0, totalTimePhases);
        // timeObjectSpriteRenderer.sprite=this.GetSprite(currentPhase);
        // TryUpdateShapeToAttachedSprite();
    }

    public override void SubtractTime(int subtractedTime)
    {
        int newPhase = Mathf.Clamp(base.currentPhase_i - subtractedTime, 0, base.maxTimePhase_i);
        currentPhase_i = newPhase;
        // currentPhase = Mathf.Clamp(newValue, 0, totalTimePhases);
        // timeObjectSpriteRenderer.sprite=this.GetSprite(currentPhase);
        // TryUpdateShapeToAttachedSprite();
    }
}
