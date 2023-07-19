using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This handles how falling objects interact with rewind mechanic
/// </summary>
public class FallingRewindObject : RewindObject
{
    public override void ChildStart()
    {
        startPosition = this.transform.position;
        objectRewinding = false;
        rb2d.gravityScale = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        // Make the object go up in position if rewinding
        if (transform.position.y >= startPosition.y && objectRewinding)
        {
            rb2d.velocity = Vector3.zero;
            rb2d.gravityScale = 0f;
        }
    }

    public override void Rewind()
    {

        PlayerStatus.isRewinding = true;
        //Debug.Log("Set is rewinding to true??? " + PlayerStatus.isRewinding);

        rb2d.gravityScale = -1f;
        objectRewinding = true;
        sr.color = Color.yellow;

        StartCoroutine(RewindDuration());
    }

    protected override IEnumerator RewindDuration()
    {
        counterText.enabled = true;
        for (int i = rewindedDuration; i >= 1; i--)
        {
            counterText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        PlayerStatus.isRewinding = false;

        rb2d.gravityScale = 1f;
        sr.color = Color.white;

        objectRewinding = false;

        counterText.enabled = false;
    }
}
