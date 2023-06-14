using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float xOffset = 2;

    public float zOffset = -10;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x + xOffset, 0, zOffset);
        
    }
}
