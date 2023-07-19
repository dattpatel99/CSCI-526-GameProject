using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This handles how falling objects interact with rewind mechanic
/// </summary>
public class FallingRewindObject : MonoBehaviour
{
    public Text counterText;
    public int rewindedDuration = 5;

    private Vector3 startPosition;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private bool objectRewinding;

    private Material yellowOutline;
    private Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
        objectRewinding = false;

        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        counterText.enabled = false;

        rb2d.gravityScale = 1f;

        yellowOutline = Resources.Load<Material>("Yellow Outline");
        defaultMaterial = sr.material;
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
        //Debug.Log("Set is rewinding to true??? " + PlayerStatus.isRewinding);

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
        counterText.enabled = true;
        for ( int i = rewindedDuration; i >= 1; i-- )
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

    private void OnMouseEnter()
    {
        sr.material = yellowOutline;
    }

    private void OnMouseExit()
    {
        sr.material = defaultMaterial;
    }

    public int getRewindDuration()
    {
        return rewindedDuration;
    }
}
