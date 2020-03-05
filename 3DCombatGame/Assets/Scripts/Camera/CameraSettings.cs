using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    public TransformValue follow;
    public TransformValue lookAt;

    new CinemachineFreeLook camera; //Si da problemas quitar "new"

    void Awake()
    {
        camera = GetComponentInChildren<CinemachineFreeLook>();
        camera.m_Follow = follow.GetValue();
        camera.m_LookAt = lookAt.GetValue();
    }

}
