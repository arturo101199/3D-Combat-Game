using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPosition : MonoBehaviour
{

    public Vector3 desiredPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition != desiredPosition)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, desiredPosition, Time.deltaTime * 3f);
        }
    }
}
