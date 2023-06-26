using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Analytics;
using Unity.VisualScripting;

public class Section : MonoBehaviour
{
    private SectionAnalytics sectionData;
    private SectionManager manager;
    private float runtime;
    public int puzzleNum;
    // Start is called before the first frame update
    void Start()
    {
        puzzleNum = 0;
        runtime = 0f;
        manager = gameObject.transform.parent.gameObject.GetComponent<SectionManager>();
    }

    private void Update()
    {
        runtime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Set the player controller and bank
        if (other.GameObject().name == "Player")
        {
            sectionData = new SectionAnalytics(gameObject.name, runtime, other.gameObject.GetComponent<PlayerController>(), other.gameObject.GetComponent<TimeBank>(), puzzleNum);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GameObject().name == "Player")
        {
            sectionData.UpdateLeaving(runtime,other.gameObject.GetComponent<PlayerController>(), other.gameObject.GetComponent<TimeBank>());
            manager.SendData(sectionData);
        }
    }
}
