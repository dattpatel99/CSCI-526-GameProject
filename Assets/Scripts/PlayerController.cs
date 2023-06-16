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
    private Rigidbody2D rb2d;

    // Jump information
    public float jumpforce = 500.0f;

    public LayerMask groundLayer;
    public Transform feet;
    public bool grounded;

    private Vector3 startPosition;

    void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
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
        grounded = Physics2D.OverlapCircle(feet.position, .2f, groundLayer);
        // horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");

        if (grounded)
        {
            transform.Translate(Vector2.right * Time.deltaTime * landSpeed * horizontalInput);
        }
        else
        {
            transform.Translate(Vector2.right * Time.deltaTime * airSpeed * horizontalInput);
        }

        // jump, multi-jump prevention
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb2d.AddForce(new Vector2(0, jumpforce));
        }
        
        // Check for death
        // TODO: For later stages we will to make it such that player is not visible on screen or touches a death collider incase the game has some death area that is not dependent on y-axis
        /*if (transform.position.y < -7f)
        {
            transform.position = startPosition;
        }*/
    }

    // For Jump reset 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            grounded = true;
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
    
    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(feet.position, .2f);
    }
}