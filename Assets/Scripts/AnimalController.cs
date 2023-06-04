using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Transform eyes;
    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<TimeObject>().getCurrentTimeValue() > 0 && !activated)
        {
            RaycastHit2D hit = Physics2D.Raycast(eyes.position, transform.TransformDirection(Vector2.right), 10f);
            Debug.DrawRay(eyes.position, 10f * transform.TransformDirection(Vector2.right), Color.red);

            if (hit.collider != null)
            {
                Debug.Log("Hit");

                if (hit.collider.gameObject.CompareTag("Boulder"))
                {
                    Debug.Log("Boulder seen by animal");
                    activated = true;
                    GetComponent<TimeObject>().enabled = false;
                }
                else if (hit.collider.gameObject.name == "Tree" && hit.collider.gameObject.GetComponent<TimeObject>().getCurrentTimeValue() > 1)
                {
                    Debug.Log("Tree seen by animal");
                    activated = true;
                    GetComponent<TimeObject>().enabled = false;
                }
            }
        }
        // Check if there is a level 2 tree or boulder on the screen to the right of the animal
        // move to the right, once it hits one of those, destroy it (the boulder) or knock it down (the level 2 tree)
    }

    void Update()
    {
        if (activated)
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boulder"))
        {
            Debug.Log("Hit Boulder");
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.name == "Tree")
        {
            Debug.Log("Hit Tree");
            collision.transform.parent.Rotate(0f, 0f, -90f);
            collision.gameObject.GetComponent<TimeObject>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}
