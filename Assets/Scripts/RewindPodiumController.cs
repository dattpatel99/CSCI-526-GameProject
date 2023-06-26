using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindPodiumController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rewindIcon;
    public Canvas targetCanvas;

    public List<GameObject> enemiesRequired;

    private bool isActivated;

    private void Start()
    {
        rewindIcon.SetActive(false);
        isActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated)
        {
            foreach (GameObject go in enemiesRequired)
            {
                if (go != null)
                {
                    isActivated = false;
                    break;
                }
                else
                {
                    isActivated = true;
                }
            }

            if (isActivated)
            {
                rewindIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && isActivated && !PlayerStatus.rewindUnlocked)
        {
            Debug.Log("Player picks up rewind ability");
            rewindIcon.SetActive(false);
            PlayerStatus.rewindUnlocked = true;
            targetCanvas.GetComponent<TextBoxController>().ShowText("You can now rewind non-living objects for 5 seconds with your time gun.", true);
        }
    }
}
