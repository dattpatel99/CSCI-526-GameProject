
using System;
using UnityEngine;

public class WallShooter : MonoBehaviour
{

    public GameObject bullet;
    public Transform spawnLocation;
    public float shootingRate;
    public float bulletSpeed = 4;
    private float timer = 0;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > shootingRate)
        {
            Shoot();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletObj = Instantiate(bullet, spawnLocation.transform.position, transform.rotation);
        bulletObj.GetComponent<Rigidbody2D>().velocity = spawnLocation.transform.right * -bulletSpeed;
        bulletObj.GetComponent<BulletScript>().rewindPhase = BulletScript.MotionPhase.Steady;
     }
}