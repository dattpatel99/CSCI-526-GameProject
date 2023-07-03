using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public void HitDestroy()
    {
        gameObject.SetActive(false);
    }
}
