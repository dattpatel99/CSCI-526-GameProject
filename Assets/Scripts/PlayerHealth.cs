using UnityEngine;
using UnityEngine.UI;


// This script should be attached to the heartContainer, which should be an Empty Object with Horizontal Layout Group
// Spacing is -49 for now
public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int healthMax;
    public Sprite heartSprite_good; // change in Unity
    public Sprite heartSprite_bad;
    // public GameObject heartsContainer;

    // change in unity, for test
    /*
    public Button hpBtn_lose;
    public Button hpBtn_heal;
    */
    public Button hpBtn_add;

    public Transform heartPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        hpBtn_add.onClick.AddListener(AddHP);
        /*hpBtn_lose.onClick.AddListener(LoseHP);
        hpBtn_heal.onClick.AddListener(HealHP);*/
        
        health = healthMax;
        // hearts = new Image[healthMax];
        for (int i = 0; i < healthMax; i++)
        {
            Instantiate(heartPrefab, transform);
            //GameObject heartObj = new GameObject(string.Format("{0}", i));
            //heartObj.GetComponent<RectTransform>().SetParent(transform);
            //Image heartImg = heartObj.AddComponent<Image>();
            //heartImg.sprite = heartSprite_good;
        }
    }

    public int GetHP()
    {
        return health;
    }

    public void DamagePlayer(int damageNum)
    {
        for (int i = 0; i < damageNum; i++)
        {
            removeHeart();
        }
    }

    public void HealPlayer(int healPoints)
    {
        for (int i = 0; i < healPoints; i++)
        {
            addHeart();
        }

    }

    private void removeHeart()
    {
        health--;
        transform.GetChild(health).gameObject.GetComponent<Image>().sprite = heartSprite_bad;
    }

    private void addHeart()
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
        Transform newHeart = Instantiate(heartPrefab, transform);
        newHeart.gameObject.GetComponent<Image>().sprite = heartSprite_bad;
        // GameObject heartObj = new GameObject(string.Format("{0}", healthMax));
        healthMax++;
        // Image heartImg = heartObj.AddComponent<Image>();
        // heartImg.sprite = heartSprite_bad;
        // heartObj.GetComponent<RectTransform>().SetParent(transform);
        transform.GetChild(health).gameObject.GetComponent<Image>().sprite = heartSprite_good;
        health++;
    }
    
}
