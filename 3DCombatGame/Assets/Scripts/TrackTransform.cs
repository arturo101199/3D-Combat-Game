using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class TrackTransform : MonoBehaviour
{
    public TransformValue transformToTrack;

    void Awake()
    {
        transformToTrack.SetValue(transform);
    }

    void Update()
    {
        transformToTrack.SetValue(transform);
    }
}
