using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class VirtualCamera : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    private void Awake()
    {
        cam = GetComponent<CinemachineVirtualCamera>();

    }
    private void OnEnable()
    {
        PlayerRagdoll.OnPlayerDisableAnimation += FreezeCameraPosition;
        EndLineTrigger.OnPlayerReachedEndLine += FreezeCameraPosition;
        //RotateRight.OnCameraTurnRight += TurnRight;
    }

    //private void TurnRight()
    //{
    //    CinemachineTransposer transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
    //    transposer.m_FollowOffset.x = -15f;
    //    transposer.m_XDamping = 20;
    //    transposer.m_YawDamping = 20;
    //    transposer.m_ZDamping = 20;
    //    transposer.m_YawDamping = 20;

    //}

    private void FreezeCameraPosition()
    {
        //cam.LookAt = null;
        //cam.Follow = null;
        
        CinemachineTransposer transposer = cam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset.x = 0.8f;
        transposer.m_FollowOffset.y = 13.77f;
        transposer.m_XDamping = 15;
        transposer.m_YawDamping = 15;
        transposer.m_ZDamping = 15;
       
    }

    private void OnDisable()
    {
        EndLineTrigger.OnPlayerReachedEndLine -= FreezeCameraPosition;
        PlayerRagdoll.OnPlayerDisableAnimation -= FreezeCameraPosition;
        //RotateRight.OnCameraTurnRight -= TurnRight;
    }
}
