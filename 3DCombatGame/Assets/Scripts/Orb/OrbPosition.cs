using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Vector;

public class OrbPosition : MonoBehaviour
{
    public Vector3 desiredPosition;
    public Transform targetTransform;
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

    void Start()
    {
        direction = (transform.position - targetTransform.position).normalized;
        orbDistance = Vector3.Distance(transform.position, targetTransform.position);
    }
    void Update()
    {
        //Debug.DrawRay(targetTransform.position, (transform.position - targetTransform.position) * offsetMultiplier, Color.red);
        Oscillate();
    }

    void FixedUpdate()
    {
        CheckColliding(targetTransform.position);
        if (colliding)
        {
            float distance = Mathf.Clamp(GetAdjustedDistanceWithRayFrom(targetTransform.position), radiusOffset + 0.05f, Mathf.Infinity);
            transform.localPosition = direction * (distance - radiusOffset);
            //transform.localPosition = VectorUtilities.LerpXZ(transform.localPosition, direction * (distance - radiusOffset), Time.deltaTime * 4f);
        }
        else
        {
            transform.localPosition = VectorUtilities.LerpXZ(transform.localPosition, desiredPosition, Time.deltaTime * 2f);
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


    bool CollisionDetected(Vector3 position, Vector3 targetPos)
    {
        Ray ray = new Ray(targetPos, (position - targetPos).normalized);
        //Debug.DrawRay(targetPos, (position - targetPos).normalized * orbDistance * offsetMultiplier, Color.green);
        //float distance = Vector3.Distance(position, targetPos);
        if(Physics.Raycast(ray, orbDistance * offsetMultiplier, collisionLayer))
        {
            return true;
        }
        return false;
    }

    float GetAdjustedDistanceWithRayFrom(Vector3 from)
    {
        float distance = -1;
        Ray ray = new Ray(from, transform.position - from);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            distance = hit.distance;
        }

        if (distance == -1)
            return 0;
        else return distance;
    }

    void CheckColliding(Vector3 targetPos)
    {
        colliding = (CollisionDetected(transform.position, targetPos));
    }
}
