using System;
using System.Collections;
using UnityEngine;

public class ShootMechanic : MonoBehaviour
{
    // Attributes for gun
    public GameObject nozzle;
    public GameObject player;
    
    // Attributes for line rendering
    private LineRenderer laserLine;
    public float laserLength = 10f;
    public float laserDuration = 0.05f;
    
    // Attributes for handling shoot
    private bool _take;
    private bool _give;

    private long timestampOfLastGunHit;
    
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        timestampOfLastGunHit = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
    }

    void Update()
    {

        Vector3 nozzlePosition = nozzle.transform.position;
        this._take = Input.GetButtonDown("Fire2");
        this._give = Input.GetButtonDown("Fire1");

        // If any of the buttons clicked
        if (_take || _give)
        {
            // Shoot a raycast first
            RaycastHit2D hit = Physics2D.Raycast(nozzle.transform.position, transform.TransformDirection(Vector2.right), laserLength);
            // If no collider hit then show laser yellow till laser length
            AlterColor(laserLine, Color.gray);
            
            // If raycast hits a collider
            if (hit.collider != null)
            {
                // Track last time player hit a collider 
                timestampOfLastGunHit = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

                // If Collider hits for subtraction
                if (_take)
                {
                    if (hit.collider.gameObject.CompareTag("TimeObject") && hit.collider.gameObject.GetComponent<TimeObject>().CheckSubtraction() && hit.collider.gameObject.GetComponent<TimeObject>().isActiveAndEnabled)
                    {
                        hit.collider.gameObject.GetComponent<TimeObject>().SubtractTime(1);
                        player.GetComponent<TimeBank>().AddTime(1);
                        AlterColor(laserLine, Color.red);
                        // Show laser only if it is a time object
                        hit.collider.gameObject.GetComponent<TimeObject>().TryUpdateShapeToAttachedSprite();
                    }
                    else if (hit.collider.gameObject.CompareTag("Mirror"))
                    {
                        // Subtract time if player is not a baby
                        if ( player.GetComponent<PlayerController>().getAge() > 0 )
                        {
                            player.GetComponent<PlayerController>().decreaseAge();
                            player.GetComponent<TimeBank>().AddTime(1);
                            AlterColor(laserLine, Color.red);

                            // Shrink the player
                            player.GetComponent<Transform>().localScale = player.GetComponent<PlayerController>().getPlayerSize();
                        }
                    }
                }
                // If Collider hits for addition
                else if (_give)
                {
                    if (hit.collider.gameObject.CompareTag("TimeObject") && player.GetComponent<TimeBank>().CheckSubtract() && hit.collider.gameObject.GetComponent<TimeObject>().CheckAddition() && hit.collider.gameObject.GetComponent<TimeObject>().isActiveAndEnabled )
                    {
                        hit.collider.gameObject.GetComponent<TimeObject>().AddTime(1);
                        player.GetComponent<TimeBank>().SubtractTime(1);
                        AlterColor(laserLine, Color.green);
                        // Show laser only if it is a time object
                        hit.collider.gameObject.GetComponent<TimeObject>().TryUpdateShapeToAttachedSprite();
                    }
                    else if (hit.collider.gameObject.CompareTag("Mirror") && player.GetComponent<TimeBank>().CheckSubtract())
                    {
                        // Subtract time if player is not a baby
                        if (player.GetComponent<PlayerController>().getAge() < 2)
                        {
                            player.GetComponent<PlayerController>().increaseAge();
                            player.GetComponent<TimeBank>().SubtractTime(1);
                            AlterColor(laserLine, Color.green);

                            // Grow the player
                            player.GetComponent<Transform>().localScale = player.GetComponent<PlayerController>().getPlayerSize();
                        }
                    }
                }
                // If raycast hits rewind object
                if (hit.collider.gameObject.CompareTag("RewindObject") && !PlayerStatus.isRewinding)
                {
                    if (hit.collider.gameObject.GetComponent<FallingRewindObject>() != null)
                    {
                        FallingRewindObject fro = hit.collider.gameObject.GetComponent<FallingRewindObject>();

                        if (fro.isActiveAndEnabled)
                        {
                            fro.Rewind();
                            AlterColor(laserLine, Color.yellow);
                        }
                    }
                    else if (hit.collider.gameObject.GetComponent<SlidingRewindObject>() != null)
                    {
                        SlidingRewindObject sro = hit.collider.gameObject.GetComponent<SlidingRewindObject>();
                        if (sro.isActiveAndEnabled)
                        {
                            sro.Rewind();
                            AlterColor(laserLine, Color.yellow);
                        }
                    }
                    else if (hit.collider.gameObject.GetComponent<RotatingRewindObject>() != null)
                    {
                        RotatingRewindObject rro = hit.collider.gameObject.GetComponent<RotatingRewindObject>();

                        if (rro.isActiveAndEnabled)
                        {
                            rro.Rewind();
                            AlterColor(laserLine, Color.yellow);
                        }
                    }
                }
                _ShowLaser(nozzlePosition, hit.point);
            }
            else
            { 
                _ShowLaser(nozzlePosition, nozzlePosition + nozzle.transform.right * laserLength);
            }
        }
    }

    private void _ShowLaser(Vector3 startPosition, Vector3 endPosition)
    {
        laserLine.SetPosition(0, startPosition);
        laserLine.SetPosition(1, endPosition);
        StartCoroutine(LaserShow());
    } 

    void AlterColor(LineRenderer line, Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }

    public long getLastTimePlayerHitObjectWithGun()
    {
        return timestampOfLastGunHit;
    }

    public void setLastTimePlayerHitObjectWithGun(long time)
    {
        this.timestampOfLastGunHit = time;
    }

    IEnumerator LaserShow()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}