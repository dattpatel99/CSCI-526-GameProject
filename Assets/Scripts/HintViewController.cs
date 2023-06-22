using UnityEngine;

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
    private Material thickYellowOutline;
    private bool held;
    
    private bool switchOutlines = false;

    void Start()
    {
        // Grab green outline mat
        greenOutline = Resources.Load<Material>("Green Outline");
        yellowOutline = Resources.Load<Material>("Yellow Outline");
        thickYellowOutline = Resources.Load<Material>("Yellow Outline Thick");
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
                        defaultMaterial = renderer.material;
                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
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
    }

    private void addOutlineToTimeObjects(Material outline, string tagName, bool enabled)
    {
        Debug.Log("Switching outlines");
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
                        if (tagName == "RewindObject")
                        {
                            // Thicker outlines on platforms to make them visible
                            if (timeObject.name.Contains("Platform") || timeObject.name.Contains("Sliding"))
                            {
                                renderer.material = enabled ? thickYellowOutline : defaultMaterial;
                            } 
                            else
                            {
                                renderer.material = enabled ? outline : defaultMaterial;
                            }
                        } 
                        else
                        {
                            renderer.material = enabled ? outline : defaultMaterial;
                        }
                        //Switch between help enabled and not enabled
                    }
                }
            }
        }

        switchOutlines = false;
    }
}
