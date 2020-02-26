using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPositionAndRotation : MonoBehaviour
{
    public TransformValue targetPositionTransform; //Provisional
    public TransformValue targetRotationTarget;
    public float rotationSpeed = 2;
    // Update is called once per frame
    void Update()
    {
        transform.position = targetPositionTransform.value.position;
        //transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotationTarget.value.rotation;

    }
}
