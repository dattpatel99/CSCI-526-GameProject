using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    // Player Movement: Public for testing but after that make private
    private float horizontalInput;
    public float landSpeed = 10.0f;
    public float airSpeed = 5.0f;
    public TextMeshProUGUI FinishText;

    // Gun Object Position
    public Transform gun;
    
    // Player rigidbody
    private Rigidbody2D rb;

    // Jump information
    public float jumpAmount = 40.0f;
    private bool jumpInput;
    private bool isJumping;

    private Vector3 startPosition;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        isJumping = false;
        startPosition = transform.position;
        FinishText.text = "";
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
        // Get Inputs
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");

        // Adjust player movement speed according to position
        Vector3 inputManipulation = Vector3.right * horizontalInput * Time.deltaTime;
        if (isJumping)
        {
            transform.Translate(inputManipulation * airSpeed);
        }
        else
        {
            transform.Translate(inputManipulation * landSpeed);
        }
        
        // Read Jump
        if (!isJumping && jumpInput)
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            isJumping = true;
        }

        // Check for death
        // TODO: For later stages we will to make it such that player is not visible on screen or touches a death collider incase the game has some death area that is not dependent on y-axis
        if (transform.position.y < -7f)
        {
            transform.position = startPosition;
        }
    }

    // For Jump reset 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isJumping = false;
        }
    }

    // For Finish line
    void OnTriggerEnter2D(Collider2D colliderObject)
    {
        if (colliderObject.gameObject.CompareTag("Finish"))
        {
            FinishText.text = "Congratulations!";
            Time.timeScale = 0f;
        }
    }
}
