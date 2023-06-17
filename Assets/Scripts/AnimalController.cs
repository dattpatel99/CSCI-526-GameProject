using System.Collections;
using UnityEngine;

/*
 * This class handles the Animal behavior (AKA Wolf for now)
 */
public class AnimalController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float animalSight = 10.0f;
    public Transform eyes;
    public LineRenderer line;
    
    // Tracks whether an object is ready to interact with 
    private bool _activated;

    // Start is called before the first frame update
    void Start()
    {
        _activated = false;
        this.line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // If the animal is it should move in the right hand direction
        // TODO: Make the animal move toward the object it needs to interact with not the right or left
        if (_activated)
        {
            transform.Translate(Vector3.right * Time.deltaTime * this.moveSpeed);
            StartCoroutine(UpdateLaserPosition());
        }
        
        if (GetComponent<TimeObject>().GetCurrentTimeValue() > 0 && !this._activated)
        {
            Vector3 eyePostition = eyes.position;
            
            // Draw Wolf's vision
            RaycastHit2D hit = Physics2D.Raycast(eyePostition, transform.TransformDirection(Vector2.right), this.animalSight);

            // Line Color Gradient
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Boulder"))
                {
                    this._activated = true;
                    GetComponent<TimeObject>().enabled = false;
                }
                else if (hit.collider.gameObject.name == "Tree" && hit.collider.gameObject.GetComponent<TimeObject>().GetCurrentTimeValue() > 1)
                {
                    this._activated = true;
                    GetComponent<TimeObject>().enabled = false;
                }

                SetLineColor(this.line, Color.green);
                this.line.SetPosition(0, eyePostition);
                this.line.SetPosition(1, hit.point);
            }
            else
            {
                SetLineColor(this.line, Color.green);
                this.line.SetPosition(0, eyePostition);
                this.line.SetPosition(1, eyePostition + eyes.right * this.animalSight);
            }
        }
    }

    // Set the color for the animal's vision
    void SetLineColor(LineRenderer lineRendererObject, Color color)
    {
        lineRendererObject.startColor = color;
        lineRendererObject.endColor = color;
    }

    // Make the laser legnth change based on what it hits first
    IEnumerator UpdateLaserPosition()
    {
        yield return new WaitForSeconds(0.0f);
        this.line.SetPosition(0, eyes.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    { 
        /*
         * obj1: rotation
         * obj2: shift
         * etc
         */
        
        // Handle Boulder Collision
        if (collision.gameObject.CompareTag("Boulder"))
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        // Handle Tree Collision
        else if (collision.gameObject.name == "Tree")
        {
            collision.transform.parent.Rotate(0f, 0f, -90f);
            collision.gameObject.GetComponent<TimeObject>().enabled = false; // Disable the time object script
            collision.gameObject.tag = "Floor"; // Make it tag floor so we can jump after standing on it
            Destroy(this.gameObject);
        }
    }
}
