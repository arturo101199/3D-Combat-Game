using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UpdateZOffset : MonoBehaviour
{
    public DistanceBetweenPoints distanceBetweenPoints;
    public float minOffset;
    public float maxOffset;

    float distance;
    CinemachineCameraOffset cinemachineCameraOffset;

    void Awake()
    {
        cinemachineCameraOffset = GetComponent<CinemachineCameraOffset>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = distanceBetweenPoints.distance;
        distance /= 10f;
        cinemachineCameraOffset.m_Offset.z = Mathf.Lerp(maxOffset, minOffset, Mathf.Clamp(distance, 0f, 1f));
    }
}
