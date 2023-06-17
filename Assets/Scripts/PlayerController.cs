using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    // Player Movement: Public for testing but after that make private
    public float speed = 10.0f;
    private float horizontalInput;
    public float jumpforce = 500.0f;
    private Rigidbody2D rb2d;

    public LayerMask groundLayer;
    public LayerMask objectLayer;
    public Transform feet;
    public bool grounded;

    // Gun Object Position
    public Transform gun;

    // Jump information
    private bool jumpInput;

    //Player body transformation
    // 0 = small, 1 = normal, 2 = old
    private int playerAge = 1;

    private Vector3 startPosition;

    // Move up and down the beanstalk
    private float vertical;
    private bool isBeanstalk;
    private bool isClimbing;
    private float initGravityScale;

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        initGravityScale = rb2d.gravityScale;
    }

    void Update()
    {
        GunRotation();
        HandleJump();

        // Beanstalk logic
        vertical = Input.GetAxis("Vertical");
        if (isBeanstalk && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        if (collidingObject.name == "Bean")
        {
            if (collidingObject.GetComponent<TimeObject>().GetCurrentTimeValue() == 1)
            {
                isBeanstalk = true;
                Debug.Log("Trigger Enter 2d: " + collision.gameObject.name);
            }
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
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 gunRotation = mousePos - playerPos;
        float angle = Mathf.Atan2(gunRotation.y, gunRotation.x) * Mathf.Rad2Deg;
        gun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }



    private void HandleJump()
    {
        grounded = Physics2D.OverlapCircle(feet.position, .2f, groundLayer) || Physics2D.OverlapCircle(feet.position, .2f, objectLayer);

        // horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");

        transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);

        // jump, multi-jump prevention
        if (jumpInput && grounded)
        {
            rb2d.AddForce(new Vector2(0, jumpforce));
        }

        // Check for death
        // TODO: For later stages we will to make it such that player is not visible on screen or touches a death collider incase the game has some death area that is not dependent on y-axis
        if (transform.position.y < -7f)
        {
            transform.position = startPosition;
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
                return new Vector3(0.95f, 0.6f, 1);
            case 1:
                return new Vector3(0.95f, 1, 1);
            case 2:
                return new Vector3(0.95f, 1, 1);
            default:
                // shouldn't be possible
                return new Vector3(0, 0, 0);
        }

    }
}
