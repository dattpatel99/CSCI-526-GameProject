
using UnityEngine;

public class WallShooter : MonoBehaviour
{

    public GameObject bullet;
    public GameObject spawnLocation;
    public float shootingRate;
    public float bulletSpeed = 50;
    private float timer = 0;
    
    // Update is called once per frame
    void Update()
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
        GameObject bulletObj = Instantiate(bullet, spawnLocation.transform.position, Quaternion.identity);
        bulletObj.GetComponent<Rigidbody2D>().velocity = spawnLocation.transform.right * -bulletSpeed;
    }
}
