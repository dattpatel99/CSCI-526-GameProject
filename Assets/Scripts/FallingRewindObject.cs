using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRewindObject : MonoBehaviour
{
    private Vector3 startPosition;
    private bool objectRewinding;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        objectRewinding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= startPosition.y && objectRewinding)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

    // function to be called in the player shoot mechanic script
    public void Rewind()
    {
        PlayerStatus.isRewinding = true;

        GetComponent<Rigidbody2D>().gravityScale = -1f;
        objectRewinding = true;

        StartCoroutine(RewindDuration());
    }

    IEnumerator RewindDuration()
    {
        yield return new WaitForSeconds(5);

        PlayerStatus.isRewinding = false;

        GetComponent<Rigidbody2D>().gravityScale = 1f;

        objectRewinding = false;
    }
}
