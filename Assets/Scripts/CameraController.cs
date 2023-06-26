using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


// This script should be attached to the Main Camera with two 2D Virtual Camera component
// from Cinemachine. To change the switch speed, go to
// CinemachineBrain > Default Blend > Ease In Out > s

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera goalCam;
    private float initOrthoSize;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.canCtrl = false;
        initOrthoSize = playerCam.m_Lens.OrthographicSize;
        playerCam.Priority = 10;
        goalCam.Priority = 0;
        StartCoroutine(LookAtGoal());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.M))
        {
            playerCam.m_Lens.OrthographicSize = 15;
        }
        else
        {
            playerCam.m_Lens.OrthographicSize = initOrthoSize;
        }
    }

    private IEnumerator LookAtGoal()
    {
        yield return new WaitForSeconds(0.5f);
        playerCam.Priority = 0;
        goalCam.Priority = 10;
        yield return new WaitForSeconds(3f);
        playerCam.Priority = 10;
        goalCam.Priority = 0;
        PlayerController.canCtrl = true;
    }
}
