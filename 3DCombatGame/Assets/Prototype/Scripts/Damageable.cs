﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHitPoints;
    public float invulnerabiltyTime;

    int currentHitPoints;
    bool isInvulnerable;
    Rigidbody rb;
    float timeSinceLastHit;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        currentHitPoints = maxHitPoints;
    }

    void Update()
    {
        if (isInvulnerable)
        {
            timeSinceLastHit += Time.deltaTime;
            if (timeSinceLastHit > invulnerabiltyTime)
            {
                timeSinceLastHit = 0.0f;
                isInvulnerable = false;
            }
        }
    }

    public void ApplyDamage(int hitPoints, Vector3 hitDirection)
    {
        if (isInvulnerable)
            return;
        currentHitPoints -= hitPoints;
        isInvulnerable = true;
        rb.AddForce(hitDirection * 100f);
    }

}
