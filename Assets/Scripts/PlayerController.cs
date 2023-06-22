using System;
using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the player movement methods
/// </summary>
public class PlayerController : MonoBehaviour
{

    // Player Movement: Public for testing but after that make private
    public float landSpeed = 10.0f;
    public float airSpeed = 5.0f;
    private float horizontalInput;
    public float jumpforce = 500.0f;
    private Rigidbody2D rb2d;
    // private float direction = 1.0f;

    // Attributes required for jump functionality
    public LayerMask groundLayer;
    public LayerMask objectLayer;
    public Transform feet;
    public bool grounded;

    // Gun Object Position
    public Transform gun;
    public float sensitivity = 1.0f;

    // Jump information
    private bool jumpInput;

    //Player body transformation
    // 0 = small, 1 = normal, 2 = old
    private int playerAge = 1;
    private Vector3 _respawnPosition;

    // Move up and down the beanstalk
    private float vertical;
    private bool isBeanstalk;
    private bool isClimbing;
    private float initGravityScale;
    
    // Finish Line
    public TextMeshProUGUI FinishText;
    
    // Damage related
    private string playerStatus; // normal, invincible
    private bool canCtrl;
    public float afterDmgForce;
    public GameObject heartsObj;
    private PlayerHealth HP;
    private int damageValAll;

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        initGravityScale = rb2d.gravityScale;
        _respawnPosition = transform.position;
        FinishText.text = "";

        playerStatus = "normal";
        canCtrl = true;
        afterDmgForce = 300.0f;
        HP = heartsObj.GetComponent<PlayerHealth>();
        damageValAll = 1;
    }

    void Update()
    {
        // Check for death
        if (transform.position.y < -22 || HP.GetCurr() == 0)
        {
            // transform.position = this._respawnPosition;
            // Time.timeScale = 0; // Pause movement
            // yield return new WaitForSecondsRealtime(2); // Wait 2 seconds to restart  
            transform.position = this._respawnPosition;
            HP.Reset();
            // Time.timeScale = 1; // Continue movement 
        }
        GunRotation();
        if (!canCtrl)
        {
            return;
        }
        HandleJump();
        
        // Beanstalk logic
        vertical = Input.GetAxis("Vertical");
        if (isBeanstalk && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }

        // TODO: For later stages we will to make it such that player is not visible on screen or touches a death collider incase the game has some death area that is not dependent on y-axis

    }
    
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb2d.gravityScale = 0f;
            rb2d.velocity = new Vector2(rb2d.velocity.x, vertical * 5.0f);
        }
        else
        {
            rb2d.gravityScale = initGravityScale;
            //rb2d.velocity = Vector2.zero;
        }
    }

    // Climb beanstalk logic
    private void OnTriggerEnter2D(Collider2D other)
    {
        // takes 1 and ONLY 1 point of damage each time
        if(other.CompareTag("Damaging") && (playerStatus == "normal")) // player not in after-damage protection
        {
            ReceiveDamage(other.gameObject);
        }
        GameObject collidingObject = other.gameObject;
        if (collidingObject.name == "Bean")
        {
            if (collidingObject.GetComponent<TimeObject>().GetCurrentTimeValue() == 1)
            {
                isBeanstalk = true;
            }
        }
        else if (collidingObject.name == "FinishLine")
        {
            FinishText.text = "Congratulations!";
            Time.timeScale = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Damaging") && (playerStatus == "normal")) // player not in after-damage protection
        {
            ReceiveDamage(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ( collision.gameObject.name == "Bean")
        {
            isBeanstalk = false;
            isClimbing = false;
        }
    }

    private void GunRotation()
    {
        // Rotate Gun
        Vector3 mousePos = (Input.mousePosition);
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 gunRotation = mousePos - playerPos;
        float angle = Mathf.Atan2(gunRotation.y, gunRotation.x) * Mathf.Rad2Deg;
        /*angle = Mathf.Clamp(angle, -45f, 45f);*/ // Gun Rotation 
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0,  angle));
    }
    
    private void HandleJump()
    {
        grounded = Physics2D.OverlapCircle(feet.position, .2f, groundLayer) || Physics2D.OverlapCircle(feet.position, .2f, objectLayer);

        // horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");

        // Difference between land and air speed
        if (!grounded)
        {
            transform.Translate(Vector2.right * Time.deltaTime * airSpeed * horizontalInput);
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime * landSpeed * horizontalInput);
        }

        /*
        // Scale Player left or right based on horizontal input
        if (horizontalInput < 0)
        {
            direction = -1.0f;
        }
        else if (horizontalInput > 0)
        {
            direction = 1.0f;
        }  
        this.transform.localScale = new Vector3(direction * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z); // Flip the player horizontally
        */
        
        // jump, multi-jump prevention
        if (jumpInput && grounded)
        {
            rb2d.AddForce(new Vector2(0, jumpforce));
        }
    }
    
    public void increaseAge()
    {
        playerAge += 1;
    }

    public void decreaseAge()
    {
        playerAge -= 1;
    }

    public int getAge()
    {
        return playerAge;
    }

    public Vector3 getPlayerSize()
    {
        switch (this.playerAge) {
            case 0:
                return new Vector3(0.75f, 0.75f, 1);
            case 1:
                return new Vector3(0.9f, 0.9f, 1);
            case 2:
                return new Vector3(.95f, 0.95f, 1);
            default:
                // shouldn't be possible
                return new Vector3(0, 0, 0);
        }
    }
    
    public void setRespwan(Vector3 location)
    {
        _respawnPosition = location;
    }
    
    public Vector3 getRespwan()
    {
        return _respawnPosition;
    }

    private void ReceiveDamage(GameObject target)
    {
        playerStatus = "invincible";
        canCtrl = false;
        StartCoroutine(AfterDmgProcess()); // reset status to normal, re-enable control
        HP.Damage(damageValAll);
        rb2d.velocity = new Vector2(0, 0);
        int bounceDir = (transform.position.x - target.transform.position.x < 0) ? -1 : 1;
        // Debug.Log(bounceDir);
        rb2d.AddForce(new Vector2(afterDmgForce * bounceDir, afterDmgForce));
    }
    
    private IEnumerator AfterDmgProcess()
    {
        yield return new WaitForSeconds(0.5f); // 0.5s to allow the bump-off to finish
        canCtrl = true;
        yield return new WaitForSeconds(1.5f); // 2s invincilble
        playerStatus = "normal";
    }
    
}
