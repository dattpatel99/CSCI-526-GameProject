using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float xLimit = 5f;
    public float yLimit = 5f;
    public float cameraSpeed = 4f;

    public float zOffset = -10;
    // Update is called once per frame
    void Update()
    {
        float xError = Mathf.Abs(player.transform.position.x - transform.position.x);
        float yError = Mathf.Abs(player.transform.position.y - transform.position.y);

        if (xError > xLimit || yError > yLimit)
        {
            float step = cameraSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 0, zOffset), step);
        }
    }
}
