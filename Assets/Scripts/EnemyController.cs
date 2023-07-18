using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public float range;
    //public float speed;

    //private Vector3 centerLocation;
    //private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        //centerLocation = transform.position;
        //velocity = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.position.x > centerLocation.x + range)
        //{
        //    velocity = Vector3.left;
        //}
        //else if (transform.position.x < centerLocation.x - range)
        //{
        //    velocity = Vector3.right;
        //}

        //transform.Translate(speed * velocity * Time.deltaTime);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    velocity = -1f * velocity;
    //}

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
