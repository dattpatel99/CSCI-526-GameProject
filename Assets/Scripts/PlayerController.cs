using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float landSpeed = 10.0f;
    public float airSpeed = 5.0f;
    public float jumpAmount = 20;

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool jumpInput;
    private bool isJumping;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();

        isJumping = false;
    }

    void Update()
    {
        // Get Inputs
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");

        if (isJumping)
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * airSpeed);
        }
        else
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * landSpeed);
        }
        

        if (!isJumping && jumpInput)
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Debug.Log("hit the floor");
            isJumping = false;
        }
    }
}
