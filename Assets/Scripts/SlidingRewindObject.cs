using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class SlidingRewindObject : RewindObject
{
    // -ve to move left and +ve to move right
    // Unit is current - start
    private Vector3 startingPoint;
    private Vector3 endPoint;
    public float numberUnitMove;
    public float movementDirection;
    public float slideSpeed;

    // Start is called before the first frame update
    public override void ChildStart()
    {
        startingPoint = this.transform.position;
        endPoint = new Vector3(startingPoint.x + movementDirection * numberUnitMove, startingPoint.y, startingPoint.z);
    }

    void Update()
    {
        if (objectRewinding)
        {
            if (Vector3.Distance(transform.position, endPoint) > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPoint, slideSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, startingPoint) > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, startingPoint, slideSpeed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// The coroutine to maket he log slide pause and slide again.
    /// </summary>
    /// <returns></returns>
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

    public override void Rewind()
    {
        PlayerStatus.isRewinding = true;
        objectRewinding = true;
        sr.color= Color.yellow;
        StartCoroutine(RewindDuration());
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
