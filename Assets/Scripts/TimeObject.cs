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
    public int initTimePhase_i; // Paul: strictly greater than 0
    public int maxTimePhase_i; // Paul: length of Sprites and Colliders array
    public Sprite[] phaseSprites;
    protected int currentPhase_i;
    protected Collider2D[] phaseColliders;
    protected SpriteRenderer timeObjectSpriteRenderer;

    // [SerializeField]
    // private bool[] colliderOn;
    
    /*
     * Start does:
     * Store entered time value. Get the sprite based on current time. Update Sprite after choosing
     */
    void Start()
    {
        phaseColliders = GetComponents<Collider2D>();
        if (phaseColliders.Length != maxTimePhase_i + 1 || phaseSprites.Length != maxTimePhase_i + 1) {
            Debug.Log("WARNING: OUT OF INDEX POSSIBLE. Check totalTimephases, phaseSprites and number of attached colliders. They should all match");
        }
        timeObjectSpriteRenderer = GetComponent<SpriteRenderer>();
        timeObjectSpriteRenderer.sprite = phaseSprites[initTimePhase_i];
        for (int i = 0; i <= maxTimePhase_i; i++)
        {
            phaseColliders[i].enabled = (i==initTimePhase_i) ? true : false;
        }
        currentPhase_i = initTimePhase_i;
        // TryUpdateShapeToAttachedSprite();
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

    public virtual void AddTime(int addedTime)
    {
        int newPhase = Mathf.Clamp(currentPhase_i + addedTime, 0, maxTimePhase_i);
        Update_SpriteNCollider(currentPhase_i, newPhase);
        currentPhase_i = newPhase;
        // currentPhase = Mathf.Clamp(newValue, 0, totalTimePhases);
        // timeObjectSpriteRenderer.sprite=this.GetSprite(currentPhase);
        // TryUpdateShapeToAttachedSprite();
    }

    public virtual void SubtractTime(int subtractedTime)
    {
        int newPhase = Mathf.Clamp(currentPhase_i - subtractedTime, 0, maxTimePhase_i);
        Update_SpriteNCollider(currentPhase_i, newPhase);
        currentPhase_i = newPhase;
        // currentPhase = Mathf.Clamp(newValue, 0, totalTimePhases);
        // timeObjectSpriteRenderer.sprite=this.GetSprite(currentPhase);
        // TryUpdateShapeToAttachedSprite();
    }

    private void Update_SpriteNCollider(int currentPhase, int newPhase)
    {
        timeObjectSpriteRenderer.sprite = phaseSprites[newPhase];
        phaseColliders[currentPhase].enabled = false;
        phaseColliders[newPhase].enabled = true;
    }

    public int GetCurrentTimeValue()
    {
        return currentPhase_i;
    }

    public bool CheckAddition()
    {
        return currentPhase_i < maxTimePhase_i;
    }
    public bool CheckSubtraction()
    {
        return currentPhase_i > 0;
    }

    private Sprite GetSprite(int currentTime)
    {
        return this.phaseSprites[currentTime];
    }

    /*

    // These two functions are used to update the collider based on png of sprite
    
    // Unity Reference: https://answers.unity.com/questions/722748/refreshing-the-polygon-collider-2d-upon-sprite-cha.html
    public void TryUpdateShapeToAttachedSprite ()
    {
        PolygonCollider2D thisObjectsCollider = this.GetComponent<PolygonCollider2D>();
        this.UpdateShapeToSprite(thisObjectsCollider, thisObjectsCollider.GetComponent<SpriteRenderer>().sprite);

        // The collider is set to be a trigger if we want the player to be able to walk past it
        thisObjectsCollider.isTrigger = !colliderOn[currentPhase];
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
    */
}
