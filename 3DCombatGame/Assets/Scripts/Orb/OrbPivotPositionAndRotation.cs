using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPivotPositionAndRotation : MonoBehaviour
{
    public TransformValue targetPositionTransform;
    public TransformValue targetRotationTransform;
    
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = targetPositionTransform.GetValue().position;

        transform.rotation = Quaternion.Euler(0f, targetRotationTransform.GetValue().rotation.eulerAngles.y, 0f);

    }
}
