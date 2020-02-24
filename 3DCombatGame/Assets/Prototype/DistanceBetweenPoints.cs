using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DistanceBetweenPoints : MonoBehaviour
{
    public float distance;
    public CinemachineTargetGroup targets;

    void Awake()
    {
        targets = GetComponent<CinemachineTargetGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(targets.m_Targets[0].target.position, targets.m_Targets[1].target.position);
    }
}
