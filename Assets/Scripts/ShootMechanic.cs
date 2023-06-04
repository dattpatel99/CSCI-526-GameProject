using System.Collections;
using UnityEngine;

public class ShootMechanic : MonoBehaviour
{
    public GameObject nozzle;
    public GameObject player;
    public LineRenderer laserLine;

    public float laserLength = 10f;
    public float laserDuration = 0.05f;
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 nozzlePosition = nozzle.transform.position;
        bool take = Input.GetButtonDown("Fire2");
        bool give = Input.GetButtonDown("Fire1");
        if (take || give)
        {
            RaycastHit2D hit = Physics2D.Raycast(nozzle.transform.position, transform.TransformDirection(Vector2.right), laserLength);
            // If no collider hit then show laser till laser length
            AlterColor(laserLine, Color.yellow);
            if (hit.collider != null)
            {
                // If Collider hits for subtraction
                if (take && hit.collider.gameObject.CompareTag("TimeObject") &&
                    hit.collider.gameObject.GetComponent<TimeObject>().checkSubtration()
                    && hit.collider.gameObject.GetComponent<TimeObject>().isActiveAndEnabled)
                {
                    hit.collider.gameObject.GetComponent<TimeObject>().SubtractTime(1);
                    hit.collider.gameObject.GetComponent<TimeObject>().TryUpdateShapeToAttachedSprite();
                    player.GetComponent<TimeBank>().AddTime(1);
                    AlterColor(laserLine, Color.red);
                }
                // If Collider hits for addition
                else if (give && hit.collider.gameObject.CompareTag("TimeObject") &&
                         player.GetComponent<TimeBank>().checkSubtract() &&
                         hit.collider.gameObject.GetComponent<TimeObject>().checkAddition()
                         && hit.collider.gameObject.GetComponent<TimeObject>().isActiveAndEnabled)
                {
                    hit.collider.gameObject.GetComponent<TimeObject>().AddTime(1);
                    hit.collider.gameObject.GetComponent<TimeObject>().TryUpdateShapeToAttachedSprite();
                    player.GetComponent<TimeBank>().SubtractTime(1);
                    AlterColor(laserLine, Color.green);
                }
                // Show laser
                laserLine.SetPosition(0, nozzlePosition);
                laserLine.SetPosition(1, hit.point);
                StartCoroutine(LaserShow());
            }
            else
            {
                laserLine.SetPosition(0, nozzlePosition);
                laserLine.SetPosition(1, nozzlePosition + nozzle.transform.right * laserLength);
                StartCoroutine(LaserShow());
            }
        }
    }

    void AlterColor(LineRenderer line, Color color)
    {
        line.startColor = color;
        line.endColor = color;
    }

    IEnumerator LaserShow()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}