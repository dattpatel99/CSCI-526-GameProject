using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public int startingTimeValue;
    public int highestTimeValue;

    public List<Sprite> phaseSprites;

    int currentTimeValue;

    void Start()
    {
        currentTimeValue = Mathf.Clamp(startingTimeValue, 0, highestTimeValue);
        GetComponent<SpriteRenderer>().sprite = phaseSprites[currentTimeValue];
        TryUpdateShapeToAttachedSprite();
    }

    public void AddTime(int addedTime)
    {
        int newValue = currentTimeValue + addedTime;
        currentTimeValue = Mathf.Clamp(newValue, 0, highestTimeValue);
        UpdateSprite();
    }

    public void SubtractTime(int subtractedTime)
    {
        int newValue = currentTimeValue - subtractedTime;
        currentTimeValue = Mathf.Clamp(newValue, 0, highestTimeValue);
        UpdateSprite();
    }

    void UpdateSprite()
    {
        GetComponent<SpriteRenderer>().sprite = phaseSprites[currentTimeValue];
    }

    public int getCurrentTimeValue()
    {
        return currentTimeValue;
    }

    public bool checkAddition()
    {
        return currentTimeValue < highestTimeValue;
    }
    public bool checkSubtration()
    {
        
        return currentTimeValue > 0;
    }
    
    // Unity Reference: https://answers.unity.com/questions/722748/refreshing-the-polygon-collider-2d-upon-sprite-cha.html
    public void TryUpdateShapeToAttachedSprite ()
    {
        PolygonCollider2D collider = this.GetComponent<PolygonCollider2D>();
        UpdateShapeToSprite(collider, collider.GetComponent<SpriteRenderer>().sprite);
    }
 
    public void UpdateShapeToSprite (PolygonCollider2D collider, Sprite sprite) { 
        // ensure both valid
        if (collider != null && sprite != null) {
            // update count
            collider.pathCount = sprite.GetPhysicsShapeCount();
                 
            // new paths variable
            List<Vector2> path = new List<Vector2>();
 
            // loop path count
            for (int i = 0; i < collider.pathCount; i++) {
                // clear
                path.Clear();
                // get shape
                sprite.GetPhysicsShape(i, path);
                // set path
                collider.SetPath(i, path.ToArray());
            }
        }
    }
}
