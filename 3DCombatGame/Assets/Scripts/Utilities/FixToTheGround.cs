using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixToTheGround : MonoBehaviour
{

    void Start()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 10);
        transform.position = hit.point;
    }

}
