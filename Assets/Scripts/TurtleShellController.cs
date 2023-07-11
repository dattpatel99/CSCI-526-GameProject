using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleShellController : MonoBehaviour
{
    public GameObject turtle;

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
        if (collision.gameObject.name.Contains("Stalactite"))
        {
            turtle.SetActive(false); // Turtle dead
        }
    }
}
