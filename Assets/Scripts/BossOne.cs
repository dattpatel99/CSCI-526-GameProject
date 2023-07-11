using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BossOne : MonoBehaviour
{
    // Enemy Movement 
    public float speed = -2.0f;
    private float center;
    private float leftPoint;
    private float rightPoint;
    public float range;
    private Rigidbody2D rb2d;
    private bool turned;

    // Enemy Shields
    private List<Transform> ShieldGroups;
    private List<Transform> Shields;
    public bool group1Active;
    private float timer;
    public float SwapTime;
    private int numActiveShields;

    private float health; 


    void Start()
    {
        // Movement
        this.timer = 0f;
        center = transform.position.x;
        leftPoint = center - range;
        rightPoint = center + range;
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        // Shields
        ShieldGroups = new List<Transform>();
        Shields = new List<Transform>();
        ShieldGroups.Add(getChildrenByName(gameObject, "Group1")); 
        ShieldGroups.Add(getChildrenByName(gameObject, "Group2")); 
        ShieldGroups[1].gameObject.SetActive(false); 
        group1Active = true;
        UpdateShields();
        
        // Health Update
        health = 10;
    }

    void Update()
    {
        UpdateShields();
        EnemyLevelUp();
        ShieldSwap();
        BossMovement();
        if (checkDeath())
        {
            Dead();
        }
    }

    public void BossMovement()
    {
        // Movement Scripts
        // If at edge of left point and haven't turned turn
        if (transform.position.x < leftPoint && !turned)
        {
            speed = -speed;
            turned = true;
        }
        // If at edge of right point and haven't turned turn
        else if (transform.position.x > rightPoint && !turned)
        {
            speed = -speed;
            turned = true;
        }
        else if(transform.position.x > leftPoint && transform.position.x < rightPoint && turned)
        {
            turned = false;
        }
        rb2d.velocity = new Vector2(speed, 0);
    }

    private void ShieldSwap()
    {
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

    private void EnemyLevelUp()
    {
        // Making it harder
        if (this.getHealth() == 3)
        {
            this.speed = 3 * this.speed;
        }
    }

    private void UpdateShields()
    {
        // Update Shields
        foreach (var group in ShieldGroups)
        {
            Shields = getAllChildren(group.transform, Shields);
        }
        numActiveShields = Shields.Count;
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

    private Transform getChildrenByName(GameObject parent, string childName)
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

    public void Damage(int damageValue)
    {
        health -= damageValue;
    }

    public float getHealth()
    {
        return health;
    }

    private void Dead()
    {
        gameObject.SetActive(false);
    }

    private bool checkDeath()
    {
        return health <= 0;
    }
    
}
