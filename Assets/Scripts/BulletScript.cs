
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
        Destroy(this);
    }
}
