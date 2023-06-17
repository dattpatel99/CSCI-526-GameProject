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

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    void Update()
    {
        GunRotation();
        HandleJump();
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
