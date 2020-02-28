using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    Rigidbody rb;
    
    public GameObject effect;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 velocity, float speed)
    {
        rb.velocity = velocity * speed;
        //Instantiate(effect, transform.position, transform.rotation, this.transform);
    }
}
