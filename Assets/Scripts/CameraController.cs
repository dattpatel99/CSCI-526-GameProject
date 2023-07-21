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
    public CinemachineVirtualCamera mapCam;
    private bool canPressM = false;
    private bool cameraSizeSmall = true;
    public Canvas legenCanvas;

    // Start is called before the first frame update
    void Start()
    {
        legenCanvas.enabled = false;
        PlayerController.canCtrl = false;
        playerCam.Priority = 10;
        goalCam.Priority = 0;
        StartCoroutine(LookAtGoal());
    }

    // Update is called once per frame
    void Update()
    {
        ToggleMap();
    }

    private void ToggleMap()
    {
        if (canPressM && Input.GetKeyDown(KeyCode.M))
        {
            cameraSizeSmall = !cameraSizeSmall;
            if (!cameraSizeSmall)
            {
                PlayerController.canCtrl = false;
                mapCam.Priority = 10;
                playerCam.Priority = 0;
                PlayerController.showMapIcon = true;
                ShopNpcController.shopMapIcon = true;
                FinishLine.finishMapIcon = true;
                legenCanvas.enabled = true;
            }
            else
            {
                mapCam.Priority = 0;
                playerCam.Priority = 10;
                PlayerController.canCtrl = true;
                PlayerController.showMapIcon = false;
                ShopNpcController.shopMapIcon = false;
                FinishLine.finishMapIcon = false;
                legenCanvas.enabled = false;
            }
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
        yield return new WaitForSeconds(2f);
        canPressM = true;
    }
}
