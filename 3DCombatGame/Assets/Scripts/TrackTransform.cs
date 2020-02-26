using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTransform : MonoBehaviour
{
    public TransformValue transformToTrack;

    // Update is called once per frame
    void Update()
    {
        transformToTrack.value = transform;
    }
}
