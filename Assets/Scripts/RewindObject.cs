using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class RewindObject : MonoBehaviour
{
    public Text counterText;
    public int rewindedDuration = 5;

    protected SpriteRenderer sr;
    protected bool objectRewinding;

    private Material yellowOutline;
    private Material defaultMaterial;

    // Start is called before the first frame update
    protected void Start()
    {
        objectRewinding = false;
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.white;
        counterText.enabled = false;

        yellowOutline = Resources.Load<Material>("Yellow Outline");
        defaultMaterial = sr.material;

        ChildStart();
    }

    //Unique properties in child that need to be initialized in Start
    public abstract void ChildStart();

    // function to be called in the player shoot mechanic script
    public abstract void Rewind();

    // How long should the rewind last
    protected virtual IEnumerator RewindDuration()
    {
        counterText.enabled = true;
        for (int i = rewindedDuration; i >= 1; i--)
        {
            counterText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        PlayerStatus.isRewinding = false;
        sr.color = Color.white;
        objectRewinding = false;

        counterText.enabled = false;
    }
    

    private void OnMouseEnter()
    {
        sr.material = yellowOutline;
    }

    private void OnMouseExit()
    {
        sr.material = defaultMaterial;
    }

    public int getRewindDuration()
    {
        return rewindedDuration;
    }
}
