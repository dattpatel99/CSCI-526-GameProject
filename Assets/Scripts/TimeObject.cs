using System.Collections.Generic;
using UnityEngine;

/*
 * Class: Object whose 'age' we can change
 */
public class TimeObject : MonoBehaviour
{
    // Tracks if the object can be reacted with (e. Tree falls to bridge)
    public List<bool> reactable;
    
    // These are public so level designers can change easily
    public int startingTimeValue; 
    public int highestTimeValue;
    public List<Sprite> phaseSprites;
    private int currentTimeValue;

    [SerializeField]
    private bool[] colliderOn;
    
    /*
     * Start does:
     * Store entered time value. Get the sprite based on current time. Update Sprite after choosing
     */
    void Start()
    {
        this.currentTimeValue = Mathf.Clamp(this.startingTimeValue, 0, this.highestTimeValue);
        GetComponent<SpriteRenderer>().sprite = this.GetSprite(this.currentTimeValue);
        TryUpdateShapeToAttachedSprite();
    }

    public void SetPhaseInteractability(List<bool> newInteractable)
    {
        if (newInteractable.Count == this.reactable.Count)
        {
            this.reactable = newInteractable;
        }
    }
    
    public List<bool> GetPhaseInteractability()
    {
        return this.reactable;
    }

    public void AddTime(int addedTime)
    {
        int newValue = this.currentTimeValue + addedTime;
        this.currentTimeValue = Mathf.Clamp(newValue, 0, this.highestTimeValue);
        GetComponent<SpriteRenderer>().sprite=this.GetSprite(this.currentTimeValue);
        TryUpdateShapeToAttachedSprite();
    }

    public void SubtractTime(int subtractedTime)
    {
        int newValue = this.currentTimeValue - subtractedTime;
        this.currentTimeValue = Mathf.Clamp(newValue, 0, this.highestTimeValue);
        GetComponent<SpriteRenderer>().sprite=this.GetSprite(this.currentTimeValue);
        TryUpdateShapeToAttachedSprite();
    }

    public int GetCurrentTimeValue()
    {
        return this.currentTimeValue;
    }

    public bool CheckAddition()
    {
        return this.currentTimeValue < this.highestTimeValue;
    }
    public bool CheckSubtraction()
    {
        return this.currentTimeValue > 0;
    }

    private Sprite GetSprite(int currentTime)
    {
        return this.phaseSprites[currentTime];
    }

    /*
     * These two functions are used to update the collider based on png of sprite
     */
    // Unity Reference: https://answers.unity.com/questions/722748/refreshing-the-polygon-collider-2d-upon-sprite-cha.html
    public void TryUpdateShapeToAttachedSprite ()
    {
        PolygonCollider2D thisObjectsCollider = this.GetComponent<PolygonCollider2D>();
        this.UpdateShapeToSprite(thisObjectsCollider, thisObjectsCollider.GetComponent<SpriteRenderer>().sprite);

        // The collider is set to be a trigger if we want the player to be able to walk past it
        thisObjectsCollider.isTrigger = !colliderOn[currentTimeValue];
    }
    private void UpdateShapeToSprite (PolygonCollider2D colliderObject, Sprite sprite) { 
        // ensure both valid
        if (colliderObject != null && sprite != null) {
            // update count
            colliderObject.pathCount = sprite.GetPhysicsShapeCount();
            // new paths variable
            List<Vector2> path = new List<Vector2>();
            // loop path count
            for (int i = 0; i < colliderObject.pathCount; i++) {
                // clear
                path.Clear();
                // get shape
                sprite.GetPhysicsShape(i, path);
                // set path
                colliderObject.SetPath(i, path.ToArray());
            }
        }
    }
}
