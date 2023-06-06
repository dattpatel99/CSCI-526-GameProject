using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform eyes;
    public float wolfSight = 10.0f;
    private bool activated;
    public LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        // Check if there is a level 2 tree or boulder on the screen to the right of the animal
        // move to the right, once it hits one of those, destroy it (the boulder) or knock it down (the level 2 tree)
    }

    void Update()
    {
        if (activated)
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            StartCoroutine(UpdateLaserPosition());
        }
        
        if (GetComponent<TimeObject>().getCurrentTimeValue() > 0 && !activated)
        {
            RaycastHit2D hit = Physics2D.Raycast(eyes.position, transform.TransformDirection(Vector2.right), wolfSight);
            // Draw Wolf's vision
            
            // Line Color Gradient
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Boulder"))
                {
                    activated = true;
                    GetComponent<TimeObject>().enabled = false;
                }
                else if (hit.collider.gameObject.name == "Tree" && hit.collider.gameObject.GetComponent<TimeObject>().getCurrentTimeValue() > 1)
                {
                    activated = true;
                    GetComponent<TimeObject>().enabled = false;
                }

                SetLineColor(line);
                line.SetPosition(0, eyes.position);
                line.SetPosition(1, hit.point);
            }
            else
            {
                SetLineColor(line);
                line.SetPosition(0, eyes.position);
                line.SetPosition(1, eyes.position + eyes.right * wolfSight);
            }
        }
    }

    void SetLineColor(LineRenderer line)
    {
        line.startColor = Color.green;
        line.endColor = Color.green;
    }

    IEnumerator UpdateLaserPosition()
    {
        yield return new WaitForSeconds(0.0f);
        line.SetPosition(0, eyes.position);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
            collision.gameObject.GetComponent<TimeObject>().enabled = false;
            collision.gameObject.tag = "Floor";
            Destroy(this.gameObject);
        }
    }
}
