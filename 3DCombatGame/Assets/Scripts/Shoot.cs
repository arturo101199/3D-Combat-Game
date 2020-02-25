using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //public GameObject shot;

    Vector3 cameraZAxis;

    // Update is called once per frame
    void Update()
    {
        float y = Camera.main.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, y, 0);
        if (Input.GetButtonDown("Fire2"))
        {
            camDirection();
            /*shot _shot = Instantiate(shot, transform.position, transform.rotation).GetComponent<shot>();
            _shot.SetVelocity(transform.forward);*/
        }
    }

    void camDirection()
    {
        cameraZAxis = Camera.main.transform.forward;
        Debug.Log(cameraZAxis);
        cameraZAxis.y = 0f;

        cameraZAxis = cameraZAxis.normalized;

    }
}
