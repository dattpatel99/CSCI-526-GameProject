using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float xLimit = 3f;
    public float yLimit = 2f;
    public float cameraSpeed = 15f;

    public float zOffset = -10;
    // Update is called once per frame
    void Update()
    {
        float xError = Mathf.Abs(player.transform.position.x - transform.position.x);
        float yError = Mathf.Abs(player.transform.position.y - transform.position.y);

        // If the object has moved more than 5f from camer's position move the camera
        if (xError > xLimit || yError > yLimit)
        {
            float step = cameraSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, zOffset), step);
        }
    }
}
