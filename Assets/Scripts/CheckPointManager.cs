using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private static List<Transform> _checkpoints = new List<Transform>();
    void Start()
    {
        Transform parentTransform = gameObject.transform;
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            _checkpoints.Add(parentTransform.GetChild(i).gameObject.transform);
        }
    }
}
