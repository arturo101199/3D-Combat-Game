using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HitData
{
    public int hitPoints;
    public Vector3 hitDirection;
    public bool makeInvulnerable;
    public bool ignoreInvulnerable;

    public HitData(int hitPoints, Vector3 hitDirection, bool makeInvulnerable, bool ignoreInvulnerable)
    {
        this.hitPoints = hitPoints;
        this.hitDirection = hitDirection;
        this.makeInvulnerable = makeInvulnerable;
        this.ignoreInvulnerable = ignoreInvulnerable;
    }
}

