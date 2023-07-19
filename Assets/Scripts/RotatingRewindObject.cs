using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles rewind objects that rotate on rewing
/// </summary>
public class RotatingRewindObject : RewindObject
{
    public float rotationSpeed;

    public override void ChildStart(){}
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
    
    // Implement rewind
    public override void Rewind()
    {
        PlayerStatus.isRewinding = true;

        objectRewinding = true;
        sr.color = Color.yellow;

        StartCoroutine(RewindDuration());
    }

    // Coroutine to handle rewind duration
    protected override IEnumerator RewindDuration()
    {
        counterText.enabled = true;
        for (int i = rewindedDuration; i >= 1; i--)
        {
            counterText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        PlayerStatus.isRewinding = false;
        sr.color = Color.white;
        objectRewinding = false;

        counterText.enabled = false;
    }
}
