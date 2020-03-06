using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPivotPositionAndRotation : MonoBehaviour
{
    public TransformValue targetPositionTransform;
    public TransformValue targetRotationTransform;
    public float rotationSpeed = 2;
    
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = targetPositionTransform.GetValue().position;

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetPositionTransform.value.rotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, targetRotationTransform.GetValue().rotation.eulerAngles.y, 0f);

    }
}
