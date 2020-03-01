using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public TransformValue CameraTransform;
    public GameObject shotPrefab;
    public float shotSpeed = 10f;
    public float fireRate = 1f;

    Vector3 cameraZAxis;
    float timer;

    void Start()
    {
        timer = 1/fireRate;    
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float y = CameraTransform.GetValue().rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, y, 0f);

        if (Input.GetButtonDown("Fire2") && timer > 1/fireRate)
        {
            camDirection();
            Instantiate(shotPrefab, transform.position, transform.rotation);
            timer = 0f;
        }
    }

    void camDirection()
    {
        cameraZAxis = Camera.main.transform.forward;
        cameraZAxis.y = 0f;

    }
}
