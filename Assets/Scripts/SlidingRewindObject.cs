using System.Collections;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SlidingRewindObject : MonoBehaviour
{
    // -ve to move left and +ve to move right
    // Unit is current - start
    public float numberUnitMove;
    public float movementDirection;
    public float slideSpeed;
    private Vector3 startingPoint;
    private Vector3 endPoint;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        
        startingPoint = this.transform.position;
        endPoint = new Vector3(startingPoint.x + movementDirection * numberUnitMove, startingPoint.y, startingPoint.z);
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
    }

    /// <summary>
    /// The coroutine to maket he log slide pause and slide again.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SlideRewind()
    {
        float elapsedTime = 0f;

        while (elapsedTime < slideSpeed)
        {
            transform.position = Vector3.Lerp(startingPoint, endPoint, elapsedTime / slideSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = endPoint;
        elapsedTime = slideSpeed + 1;

        yield return new WaitForSeconds(2);
        elapsedTime = 0;
        while (elapsedTime  < slideSpeed)
        {
            transform.position = Vector3.Lerp(endPoint, startingPoint, elapsedTime/ slideSpeed);
            elapsedTime  += Time.deltaTime;
            yield return null;
        }

        transform.position = startingPoint;
        elapsedTime = slideSpeed + 1;
        PlayerStatus.isRewinding = false;
        sr.color = Color.white;
    }
    
    public void Rewind()
    {
        PlayerStatus.isRewinding = true;
        sr.color= Color.yellow;
        StartCoroutine(SlideRewind());
    }
    
}
