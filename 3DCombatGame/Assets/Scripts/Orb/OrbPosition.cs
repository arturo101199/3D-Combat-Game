using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Vector;

public class OrbPosition : MonoBehaviour
{
    public Vector3 desiredPosition;
    public Transform OrbPivot;
    public LayerMask collisionLayer;

    //Oscilation parameters
    #region

    float maxY = 0.05f;
    float minY = -0.05f;
    float velY = 0.0f;
    float smoothTime = 1f;
    bool up;

    #endregion

    bool colliding = false;
    Vector3 direction;
    float orbDistance;
    float offsetMultiplier = 1.2f;
    float radiusOffset = 0.2f;

    const float repositionSpeed = 4f;
    const float returningSpeed = 2f;

    void Start()
    {
        direction = (transform.position - OrbPivot.position).normalized;     //direction from center of the player to the orb
        orbDistance = Vector3.Distance(transform.position, OrbPivot.position);
    }
    void Update()
    {
        //Debug.DrawRay(OrbPivot.position, (transform.position - OrbPivot.position) * offsetMultiplier, Color.red);
        Oscillate();
    }

    void FixedUpdate()
    {
        CheckCollidingFromTo(OrbPivot.position, transform.position);
        if (colliding)
        {
            float distance = Mathf.Clamp(GetAdjustedDistanceWithRayFromTo(OrbPivot.position, transform.position), radiusOffset + 0.05f, Mathf.Infinity);
            transform.localPosition = VectorUtilities.LerpXZ(transform.localPosition, direction * (distance - radiusOffset), Time.deltaTime * repositionSpeed);
        }
        else
        {
            transform.localPosition = VectorUtilities.LerpXZ(transform.localPosition, desiredPosition, Time.deltaTime * returningSpeed);
        }
    }

    void Oscillate()
    {
        if (up)
        {
            if (maxY - transform.localPosition.y > 0.01f)
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.SmoothDamp(transform.localPosition.y, maxY, ref velY, smoothTime), transform.localPosition.z);
            else up = false;
        }
        else
        {
            if (transform.localPosition.y - minY > 0.01f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, Mathf.SmoothDamp(transform.localPosition.y, minY, ref velY, smoothTime), transform.localPosition.z);
            }
            else up = true;
        }
    }


    bool CollisionDetectedFromTo(Vector3 from, Vector3 to)
    {
        Ray ray = new Ray(from, to - from);
        //Debug.DrawRay(targetPos, (position - targetPos).normalized * orbDistance * offsetMultiplier, Color.green);
        if(Physics.Raycast(ray, orbDistance * offsetMultiplier, collisionLayer))
        {
            return true;
        }
        return false;
    }

    float GetAdjustedDistanceWithRayFromTo(Vector3 from, Vector3 to)
    {
        float distance = 0;
        Ray ray = new Ray(from, to - from);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            distance = hit.distance;
        }

        return distance;
    }

    void CheckCollidingFromTo(Vector3 from, Vector3 to)
    {
        colliding = (CollisionDetectedFromTo(from, to));
    }
}
