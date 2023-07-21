using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public GameObject target;
    public float targetAngle;

    // Tracks whether an object is ready to interact with 
    private bool _activated;

    private TimeObject animalTimeObject;
    private TimeObject targetTimeObject;

    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        animalTimeObject = GetComponent<TimeObject>();
        targetTimeObject = target.transform.GetComponent<TimeObject>();

        targetPosition = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // If the animal is it should move in the right hand direction
        // TODO: Make the animal move toward the object it needs to interact with not the right or left
        if (_activated)
        {
            //transform.Translate(Vector3.right * Time.deltaTime * this.moveSpeed);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, this.moveSpeed * Time.deltaTime);
        }

        if (animalTimeObject.GetCurrentTimeValue() > 0 && !this._activated)
        {
            if (targetTimeObject.GetCurrentTimeValue() >= 2)
            {
                this._activated = true;
                animalTimeObject.enabled = false;
                targetTimeObject.enabled = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle Palm Tree Collision
        if (collision.gameObject == target)
        {
            collision.transform.Rotate(0f, 0f, targetAngle);
            //collision.gameObject.tag = "Floor"; // Make it tag floor so we can jump after standing on it
            Destroy(this.gameObject);
        }
    }
}
