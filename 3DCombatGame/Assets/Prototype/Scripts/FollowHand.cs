using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    public Transform hand;

    void Update()
    {
        transform.position = hand.position;
        transform.rotation = hand.rotation;
    }
}
