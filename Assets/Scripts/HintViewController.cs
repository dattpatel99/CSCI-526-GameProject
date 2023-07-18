/*
using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Handle player hint view 
/// </summary>
// https://assetstore.unity.com/packages/vfx/shaders/2d-sprite-outline-109669
public class HintViewController : MonoBehaviour
{
    private GameObject[] objects;
    private Material greenOutline;
    private Material yellowOutline;
    private Material defaultMaterial;
    private Material cocoonMaterial;
    private Material cocoonGreenOutline;

    private bool held;
    
    private bool switchOutlines = false;

    public ShootMechanic shootMechanicScript;
    public long timeToTriggerHint;
    private long timestampOfLastGunHit;
    
    // Target to canvas
    public GameObject canvas;
    private TextBoxController canvasTextController;

    public Sprite button3; 

    void Start()
    {
        // Grab green outline mat
        greenOutline = Resources.Load<Material>("Green Outline");
        yellowOutline = Resources.Load<Material>("Yellow Outline");
        cocoonMaterial = Resources.Load<Material>("Cocoon Outline");
        cocoonGreenOutline = Resources.Load<Material>("Cocoon Green Outline");


        objects = GameObject.FindGameObjectsWithTag("TimeObject");

        // Grab the sprite default
        foreach (GameObject timeObject in objects)
        {
            if (timeObject.GetComponent<Renderer>() != null)
            {
                foreach (var renderer in timeObject.GetComponents<Renderer>())
                {
                    if (renderer.GetType().Name == "SpriteRenderer")
                    {
                        if (timeObject.name != "Cocoon")
                        {
                            defaultMaterial = renderer.material;
                        }
                    }
                }
            }
        }
        
        // Grab the text controller
        this.canvasTextController = canvas.GetComponent<TextBoxController>();
    }
    // Update is called once per frame
    void Update()
    {
        /*
        timestampOfLastGunHit  = shootMechanicScript.getLastTimePlayerHitObjectWithGun();
        // KEY CODE H for viewing the hint
        if (Input.GetKeyUp(KeyCode.H))
        {
            if ( held )
            {
                switchOutlines = true;
            }
            held = false;
        } 
        else if (Input.GetKeyDown(KeyCode.H))
        {
            if (!held)
            {
                switchOutlines = true;
            }
            held = true;
        }

        if ( switchOutlines )
        {
            addOutlineToTimeObjects(greenOutline, "TimeObject", held);
            addOutlineToTimeObjects(yellowOutline, "RewindObject", held);
        }


        long currentTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
        // If 
        if (currentTime - timestampOfLastGunHit > timeToTriggerHint)
        {
            StartCoroutine(displayReminderText());
            shootMechanicScript.setLastTimePlayerHitObjectWithGun(currentTime);
        }#1#
    }

    private void addOutlineToTimeObjects(Material outline, string tagName, bool outlineEnabled)
    {
        //Do on update so we don't grab destroyed objects 
        objects = GameObject.FindGameObjectsWithTag(tagName);
        foreach (GameObject timeObject in objects)
        {
            if (timeObject.GetComponent<Renderer>() != null)
            {
                foreach (var renderer in timeObject.GetComponents<Renderer>())
                {
                    if (renderer.GetType().Name == "SpriteRenderer")
                    {
                        if (timeObject.name == "Cocoon")
                        {
                            renderer.material = outlineEnabled ? cocoonGreenOutline : cocoonMaterial;
                        }
                        else
                        {
                            renderer.material = outlineEnabled ? outline : defaultMaterial;
                        }
                        //Switch between help enabled and not enabled
                    }
                }
            }
        }

        switchOutlines = false;
    }

    /*IEnumerator displayReminderText()
    {
        string textOutput = "If you are stuck, hold 'H' to view interactable objects";
        canvasTextController.SetButton3(button3);
        canvasTextController.ShowText(textOutput, true,false, false, true);
        yield return new WaitForSeconds(20.0f);
        canvasTextController.StopText(true);
    }#1#
}
*/
