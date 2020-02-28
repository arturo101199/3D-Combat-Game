using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TransformValue : ScriptableObject
{
    Transform value;

    public Transform GetValue()
    {
        return value;
    }
    public void SetValue(Transform value)
    {
        this.value = value;
    }
}
