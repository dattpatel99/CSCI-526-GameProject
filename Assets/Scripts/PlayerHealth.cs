using UnityEngine;
using UnityEngine.UI;


// This script should be attached to the heartContainer, which should be an Empty Object with Horizontal Layout Group
// Spacing between each heart Prefab is -49 for now
public class PlayerHealth : MonoBehaviour
{
    private int health;
    public int healthMax;
    public Sprite heartSprite_good; // change in Unity
    public Sprite heartSprite_bad;
    public Transform heartPrefab;

    // change in unity, for test
    /*
    public Button hpBtn_lose;
    public Button hpBtn_heal;
    */
    public Button hpBtn_add;
    
    
    // Start is called before the first frame update
    void Start()
    {
        hpBtn_add.onClick.AddListener(AddMax);
        /*hpBtn_lose.onClick.AddListener(LoseHP);
        hpBtn_heal.onClick.AddListener(HealHP);*/
        
        health = healthMax;
        for (int i = 0; i < healthMax; i++)
        {
            Instantiate(heartPrefab, transform);
        }
    }

    public int GetCurr()
    {
        return health;
    }

    public void Damage(int damageVal)
    {
        int currHealth = health;
        int newHealth = health - damageVal;
        if (newHealth < 0)
        {
            newHealth = 0;
        }
        for (int i = currHealth-1; i > newHealth-1; i--)
        {
            transform.GetChild(i).gameObject.GetComponent<Image>().sprite = heartSprite_bad;
        }
        health = newHealth;
    }

    public void Heal(int healVal)
    {
        int currHealth = health;
        int newHealth = health + healVal;
        if (newHealth > healthMax)
        {
            newHealth = healthMax;
        }
        for (int i = currHealth; i < newHealth; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Image>().sprite = heartSprite_good;
        }
        health = newHealth;
    }

    // Paul: Do we want to health the player as well?
    public void AddMax()
    {
        Transform newHeart = Instantiate(heartPrefab, transform);
        newHeart.gameObject.GetComponent<Image>().sprite = heartSprite_bad;
        healthMax++;
        transform.GetChild(health).gameObject.GetComponent<Image>().sprite = heartSprite_good;
        health++;
    }

    public void Reset()
    {
        health = healthMax;
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.GetComponent<Image>().sprite = heartSprite_good;
        }
    }

    public int GetHP()
    {
        return this.health;
    }

}
