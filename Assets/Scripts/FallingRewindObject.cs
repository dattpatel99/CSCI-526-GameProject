using System.Collections;
using UnityEngine;

/// <summary>
/// This handles how falling objects interact with rewind mechanic
/// </summary>
public class FallingRewindObject : MonoBehaviour
{
    private Vector3 startPosition;
    private bool objectRewinding;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        objectRewinding = false;

        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;

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

    // function to be called in the player shoot mechanic script
    public void Rewind()
    {
        PlayerStatus.isRewinding = true;

        rb2d.gravityScale = -1f;
        objectRewinding = true;
        sr.color = Color.yellow;

        StartCoroutine(RewindDuration());
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb2d.gravityScale = 1f;
        }
    }*/

    // How long should the rewind last
    IEnumerator RewindDuration()
    {
        yield return new WaitForSeconds(5);

        PlayerStatus.isRewinding = false;

        rb2d.gravityScale = 1f;
        sr.color = Color.white;

        objectRewinding = false;

        
    }
}
