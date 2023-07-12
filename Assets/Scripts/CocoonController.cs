using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocoonController : TimeObject
{
    public GameObject cocoon;
    public GameObject butterfly;
    bool opening;

    float initXPosition;
    float initYPosition;
    float initZPosition;

    private Material cocoonMaterial;
    private Material cocoonGreenOutline;

    // Start is called before the first frame update
    void Start()
    {
        cocoon.GetComponent<Transform>().localScale = getCocoonSize();
        initXPosition = cocoon.GetComponent<Transform>().localPosition.x;
        initZPosition = cocoon.GetComponent<Transform>().localPosition.z;
        initYPosition = cocoon.GetComponent<Transform>().localPosition.y;
        butterfly.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (base.currentPhase_i == base.maxTimePhase_i)
        {
            opening = true;
            butterfly.SetActive(true);
            cocoon.SetActive(false);
        }
    }

    public override void AddTime(int addedTime)
    {
        int newPhase = Mathf.Clamp(base.currentPhase_i + addedTime, 0, base.maxTimePhase_i);
        currentPhase_i = newPhase;
        cocoon.GetComponent<Transform>().localScale = getCocoonSize();
        cocoon.GetComponent<Transform>().localPosition = getCocoonPosition();
    }

    public override void SubtractTime(int subtractedTime)
    {
        int newPhase = Mathf.Clamp(base.currentPhase_i - subtractedTime, 0, base.maxTimePhase_i);
        currentPhase_i = newPhase;
        cocoon.GetComponent<Transform>().localScale = getCocoonSize();
        cocoon.GetComponent<Transform>().localPosition = getCocoonPosition();
    }

    public override void AlterTime(int deltaTime)
    {
        int newPhase = Mathf.Clamp(currentPhase_i + deltaTime, 0, maxTimePhase_i);
        currentPhase_i = newPhase;
        cocoon.GetComponent<Transform>().localScale = getCocoonSize();
        cocoon.GetComponent<Transform>().localPosition = getCocoonPosition();
    }

    public Vector3 getCocoonSize()
    {
        switch (base.currentPhase_i)
        {
            case 0:
                return new Vector3(0.013f, 0.013f, 1);
            case 1:
                return new Vector3(0.014f, 0.014f, 1);
            case 2:
                return new Vector3(0.015f, 0.015f, 1);
            default:
                // shouldn't be possible
                return new Vector3(0, 0, 0);
        }
    }

    public Vector3 getCocoonPosition()
    {
        float newYPosition = initYPosition;
        switch (base.currentPhase_i)
        {
            case 1:
                newYPosition = newYPosition + 0.05f;
                break;
            case 2:
                newYPosition = newYPosition + 0.01f;
                break;
        }

        return new Vector3(initXPosition, newYPosition, initZPosition);
    }

    public bool isOpening()
    {
        return opening;
    }

    private void OnMouseEnter()
    {
        timeObjectSpriteRenderer.material = cocoonGreenOutline;
    }

    private void OnMouseExit()
    {
        timeObjectSpriteRenderer.material = cocoonMaterial;
    }
}
