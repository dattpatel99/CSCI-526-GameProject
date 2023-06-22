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
    private bool held;

    void Start()
    {
        // Grab green outline mat
        greenOutline = Resources.Load<Material>("Green Outline");
        yellowOutline = Resources.Load<Material>("Yellow Outline");
        defaultMaterial = Resources.Load<Material>("unity_builtin_extra/Sprites-Default");
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
            //Debug.Log("Key not being pressed");
            held = false;
        } 
        else if (Input.GetKeyDown(KeyCode.H))
        {
            //Debug.Log("Key pressed");
            held = true;
        }

        addOutlineToTimeObjects(greenOutline, "TimeObject", held);
        addOutlineToTimeObjects(yellowOutline, "RewindObject", held);

    }

    private void addOutlineToTimeObjects(Material outline, string tagName, bool enabled)
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
                        //Switch between help enabled and not enabled
                        renderer.material = enabled ? outline : defaultMaterial;
                    }
                }
            }
        }
    }
}
