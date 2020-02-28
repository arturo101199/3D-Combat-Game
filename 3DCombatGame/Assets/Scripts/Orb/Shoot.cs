using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public TransformValue CameraTransform;
    public GameObject shotPrefab;
    public float shotSpeed = 10f;

    Vector3 cameraZAxis;

    // Update is called once per frame
    void Update()
    {
        float y = CameraTransform.value.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, y, 0f);
        if (Input.GetButtonDown("Fire2"))
        {
            camDirection();
            /*Shot shot = */Instantiate(shotPrefab, transform.position, transform.rotation)/*.GetComponent<Shot>()*/;
            //shot.SetVelocity(transform.forward, shotSpeed);
        }
    }

    void camDirection()
    {
        cameraZAxis = Camera.main.transform.forward;
        cameraZAxis.y = 0f;

        cameraZAxis = cameraZAxis.normalized;

    }
}
