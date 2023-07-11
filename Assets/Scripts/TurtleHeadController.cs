using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleHeadController : MonoBehaviour
{

    private bool collidedWithLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        collidedWithLevel = true;
    }

    public bool isColliding()
    {
        return collidedWithLevel;
    }

    public void resetCollidingStatus()
    {
        collidedWithLevel = false;
    }
}
