using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHp;
    public float invulnerabiltyTime;

    int currentHp;
    bool isInvulnerable;

    Rigidbody rb;
    float timeSinceLastHit;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        currentHp = maxHp;
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

        currentHp -= hitPoints;
        isInvulnerable = true;

        //Apply hit effects
        IEffectWhenDamaged obj = GetComponent<IEffectWhenDamaged>();
        if(obj != null)
        {
            obj.WhenDamaged(hitDirection);
        }
    }

}
