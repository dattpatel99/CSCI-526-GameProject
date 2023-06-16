using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://assetstore.unity.com/packages/vfx/shaders/2d-sprite-outline-109669
public class HintViewController : MonoBehaviour
{
    private GameObject[] timeObjects;
    private Material greenOutline;
    private Material defaultMaterial;
    private bool held;

    void Start()
    {
        // Grab green outline mat
        greenOutline = Resources.Load<Material>("Green Outline");
        defaultMaterial = Resources.Load<Material>("unity_builtin_extra/Sprites-Default");
        timeObjects = GameObject.FindGameObjectsWithTag("TimeObject");

        // Grab the sprite default
        foreach (GameObject timeObject in timeObjects)
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
        if (Input.GetKeyUp(KeyCode.H))
        {
            Debug.Log("Key not being pressed");
            held = false;
        } 
        else if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Key pressed");
            held = true;
        }

        addGreenOutlineToTimeObjects(held);
    }

    private void addGreenOutlineToTimeObjects(bool enabled)
    {
        //Do on update so we don't grab destroyed objects 
        timeObjects = GameObject.FindGameObjectsWithTag("TimeObject");
        foreach (GameObject timeObject in timeObjects)
        {
            if (timeObject.GetComponent<Renderer>() != null)
            {
                foreach (var renderer in timeObject.GetComponents<Renderer>())
                {
                    if (renderer.GetType().Name == "SpriteRenderer")
                    {
                        //Switch between help enabled and not enabled
                        renderer.material = enabled ? greenOutline : defaultMaterial;
                    }
                }
            }
        }
    }
}
