using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Esto se quita cuando se pongan las animaciones

public class ShootAI : MonoBehaviour
{

    private ShooterController parent;
    private ObjectPooler objectPool;

    public TransformValue playerPos;

    //Para disparos en parabola
    private float gravity = 9.81f;
    public float bulletSpeed = 15f;

    private Transform shootPoint;
    private Transform target;

    float? angle;
    Vector3 direction;

    private void Awake()
    {
        objectPool = ObjectPooler.GetInstance();
        parent = GetComponentInParent<ShooterController>();
        shootPoint = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        target = playerPos.GetValue();

        direction = (target.position - this.transform.position).normalized;
        //Mover cabeza para disparar
        angle = AimHead();
    }

    public void ShootProjectile()
    {
        if (parent.attacking)
            if (angle != null && Vector3.Angle(direction, parent.transform.forward) < 30)
            {
                //Instance prefab from object pool
                GameObject projectile = objectPool.SpawnObject("Bullet", shootPoint.position, shootPoint.rotation);

                //Shoot the projectile in the direction we're facing
                projectile.GetComponent<Rigidbody>().velocity = bulletSpeed * this.transform.forward;
            }
    }

    private float? AimHead() //OJO: Esta parte del código deberá estar en su cabeza para que sea la única parte que se mueva aquí
    {
        float? angle = CalculateAngle();

        if (angle != null)
        {
            this.transform.localEulerAngles = new Vector3(360f - (float)angle, 0f, 0f);
        }

        return angle;
    }

    private float? CalculateAngle()
    {
        Vector3 targetDir = target.transform.position - this.transform.position;
        float y = targetDir.y;
        targetDir.y = 0f;
        float x = targetDir.magnitude;
        float sSqr = bulletSpeed * bulletSpeed;
        float underTheSqrRoot = (sSqr * sSqr) - gravity * (gravity * x * x + 2 * y * sSqr);

        if (underTheSqrRoot >= 0f)
        {
            float root = Mathf.Sqrt(underTheSqrRoot);
            float angle = sSqr - root; //Siempre cogeremos el ángulo negativo

            return (Mathf.Atan2(angle, gravity * x) * Mathf.Rad2Deg);
        }
        else
        {
            return null;
        }
    }
}
