using AnalyticsSection;
using UnityEngine;

public class CheckPointTracker : MonoBehaviour
{
    private Section aSection;
    private GameObject parent;

    void Start()
    {
        parent = this.gameObject.transform.parent.gameObject;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log(other.gameObject.GetComponent<PlayerHealth>().GetHP());
            aSection = new Section(this.gameObject.name, System.DateTime.Now, other.gameObject.GetComponent<PlayerHealth>().GetHP(),other.gameObject.GetComponent<TimeBank>().GetTimeStore());
            Debug.Log("Entering a section");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            aSection.updateLeaveValues(System.DateTime.Now, other.gameObject.GetComponent<TimeBank>().GetTimeStore(),
                other.gameObject.GetComponent<PlayerHealth>().GetHP());
            parent.GetComponent<SectionManager>().addSection(aSection);
            Debug.Log("Done Adding");
        }
    }
}
