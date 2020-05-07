using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 1;

    public AttackPoint[] attackPoints = new AttackPoint[0];
    public LayerMask targetLayers;

    Vector3[] previousPos = null;
    GameObject owner;
    bool inAttack;

    static RaycastHit[] s_RaycastHitCache = new RaycastHit[32];

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inAttack)
        {
            for (int i = 0; i < attackPoints.Length; ++i)
            {
                AttackPoint pts = attackPoints[i];

                Vector3 worldPos = pts.attackRoot.position + pts.attackRoot.TransformVector(pts.offset);
                Vector3 attackVector = worldPos - previousPos[i];

                if (attackVector.magnitude < 0.001f)
                {
                    // A zero vector for the sphere cast don't yield any result, even if a collider overlap the "sphere" created by radius. 
                    // so we set a very tiny microscopic forward cast to be sure it will catch anything overlaping that "stationary" sphere cast
                    attackVector = Vector3.forward * 0.0001f;
                }


                Ray r = new Ray(worldPos, attackVector.normalized);

                int contacts = Physics.SphereCastNonAlloc(r, pts.radius, s_RaycastHitCache, attackVector.magnitude,
                    targetLayers.value,
                    QueryTriggerInteraction.Ignore);

                for (int k = 0; k < contacts; ++k)
                {
                    Collider col = s_RaycastHitCache[k].collider;

                    if (col != null)
                    {
                        CheckDamage(col);
                    }
                }

                previousPos[i] = worldPos;

#if UNITY_EDITOR
                pts.previousPositions.Add(previousPos[i]);
#endif
            }
        }
    }

    public void BeginAttack()
    {
        inAttack = true;

        previousPos = new Vector3[attackPoints.Length];

        for (int i = 0; i < attackPoints.Length; ++i)
        {
            Vector3 worldPos = attackPoints[i].attackRoot.position +
                               attackPoints[i].attackRoot.TransformVector(attackPoints[i].offset);
            previousPos[i] = worldPos;

#if UNITY_EDITOR
            attackPoints[i].previousPositions.Clear();
            attackPoints[i].previousPositions.Add(previousPos[i]);
#endif
        }
    }

    public void EndAttack()
    {
        inAttack = false;


#if UNITY_EDITOR
        for (int i = 0; i < attackPoints.Length; ++i)
        {
            attackPoints[i].previousPositions.Clear();
        }
#endif
    }

    public void CheckDamage(Collider other)
    {
        if ((targetLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            Vector3 hitDirection = (other.transform.position - owner.transform.position).normalized;
            HitData hitData = new HitData(damage, hitDirection, true, false);
            try
            {
                other.GetComponent<Damageable>().ApplyDamage(hitData);
            }
            catch (Exception)
            {
                print("Missing: Damageable");
            }
            return;
        }


    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < attackPoints.Length; ++i)
        {
            AttackPoint pts = attackPoints[i];

            if (pts.attackRoot != null)
            {
                Vector3 worldPos = pts.attackRoot.TransformVector(pts.offset);
                Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
                Gizmos.DrawSphere(pts.attackRoot.position + worldPos, pts.radius);
            }

            if (pts.previousPositions.Count > 1)
            {
                UnityEditor.Handles.DrawAAPolyLine(40, pts.previousPositions.ToArray());
            }
        }
    }

#endif
}
