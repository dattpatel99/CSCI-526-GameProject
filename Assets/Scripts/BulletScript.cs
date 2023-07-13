using System.Collections;using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BulletScript : MonoBehaviour
{
    public enum MotionPhase
    {
        Steady,
        Rewind,
        Rewinding,
        EndRewind
    }

    private Rigidbody2D rb2d;
    public MotionPhase rewindPhase;
    
    private SpriteRenderer sr;
    public Text counterText;
    private Material yellowOutline;
    private Material defaultMaterial;

    private void Start()
    {
        rewindPhase = MotionPhase.Steady;

        // For color 
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        counterText.enabled = false;
        
        // Get component on start
        rb2d = GetComponent<Rigidbody2D>();
        
        yellowOutline = Resources.Load<Material>("Yellow Outline");
        defaultMaterial = sr.material;
        
    }

    private void FixedUpdate()
    {
        // Update motion when hit with rewind, and rewind ends
        if (rewindPhase == MotionPhase.Rewind || rewindPhase == MotionPhase.EndRewind)
        {
            UpdateMotion();
            if (rewindPhase == MotionPhase.Rewind)
            {
                rewindPhase = MotionPhase.Rewinding;
            }
            else if (rewindPhase == MotionPhase.EndRewind)
            {
                rewindPhase = MotionPhase.Steady;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
        gameObject.SetActive(false);
        // Incase the object is destroyed before the coroutine is finished
        PlayerStatus.isRewinding = false;
        counterText.enabled = false;
        Destroy(gameObject);
    }

    public void Rewind()
    {
        if (rewindPhase == MotionPhase.Steady)
        {
            PlayerStatus.isRewinding = true;
            sr.color = Color.yellow;
            StartCoroutine(RewindDuration());
            rewindPhase = MotionPhase.Rewind;
        }
    }

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
        rewindPhase = MotionPhase.EndRewind;

        counterText.enabled = false;
    }

    private void UpdateMotion()
    {
        rb2d.velocity = -rb2d.velocity;
        rb2d.rotation = -rb2d.rotation;
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
