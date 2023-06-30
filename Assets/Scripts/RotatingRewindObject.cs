using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles rewind objects that rotate on rewing
/// </summary>
public class RotatingRewindObject : MonoBehaviour
{
    public float rotationSpeed;
    private bool objectRewinding;
    private SpriteRenderer sr;
    public Text counterText;

    // Start is called before the first frame update
    void Start()
    {
        objectRewinding = false;
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        counterText.enabled = false;
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
    
    // Implement rewind
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
}
