using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms;


// Testing: radius = 8, speed = 200, nxt_waypoint_distance=1
// Have collider2D, Rigidbody2D, seeker attached to empty parent
public class EnemyAI : MonoBehaviour
{
    public Transform patrolTarget1;
    public Transform patrolTarget2;
    public Transform player;
    public Transform front;
    public float frontRadius;
    private LayerMask playerLayer;
    private LayerMask jumpLayer;
    public float chaseRadius;

    // private Vector3 startPt;
    private Transform target;
    private bool shouldChase;
    private bool isChasing;

    public float speed;
    // Waypoint is considered reached when object enters a circle area
    // The center is the actual waypoint location and the radius is this variable
    public float nxtWyptDistance; 

    // Current path following
    Path path;
    int currWypt = 0;
    // bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    Collider2D detected;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.GetMask("Ignore Raycast");
        jumpLayer = LayerMask.GetMask("Ground", "Object");
        target = patrolTarget1;
        shouldChase = false;
        isChasing = false;
        // UpdatePath(patrolTarget1.position, patrolTarget2.position);
        InvokeRepeating("UpdatePath", 0f, .25f); // Update path every 0.25 seconds
    }

    void Update()
    {
        Collider2D collided = Physics2D.OverlapCircle(rb.position, chaseRadius, playerLayer);
        if (collided == null)
        {
            shouldChase = false;
        }
        else if (!collided.gameObject.CompareTag("Player"))
        {
            shouldChase = false;
        }
        else
        {
            shouldChase = true;
        }
    }

    void FixedUpdate()
    {
        if (path == null) { return; }
        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (shouldChase && !isChasing)
        {
            rb.velocity = Vector2.zero;
            target = player;
            isChasing=true;
        }
        else if (shouldChase && isChasing)
        {
            target = player;
        }
        else if (!shouldChase && isChasing)
        {
            rb.velocity = Vector2.zero;
            target = patrolTarget1;
            isChasing=false;
        }

        if (currWypt >= path.vectorPath.Count) // reached final waypoint, end of patrol path
        {
            // rb.velocity = Vector2.zero;
            if (target != patrolTarget1 && target != patrolTarget2) { return; }
            target = (target == patrolTarget1) ? patrolTarget2 : patrolTarget1;
            return;
        }

        
        // not at goal yet, keep movin
        Vector2 direction = new Vector2((path.vectorPath[currWypt].x - rb.position.x), 0).normalized;
        float xVelocity = direction.x * speed * Time.deltaTime;
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
        if (Physics2D.OverlapCircle(front.position, frontRadius, jumpLayer))
        {
            rb.velocity = new Vector2(xVelocity, 0);
            rb.AddForce(new Vector2(0, 250f));
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currWypt]);
        if (distance < nxtWyptDistance)
        {
            currWypt++;
        }
    }

    void UpdatePath()
    {
        // Generate a path if no path is being calculated RN
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, target.position, OnPathGeneratComplete); 
        }
    }
    void OnPathGeneratComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currWypt = 0; // Start from beginnig of the new path
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, nxtWyptDistance);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(front.transform.position, frontRadius);
    }

}
