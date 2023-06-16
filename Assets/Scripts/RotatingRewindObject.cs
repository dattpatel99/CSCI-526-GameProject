using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingRewindObject : MonoBehaviour
{
    public float rotationSpeed;
    private bool objectRewinding;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        objectRewinding = false;
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectRewinding)
        {
            transform.Rotate(new Vector3(0f, 0f, -1f * rotationSpeed * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0f, 0f, rotationSpeed * Time.deltaTime));
        }
    }

    public void Rewind()
    {
        PlayerStatus.isRewinding = true;

        objectRewinding = true;
        sr.color = Color.yellow;

        StartCoroutine(RewindDuration());
    }

    IEnumerator RewindDuration()
    {
        yield return new WaitForSeconds(5);

        PlayerStatus.isRewinding = false;
        sr.color = Color.white;
        objectRewinding = false;
    }
}
