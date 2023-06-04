using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMechanic : MonoBehaviour
{
    public GameObject nozzle;
    public GameObject player;

    private bool shoot = false;

    public float laserLength = 10f;
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
                if (hit.collider.gameObject.tag == "TimeObject" && hit.collider.gameObject.GetComponent<TimeObject>().getCurrentTimeValue() > 0)
                {
                    hit.collider.gameObject.GetComponent<TimeObject>().SubtractTime(1);
                    player.GetComponent<TimeBank>().AddTime(1);
                }

                Debug.Log ("Target Hit: " + hit.collider.gameObject.name);
            }
        }
    }
}
