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

    public virtual void AlterTime(int deltaTime)
    {
        int newPhase = Mathf.Clamp(currentPhase_i +deltaTime, 0, maxTimePhase_i);
        Update_SpriteNCollider(currentPhase_i, newPhase);
        currentPhase_i = newPhase;
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
}
