using System.Collections;using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BulletScript : RewindObject
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

    public override void ChildStart()
    {
        rewindPhase = MotionPhase.Steady;       
        // Get component on start
        rb2d = GetComponent<Rigidbody2D>();
        
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

    public override void Rewind()
    {
        if (rewindPhase == MotionPhase.Steady)
        {
            PlayerStatus.isRewinding = true;
            sr.color = Color.yellow;
            StartCoroutine(RewindDuration());
            rewindPhase = MotionPhase.Rewind;
        }
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
        sr.color = Color.white;
        rewindPhase = MotionPhase.EndRewind;

        counterText.enabled = false;
    }

    private void UpdateMotion()
    {
        rb2d.velocity = -rb2d.velocity;
        rb2d.rotation = -rb2d.rotation;
    }
}
