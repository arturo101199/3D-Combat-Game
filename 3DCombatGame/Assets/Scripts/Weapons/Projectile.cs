﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask targetLayers;
    public int damage;

    SphereCollider collider;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((targetLayers.value & (1 << collision.gameObject.layer)) != 0)
        {
            collider.enabled = false;
            Invoke("enableCollider", 4);
            print("Orbshot hit");
            HitData hitData = new HitData(damage, Vector3.zero, true, false);
            try
            {
                collision.collider.GetComponent<Damageable>().ApplyDamage(hitData);
            }
            catch (System.Exception)
            {
                print("Missing: Damageable");
            }
            return;
        }
    }

    void enableCollider()
    {
        collider.enabled = true;
    }
}
