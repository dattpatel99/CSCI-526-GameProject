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
    // Storing GameComponents
    private PlayerController playerControllerComp;
    private TimeBank playerTimeBank;
    private Transform playerTransform;

    private LayerMask interactableMasks;
    
    void Awake()
    {
        interactableMasks = LayerMask.GetMask("Ground", "Object");
        
        laserLine = GetComponent<LineRenderer>();
        timestampOfLastGunHit = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
        playerControllerComp = player.GetComponent<PlayerController>();
        playerTimeBank = player.GetComponent<TimeBank>();
        playerTransform = player.GetComponent<Transform>();
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
            RaycastHit2D hit = Physics2D.Raycast(nozzle.transform.position, transform.TransformDirection(Vector2.right), laserLength, interactableMasks);
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
                        playerTimeBank.AddTime(1);
                        AlterColor(laserLine, Color.red); // Show laser only if it is a time object
                        // hit.collider.gameObject.GetComponent<TimeObject>().TryUpdateShapeToAttachedSprite(); // Paul: This method is being called already in SubtractTime
                    }
                    else if (hit.collider.gameObject.CompareTag("Mirror"))
                    {
                        // Subtract time if player is not a baby
                        if (playerControllerComp.getAge() > 0 )
                        {
                            playerControllerComp.decreaseAge();
                            playerTimeBank.AddTime(1);
                            AlterColor(laserLine, Color.red);

                            // Shrink the player
                            playerTransform.localScale = playerControllerComp.getPlayerSize();
                        }
                    }
                }
                // If Collider hits for addition
                else if (_give)
                {
                    if (hit.collider.gameObject.CompareTag("TimeObject") && playerTimeBank.CheckSubtract() && hit.collider.gameObject.GetComponent<TimeObject>().CheckAddition() && hit.collider.gameObject.GetComponent<TimeObject>().isActiveAndEnabled )
                    {
                        hit.collider.gameObject.GetComponent<TimeObject>().AddTime(1);
                        playerTimeBank.SubtractTime(1);
                        AlterColor(laserLine, Color.green); // Show laser only if it is a time object
                        // hit.collider.gameObject.GetComponent<TimeObject>().TryUpdateShapeToAttachedSprite(); // Paul: This method is being called already in AddTime
                    }
                    else if (hit.collider.gameObject.CompareTag("Mirror") && playerTimeBank.CheckSubtract())
                    {
                        // Subtract time if player is not a baby
                        if (playerControllerComp.getAge() < 2)
                        {
                            playerControllerComp.increaseAge();
                            playerTimeBank.SubtractTime(1);
                            AlterColor(laserLine, Color.green);

                            // Grow the player
                            playerTransform.localScale = playerControllerComp.getPlayerSize();
                        }
                    }
                    else if (hit.collider.gameObject.CompareTag("Enemy") && playerTimeBank.CheckSubtract())
                    {
                        hit.collider.gameObject.GetComponent<EnemyController>().Die();
                        playerTimeBank.SubtractTime(1);
                        AlterColor(laserLine, Color.green);
                    }
                }

                if (PlayerStatus.rewindUnlocked)
                {
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