using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLock : MonoBehaviour
{
    public CinemachineFreeLook camera;

    void Awake()
    {
        camera = GetComponentInChildren<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
       // camera.m_XAxis.Value = 42f;
    }
}
