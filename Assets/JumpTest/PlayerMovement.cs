using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Multiple Jumps should be DISABLED!
    public float speed = 10.0f;
    private float horizontalInput;
    public float jumpforce = 500.0f;
    private Rigidbody2D rb2d;
    
    public LayerMask groundLayer;
    public Transform feet;
    public bool grounded;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        grounded = Physics2D.OverlapCircle(feet.position, .2f, groundLayer);
        // horizontal movement
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
        // jump, multi-jump prevention
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb2d.AddForce(new Vector2(0, jumpforce));
        }
    }
    
    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(feet.position, .2f);
    }
}
