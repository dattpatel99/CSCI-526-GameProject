using Unity.VisualScripting;
using UnityEngine;

public class SectionTrackerTrial : MonoBehaviour
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
            aSection = new Section();
            aSection.sectionID = this.gameObject.name;
            aSection.enterTime = System.DateTime.Now;
            aSection.enterHearts = other.gameObject.GetComponent<PlayerController>().getHP().GetHP();
            aSection.startTimeBank = other.gameObject.GetComponent<TimeBank>().GetTimeStore();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        aSection.leaveTime = System.DateTime.Now;
        aSection.leaveTimeBank =other.gameObject.GetComponent<TimeBank>().GetTimeStore();
        aSection.leaveHearts =other.gameObject.GetComponent<PlayerController>().getHP().GetHP();
        parent.GetComponent<SectionManager>().addSection(aSection);
    }
}
