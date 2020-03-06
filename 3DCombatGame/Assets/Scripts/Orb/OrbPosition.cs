using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPosition : MonoBehaviour
{
    public Vector3 desiredPosition;
    
    float maxY = 0.05f;
    float minY = -0.05f;
    float velY = 0.0f;
    float smoothTime = 1f;
    bool up;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.localPosition.x - desiredPosition.x) > 0.05f || Mathf.Abs(transform.localPosition.z - desiredPosition.z) > 0.05f)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, desiredPosition, Time.deltaTime * 3f);
        }
        else
        {
            if (up)
            {
                if (maxY - transform.localPosition.y > 0.01f)
                    transform.localPosition = new Vector3(transform.localPosition.x, Mathf.SmoothDamp(transform.localPosition.y, maxY, ref velY, smoothTime), transform.localPosition.z);
                else up = false;
            }
            else
            {
                if ( transform.localPosition.y - minY > 0.01f)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, Mathf.SmoothDamp(transform.localPosition.y, minY, ref velY, smoothTime), transform.localPosition.z);
                }
                else up = true;
            }
        }
    }
}
