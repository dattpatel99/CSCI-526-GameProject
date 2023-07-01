using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCaseController : TimeObject
{
    public GameObject timeCasePivotPoint;
    public GameObject timeCaseParent;

    public bool openLeft;
    private float rotationSpeed = 50.0f;
    bool opening;

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
            opening = true;
            if (openLeft)
            {
                float rotateZ = rotationSpeed * Time.deltaTime;
                timeCasePivotPoint.transform.Rotate(new Vector3(0f, 0f, rotateZ));
                //Debug.Log(timeCasePivotPoint.transform.localEulerAngles.z);
                if (timeCasePivotPoint.transform.localEulerAngles.z > 90.0f)
                {
                    timeCasePivotPoint.SetActive(false);
                }
            }
            else
            {
                float rotateZ = rotationSpeed * Time.deltaTime;
                timeCasePivotPoint.transform.Rotate(new Vector3(0f, 0f, -rotateZ));
                //Debug.Log(timeCasePivotPoint.transform.localEulerAngles.z);
                if (timeCasePivotPoint.transform.localEulerAngles.z < 270 && timeCasePivotPoint.transform.localEulerAngles.z > 0)
                {
                    timeCasePivotPoint.SetActive(false);
                }
            }
        }
    }


    public override void AddTime(int addedTime)
    {
        int newPhase = Mathf.Clamp(base.currentPhase_i + addedTime, 0, base.maxTimePhase_i);
        currentPhase_i = newPhase;
        StartCoroutine(flashGreenText());
    }

    public override void SubtractTime(int subtractedTime)
    {
        int newPhase = Mathf.Clamp(base.currentPhase_i - subtractedTime, 0, base.maxTimePhase_i);
        currentPhase_i = newPhase;
        StartCoroutine(flashRedText());
    }


    IEnumerator flashGreenText()
    {
        timeCaseParent.GetComponent<TMP_Text>().color = Color.white;
        timeCaseParent.GetComponent<TMP_Text>().color = Color.green;
        yield return new WaitForSeconds(0.5f);
        timeCaseParent.GetComponent<TMP_Text>().color = Color.white;
    }

    IEnumerator flashRedText()
    {
        timeCaseParent.GetComponent<TMP_Text>().color = Color.white;
        timeCaseParent.GetComponent<TMP_Text>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        timeCaseParent.GetComponent<TMP_Text>().color = Color.white;
    }

    public bool isOpening()
    {
        return opening;
    }
}