using Newtonsoft.Json.Bson;
using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the player movement methods
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    
    // 1. Player Control: Public for testing but after that make private
    // =================================================================
    // Horizontal Movement
    public float normalSpeed = 9.0f;
    public float fastSpeed = 12.0f;
    private float currentSpeed;
    private float horizontalInput;
    private float xSpeed;
    // private float direction = 1.0f;
    // Vertical Movement
    public float mediumJumpForce = 350.0f;
    public float bigJumpForce = 400.0f;
    private float jumpForce;
    public float bounceForce;
    public LayerMask groundLayer;
    public LayerMask objectLayer;
    public Transform feet;
    private bool jumpInput;
    private Vector2 feetPos;
    private bool grounded;
    private bool onObject;
    private bool shouldJump;
    private float verticalInput;
    private bool isBeanstalk;
    private bool isClimbing;
    private float initGravityScale;
    
    // 2. Health & Damage
    // =================================================================
    public GameObject heartsObj;
    public float afterDmgForce;
    private PlayerHealth HP;
    private int damageValAll;
    private string playerStatus; // normal, invincible
    public static bool canCtrl; // will be set to true by CameraController
    private int numberDeaths;

    // 3. Gun
    // =================================================================
    public Transform gun;
    public float sensitivity = 1.0f;

    // 4. Others
    // =================================================================
    private int playerAge = 1;          // Age. {0: small, 1: normal, 2: old}
    private Vector3 _respawnPosition;   // Respawn Position
    private CheckPoint respawnCheckPoint;
    private Vector3 _b4DrownedPosition; // Drown prevntion
    private bool lastGroundedPosRecorded;
    public Sprite normalSprite;
    public Sprite smallSprite;
    public Sprite bigSprite;
    private SpriteRenderer sr;
    private BoxCollider2D boxCollider;
    private SpriteRenderer objectSpriteRenderer;
    private Color playerColor;
    private int butterfliesCollected = 0;
    
    // Analytics
    //========================================================================
    public GameObject analyticManager;
    private AnalyticManager _manger;

    void Start()
    {
        _manger = analyticManager.GetComponent<AnalyticManager>();
        objectSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerColor = objectSpriteRenderer.color;
        rb2d = this.GetComponent<Rigidbody2D>();
        initGravityScale = rb2d.gravityScale;
        _respawnPosition = transform.position;

        playerStatus = "normal";
        canCtrl = false;
        afterDmgForce = 3000.0f;
        HP = heartsObj.GetComponent<PlayerHealth>();
        damageValAll = 1;
        lastGroundedPosRecorded = false;

        playerAge = 1;

        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        AdjustSpeedAndJump();
        SetSprite();

        numberDeaths = 0;
    }

    void Update()
    {
        DeathCheck();
        ClimbCheck(); //beanstalk logic
        GunRotation();
        xSpeed = Input.GetAxis("Horizontal") * currentSpeed;
        jumpInput = Input.GetButtonDown("Jump");
        feetPos = feet.position;
        grounded = Physics2D.OverlapCircle(feetPos, .2f, groundLayer);
        onObject = Physics2D.OverlapCircle(feetPos, .2f, objectLayer);
        
        if (!grounded && !lastGroundedPosRecorded)
        {
            _b4DrownedPosition = transform.position;
            lastGroundedPosRecorded = true;
        }
        else if (grounded && lastGroundedPosRecorded)
        {
            lastGroundedPosRecorded = false;
        }
        if (!shouldJump && jumpInput && (grounded || onObject))
        {
            shouldJump = true;
        }
        // TODO: For later stages we will to make it such that player is not visible on screen or touches a death collider incase the game has some death area that is not dependent on y-axis
    }
    
    void FixedUpdate()
    {
        if (!canCtrl) { return; } // after-damage protection
        rb2d.velocity = new Vector2(xSpeed, rb2d.velocity.y); // horizontal movement
        // rb2d.AddForce(new Vector2(xSpeed, 0f), ForceMode2D.Impulse);
        if (shouldJump) // jump
        {
            shouldJump = false;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0); // allows mid-air jump
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
        if (isClimbing) // climb
        {
            rb2d.gravityScale = 0f;
            rb2d.velocity = new Vector2(rb2d.velocity.x, verticalInput * 5.0f);
        }
        else
        {
            rb2d.gravityScale = initGravityScale;
        }
    }

    // Climb beanstalk logic
    void OnTriggerEnter2D(Collider2D other)
    {
        // takes damage only once each time
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Water") && playerStatus == "normal")
        {
            HP.Damage(damageValAll);
            if (!DeathCheck())
            {
                StartCoroutine(DrownedProcess()); // reset status to normal, re-enable control
                StartCoroutine(AfterDmgVisual());
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Damaging") && (playerStatus == "normal")) // player not in after-damage protection
        {
            ReceiveDamage(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy") && (playerStatus == "normal")) // player not in after-damage protection
        {
            ReceiveDamage(other.gameObject);
        }
        else if (other.gameObject.name.Contains("TimeCaseParent"))
        {
            //Prevent error for below if case when player runs into TimeCase
        }
        
        if (other.gameObject.transform.parent != null)
        {
            if (other.gameObject.transform.parent.CompareTag("Mushroom"))
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
                rb2d.AddForce(new Vector2(0f, bounceForce));
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
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

    public PlayerHealth getHP()
    {
        return this.HP;
    }

    public int getNumberDeaths()
    {
        return this.numberDeaths;
    }

    private bool DeathCheck()
    {
        if (transform.position.y < -1000 || HP.GetCurr() == 0)
        {
            transform.position = this._respawnPosition;
            numberDeaths ++;
            HP.Reset();
            rb2d.velocity = new Vector2(0f, 0f);
            return true;
        }
        return false;
    }

    private void ClimbCheck()
    {
        verticalInput = Input.GetAxis("Vertical");
        if (isBeanstalk && Mathf.Abs(verticalInput) > 0f)
        {
            isClimbing = true;
        }
    }

    public void AlterAge(int change)
    {
        playerAge += change;
        AdjustSpeedAndJump();
        SetSprite();
    }

    public void increaseAge()
    {
        playerAge += 1;
        AdjustSpeedAndJump();
        SetSprite();
    }

    public void decreaseAge()
    {
        playerAge -= 1;
        AdjustSpeedAndJump();
        SetSprite();
    }

    void AdjustSpeedAndJump()
    {
        if (playerAge >= 2)
        {
            jumpForce = bigJumpForce;
        }
        else
        {
            jumpForce = mediumJumpForce;
        }

        if (playerAge <= 0)
        {
            currentSpeed = fastSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }
    }

    void SetSprite()
    {
        if (playerAge >= 2)
        {
            sr.sprite = bigSprite;
            boxCollider.size = new Vector2(1f, 2f);
            feet.position = transform.position + new Vector3(0f, -1f, 0f);
        }
        else if (playerAge == 1)
        {
            sr.sprite = normalSprite;
            boxCollider.size = new Vector2(1f, 2f);
            feet.position = transform.position + new Vector3(0f, -1f, 0f);
        }
        else
        {
            sr.sprite = smallSprite;
            boxCollider.size = new Vector2(1f, 1f);
            feet.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }

    public int getAge()
    {
        return playerAge;
    }

    public Vector3 getPlayerSize()
    {
        switch (this.playerAge) {
            case 0:
                return new Vector3(0.95f, 0.95f, 1);
            case 1:
                return new Vector3(0.9f, 0.9f, 1);
            case 2:
                return new Vector3(1.1f, 1.1f, 1);
            default:
                // shouldn't be possible
                return new Vector3(0, 0, 0);
        }
    }
    
    public void setRespwan(CheckPoint checkPoint)
    {
        if (respawnCheckPoint != null)
        {
            respawnCheckPoint.AlterSignColor(Color.white);
        }
        respawnCheckPoint = checkPoint;
        respawnCheckPoint.AlterSignColor(Color.green);
        _respawnPosition = checkPoint.transform.position;
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
        StartCoroutine(AfterDmgVisual());
        var preHealth = HP.GetHP();
        HP.Damage(damageValAll);
        _manger.SendDamageInfo(HP.GetHP()>preHealth,target.name, preHealth,HP.GetHP(), Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.y));
        rb2d.velocity = new Vector2(0, 0);
        int bounceDir = (transform.position.x - target.transform.position.x < 0) ? -1 : 1;
        rb2d.AddForce(new Vector2(afterDmgForce * bounceDir, afterDmgForce));
    }
    
    private IEnumerator AfterDmgProcess()
    {
        yield return new WaitForSeconds(0.5f); // 0.5s to allow the bump-off to finish
        canCtrl = true;
        yield return new WaitForSeconds(1.5f); // 2s invincilble
        playerStatus = "normal";
    }

    private IEnumerator DrownedProcess()
    {
        playerStatus = "drowned";
        canCtrl = false;
        yield return new WaitForSeconds(1.0f);
        canCtrl = true;
        playerStatus = "normal";
        transform.position = _b4DrownedPosition;
    }

    private IEnumerator AfterDmgVisual()
    {
        objectSpriteRenderer.color = Color.red;
        heartsObj.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        objectSpriteRenderer.color = playerColor;
        heartsObj.SetActive(true);
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.1f);
            heartsObj.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            heartsObj.SetActive(true);
        }
    }

    public int getButterfliesCollected()
    {
        return butterfliesCollected;
    }

    public void addButterfly()
    {
        butterfliesCollected += 1;
    }

    public void spendButterfly()
    {
        butterfliesCollected -= 1;
    }
}
