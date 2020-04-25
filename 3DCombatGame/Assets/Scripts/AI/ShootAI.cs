using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Esto se quita cuando se pongan las animaciones

public class ShootAI : MonoBehaviour
{

    private ShooterController parent;
    private ObjectPooler objectPool;

    public TransformValue playerPos; //Esto se quita cuando se pongan las animaciones

    //Para disparos en parabola
    private float gravity = 9.81f;
    public float bulletSpeed = 15f;

    private Transform shootPoint;
    private Transform target;

    float distance; //Esto se quita cuando se pongan las animaciones
    NavMeshAgent agent; //Esto se quita cuando se pongan las animaciones

    //TESTING
    bool canShoot = true;

    private void Awake()
    {
        objectPool = ObjectPooler.GetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>(); //Esto se quita cuando se pongan las animaciones
        parent = GetComponentInParent<ShooterController>();
        shootPoint = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        target = playerPos.GetValue(); //Esto se quita cuando se pongan las animaciones
        distance = Vector3.Distance(target.position, parent.transform.position); //Esto se quita cuando se pongan las animaciones

        target = playerPos.GetValue();
        Vector3 direction = (target.position - this.transform.position).normalized;
        //Mover cabeza para disparar
        float? angle = AimHead();
        if (distance <= agent.stoppingDistance) //Lo correcto sería parent.attacking, pero hasta que no se pongan las animaciones no se puede
        {
            //Activar animación de ataque
            //Realizar ataque a distancia
            if (angle != null && Vector3.Angle(direction, parent.transform.forward) < 30)
            {
                ShootProjectile();
            }
        }
    }

    //TESTING
    void CanShootAgain()
    {
        canShoot = true;
    }

    void ShootProjectile()
    {
        //TESTING
        if (canShoot)
        {
            //Instance prefab from object pool
            GameObject projectile = objectPool.SpawnObject("Bullet", shootPoint.position, shootPoint.rotation);

            //Shoot the projectile in the direction we're facing
            projectile.GetComponent<Rigidbody>().velocity = bulletSpeed * this.transform.forward;

            canShoot = false;
            Invoke("CanShootAgain", 0.5f);
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
