using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float xLimit = 3f;
    public float yLimit = 2f;
    public float cameraSpeed = 15f;

    private bool isZoomedOut = false; //shriya
    private Vector3 originalPosition; //shriya
    private Quaternion originalRotation; //shriya
    private float originalOrthographicSize; //shriya

    


    public float zOffset = -10;
    
    // Update is called once per frame
    void LateUpdate()
    {
        if (originalPosition == Vector3.zero) //shriya 
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            originalOrthographicSize = Camera.main.orthographicSize;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isZoomedOut)
            {               
               
                // Calculate the bounding box of the map or canvas
                Renderer mapRenderer = player.GetComponent<Renderer>();
                if (mapRenderer != null)
                {
                Bounds mapBounds = mapRenderer.bounds;

                // Calculate the distance to fit the map or canvas within the camera's field of view
                float distance = Mathf.Max(mapBounds.size.x, mapBounds.size.y) / (2f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad));

                // Move the camera to center on the map or canvas and adjust the orthographic size
                transform.position = new Vector3(mapBounds.center.x, mapBounds.center.y, originalPosition.z);
                transform.rotation = Quaternion.Euler(originalPosition.x, originalPosition.y, 0f);
                Camera.main.orthographicSize = Mathf.Max(mapBounds.size.x * 25f, mapBounds.size.y * 10f);  //adjust the zoom out degree of the main camera(more value= more zoom out)
                Time.timeScale=0f;
                isZoomedOut = true;
                }
            }
            else
            {
            // Return to the previous camera position and settings
            Time.timeScale=1f;
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            Camera.main.orthographicSize = originalOrthographicSize;

            isZoomedOut = false;
            }
        }
        else
        {
            float xError = Mathf.Abs(player.transform.position.x - transform.position.x);
            float yError = Mathf.Abs(player.transform.position.y - transform.position.y);

        // If the object has moved more than the specified limits from the camera's position, move the camera
            if (xError > xLimit || yError > yLimit)
            {
            float step = cameraSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, zOffset), step);
            }
        } 
    }    
    
    
}
