﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float enableTime;
    public LayerMask targetLayers;
    public int damage;

    Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((targetLayers.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (collider != null) 
            { 
                collider.enabled = false;
                Invoke("enableCollider", enableTime);
            }
            HitData hitData = new HitData(damage, Vector3.zero, true, false);
            try
            {
                collision.collider.GetComponent<Damageable>().ApplyDamage(hitData);
            }
            catch (System.Exception ex)
            {
                print(ex.Message);
            }
            return;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((targetLayers.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (collider != null)
            {
                collider.enabled = false;
                Invoke("enableCollider", enableTime);
            }
            HitData hitData = new HitData(damage, Vector3.zero, true, false);
            try
            {
                collision.GetComponent<Damageable>().ApplyDamage(hitData);
            }
            catch (System.Exception ex)
            {
                print(ex.Message);
            }
            return;
        }
    }

    void enableCollider()
    {
        collider.enabled = true;
    }
}
