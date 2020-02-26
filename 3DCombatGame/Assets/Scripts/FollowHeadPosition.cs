﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeadPosition : MonoBehaviour
{
    public Transform target; //Provisional
    public float rotationSpeed = 2;
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);

    }
}
