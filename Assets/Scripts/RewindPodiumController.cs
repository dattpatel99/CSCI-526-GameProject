using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindPodiumController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rewindIcon;
    public GameObject textBox;
    public List<GameObject> enemiesRequired;

    private bool isActivated;

    private void Start()
    {
        textBox.SetActive(false);
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
            rewindIcon.SetActive(false);
            PlayerStatus.rewindUnlocked = true;
            textBox.SetActive(true);
            StartCoroutine(CloseRewindText());
        }
    }
    
    private IEnumerator CloseRewindText()
    {
        yield return new WaitForSeconds(5.0f);
        textBox.SetActive(false);
    }
}
