using Unity.VisualScripting;
using UnityEngine;
using Analytics;
public class CheckPoint : MonoBehaviour
{
    public GameObject AnalyticObject;
    private AnalyticManager manager;
    private CheckPointAnalytics checkpointData;

    void Start()
    {
        manager = AnalyticObject.GetComponent<AnalyticManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().name == "Player")
        {
            Vector3 respawnPost = new Vector3(this.GameObject().transform.position.x, other.GameObject().transform.position.y, this.GameObject().transform.position.z);
            other.GameObject().GetComponent<PlayerController>().setRespwan(respawnPost);
            
            // Add checkpoint analytics
            var info = new CheckPointAnalytics(gameObject.name, other.GameObject().GetComponent<PlayerController>(), other.GameObject().GetComponent<TimeBank>());
            manager.AddCheckPoint(info);
        }
    }
    
    
}
