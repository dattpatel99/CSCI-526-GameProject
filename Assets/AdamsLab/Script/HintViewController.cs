using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintViewController : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject[] timeObjects;
    Material greenOutline;
    void Start()
    {
        // Grab green outline mat
        greenOutline = Resources.Load<Material>("Green Outline");
    }

    // Update is called once per frame
    void Update()
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
                        // Replace Sprite Default with Green Outline
                        renderer.material = greenOutline;
                    }
                }
            }
        }
    }
}
