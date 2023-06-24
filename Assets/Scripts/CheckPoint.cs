using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using Analytics;
public class CheckPoint : MonoBehaviour
{
    public GameObject Sign;
    private SpriteRenderer signRender;
    private CheckPointAnalytics checkpointData;
    private CheckPointManager manager;

    void Start()
    {
        signRender = Sign.GetComponent<SpriteRenderer>();
        manager = gameObject.transform.parent.gameObject.GetComponent<CheckPointManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().name == "Player")
        {
            other.GameObject().GetComponent<PlayerController>().setRespwan(this);
            var info = new CheckPointAnalytics(gameObject.name, other.GameObject().GetComponent<PlayerController>(), other.GameObject().GetComponent<TimeBank>());
            manager.SendData(info);
        }
    }

    public void AlterSignColor(Color newColor)
    {
        signRender.color = newColor;
    }
    
    
}
