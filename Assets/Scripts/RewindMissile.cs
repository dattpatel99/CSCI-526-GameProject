using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindMissile : MonoBehaviour
{
    private bool objectRewinding;
    private float missileSpeed;
    private Transform startLocation;

    private Rigidbody2D rb;

    private SpriteRenderer sr;
    public Text counterText;
    private Material yellowOutline;
    private Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        objectRewinding = false;
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        counterText.enabled = false;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        yellowOutline = Resources.Load<Material>("Yellow Outline");
        defaultMaterial = sr.material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objectRewinding)
        {
            rb.velocity = startLocation.right * missileSpeed;
        }
        else
        {
            rb.velocity = startLocation.right * -missileSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collision with enemy");
            Destroy(other.gameObject);
        }

        if (objectRewinding)
        {
            PlayerStatus.isRewinding = false;
        }

        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void Rewind()
    {
        PlayerStatus.isRewinding = true;

        objectRewinding = true;
        sr.color = Color.yellow;
        

        StartCoroutine(RewindDuration());
    }

    // Coroutine to handle rewind duration
    IEnumerator RewindDuration()
    {
        counterText.enabled = true;
        counterText.text = "5";
        yield return new WaitForSeconds(1);
        counterText.text = "4";
        yield return new WaitForSeconds(1);
        counterText.text = "3";
        yield return new WaitForSeconds(1);
        counterText.text = "2";
        yield return new WaitForSeconds(1);
        counterText.text = "1";
        yield return new WaitForSeconds(1);

        PlayerStatus.isRewinding = false;
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
}
