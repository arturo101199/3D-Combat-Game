using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int maxHp;
    public float invulnerabiltyTime;

    int currentHp;
    bool isInvulnerable;

    float timeSinceLastHit;


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

    public void ApplyDamage(HitData hitData)
    {
        if (isInvulnerable && !hitData.ignoreInvulnerable)
            return;

        currentHp -= hitData.hitPoints;

        if(hitData.makeInvulnerable)
            isInvulnerable = true;

        //Apply hit effects
        IEffectWhenDamaged obj = GetComponent<IEffectWhenDamaged>();
        if(obj != null)
        {
            obj.WhenDamaged(hitData.hitDirection);
        }
    }

    public int getHp()
    {
        return currentHp;
    }

}
