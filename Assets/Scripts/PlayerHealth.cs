using UnityEngine;
using UnityEngine.UI;


// This script should be attached to the heartContainer, which should be an Empty Object with Horizontal Layout Group

public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int healthMax;
    public Sprite heartSprite_good; // change in Unity
    public Sprite heartSprite_bad;
    // public GameObject heartsContainer;

    // change in unity, for test
    public Button hpBtn_lose;
    public Button hpBtn_heal;
    public Button hpBtn_add;
    

    // Start is called before the first frame update
    void Start()
    {
        hpBtn_add.onClick.AddListener(AddHP);
        hpBtn_lose.onClick.AddListener(LoseHP);
        hpBtn_heal.onClick.AddListener(HealHP);
        
        health = healthMax;
        // hearts = new Image[healthMax];
        for (int i = 0; i < healthMax; i++)
        {
            GameObject heartObj = new GameObject(string.Format("{0}", i));
            heartObj.GetComponent<RectTransform>().SetParent(transform);
            Image heartImg = heartObj.AddComponent<Image>();
            heartImg.sprite = heartSprite_good;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // if (health <= 0)
    }

    public int GetHP()
    {
        return health;
    }

    public void LoseHP()
    {
        health--;
        transform.GetChild(health).gameObject.GetComponent<Image>().sprite = heartSprite_bad;
    }

    public void HealHP()
    {
        health = healthMax;
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.GetComponent<Image>().sprite = heartSprite_good;
        }
    }
    
    // Paul: Do we want to health the player as well?
    public void AddHP()
    {
        GameObject heartObj = new GameObject(string.Format("{0}", healthMax));
        healthMax++;
        Image heartImg = heartObj.AddComponent<Image>();
        heartImg.sprite = heartSprite_bad;
        heartObj.GetComponent<RectTransform>().SetParent(transform);
        transform.GetChild(health).gameObject.GetComponent<Image>().sprite = heartSprite_good;
        health++;
    }
    
}
