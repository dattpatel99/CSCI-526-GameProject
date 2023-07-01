using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BossOne : MonoBehaviour
{
    public float speed;
    private List<Transform> ShieldGroups;
    private List<Transform> Shields;
    public bool group1Active;
    private float timer;
    public float SwapTime;
    private int numActiveShields;
    void Start()
    {
        this.timer = 0f;
        ShieldGroups = new List<Transform>();
        Shields = new List<Transform>();
        ShieldGroups.Add(getChildren(gameObject, "Group1")); 
        ShieldGroups.Add(getChildren(gameObject, "Group2")); 
        ShieldGroups[1].gameObject.SetActive(false); 
        group1Active = true;
        foreach (var group in ShieldGroups) 
        { 
            Shields = getAllChildren(group.transform, Shields); 
        } 
        numActiveShields = Shields.Count;
    }

    void Update()
    {
        foreach (var group in ShieldGroups)
        {
            Shields = getAllChildren(group.transform, Shields);
        }
        numActiveShields = Shields.Count;

        
        // Swappin Script
        if (timer >= SwapTime)
        {
            SwapShieldGroups();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
        
        
    }

    private void SpeedUp()
    {
        if (this.numActiveShields == 3)
        {
            this.speed = 3 * this.speed;
        }
    }

    private List<Transform> getAllChildren(Transform parentTrans, List<Transform> shields)
    {
        foreach (Transform childTrans in parentTrans)
        {
            GameObject child = childTrans.gameObject;
            if (child.activeSelf)
            {
                shields.Add(childTrans);
            }
        }
        return shields;
    } 

    private Transform getChildren(GameObject parent, string childName)
    {
        Transform child = parent.transform.Find(childName);
        if (child != null)
        {
            return child;
        }
        else
        {
            return null;
        }
    }

    private void SwapShieldGroups()
    {
        if (this.group1Active)
        {
            ShieldGroups[0].gameObject.SetActive(false);
            ShieldGroups[1].gameObject.SetActive(true);
            group1Active = false;
        }
        else
        {
            ShieldGroups[0].gameObject.SetActive(true);
            ShieldGroups[1].gameObject.SetActive(false);
            group1Active = true;
        }
    }
}
