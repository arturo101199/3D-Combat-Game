using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity * 50;
    }
}
