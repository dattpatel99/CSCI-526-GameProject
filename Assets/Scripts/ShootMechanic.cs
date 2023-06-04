using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShootMechanic : MonoBehaviour
{
    public GameObject nozzle;
    public GameObject player;
    public LineRenderer laserLine;

    public float laserLength = 10f;
    public float laserDuration = 0.005f;
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Vector3 nozzlePosition = nozzle.transform.position;
        if (Input.GetButtonDown("Fire1"))
        {
            TakeTime(nozzlePosition);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            GiveTime(nozzlePosition);
        }
    }

    private void TakeTime(Vector3 nozzlePos)
    {
        RaycastHit2D hit = Physics2D.Raycast(nozzle.transform.position, transform.TransformDirection(Vector2.right), laserLength);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("TimeObject") && hit.collider.gameObject.GetComponent<TimeObject>().checkSubtration())
            {
                laserLine.SetPosition(0, nozzlePos);
                laserLine.SetPosition(1, hit.point);
                hit.collider.gameObject.GetComponent<TimeObject>().SubtractTime(1);
                player.GetComponent<TimeBank>().AddTime(1);
                StartCoroutine(LaserShow());
            }
            // Uncomment if you want to show laser if not hitting anything
            /*else
            {
                laserLine.SetPosition(0, nozzlePos);
                laserLine.SetPosition(1, nozzlePos + nozzle.transform.right * laserLength);
                StartCoroutine(LaserShow());
            }*/
        }
    }

    private void GiveTime(Vector3 nozzlePos)
    {
        RaycastHit2D hit = Physics2D.Raycast(nozzlePos, transform.TransformDirection(Vector2.right), laserLength);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("TimeObject") && player.GetComponent<TimeBank>().checkSubtract() && hit.collider.gameObject.GetComponent<TimeObject>().checkAddition())
            {
                laserLine.SetPosition(0, nozzlePos);
                laserLine.SetPosition(1, hit.point);
                hit.collider.gameObject.GetComponent<TimeObject>().AddTime(1);
                player.GetComponent<TimeBank>().SubtractTime(1);
                StartCoroutine(LaserShow());
            }
            // Uncomment if you want to show laser if not hitting anything
            /*else
            {
                laserLine.SetPosition(0, nozzlePos);
                laserLine.SetPosition(1, nozzlePos + nozzle.transform.right * laserLength);
                StartCoroutine(LaserShow());
            }*/
        }
    }

    IEnumerator LaserShow()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
