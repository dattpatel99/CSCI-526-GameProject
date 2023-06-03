using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMechanic : MonoBehaviour
{
    public GameObject nozzle;

    private bool shoot = false;

    public float laserLength = 1f;
    // Update is called once per frame
    void Update()
    {
        shoot = Input.GetButtonDown("Fire1");
        if (shoot)
        {
            Debug.DrawRay(nozzle.transform.position, transform.TransformDirection(Vector2.right)*laserLength,Color.red);
            RaycastHit2D hit = Physics2D.Raycast(nozzle.transform.position, transform.TransformDirection(Vector2.right), laserLength);
            if(hit.collider != null)
            {
                // Alter the Object
                Debug.Log ("Target Hit: " + hit.collider.gameObject.name);
            }
        }
    }
}
