using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    // Player Movement
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
        if (isJumping)
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * airSpeed);
        }
        else
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * landSpeed);
        }
        
        // Read Jump
        if (!isJumping && jumpInput)
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            isJumping = true;
        }

        if (transform.position.y < -7f)
        {
            transform.position = startPosition;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Finish")
        {
            FinishText.text = "Congratulations!";
            Time.timeScale = 0f;
        }
    }
}
