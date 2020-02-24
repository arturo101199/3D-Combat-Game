using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackPoint
{
    public float radius;
    public Vector3 offset;
    public Transform attackRoot;

#if UNITY_EDITOR
    //editor only as it's only used in editor to display the path of the attack that is used by the raycast
    [NonSerialized] public List<Vector3> previousPositions = new List<Vector3>();
#endif

}
