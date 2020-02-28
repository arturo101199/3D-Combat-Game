using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTransform : MonoBehaviour
{
    public TransformValue transformToTrack;

    void Awake()
    {
        transformToTrack.SetValue(transform);
    }
    // Update is called once per frame
    void Update()
    {
        transformToTrack.SetValue(transform);
    }
}
