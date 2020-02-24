using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeadPosition : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 100;
    // Update is called once per frame
    void Update()
    {
        transform.position = target.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
    }
}
