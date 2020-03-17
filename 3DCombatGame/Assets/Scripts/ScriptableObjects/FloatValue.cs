using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject
{
    float value;

    public float GetValue()
    {
        return value;
    }
    public void SetValue(float value)
    {
        this.value = value;
    }
}
