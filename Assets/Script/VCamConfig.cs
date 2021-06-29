using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;

public class VCamConfig : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCam;

    public static VCamConfig vCam;



    void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        virtualCam.Follow = Move._transform.transform;
        virtualCam.LookAt = Move._transform.transform;
    }

}
