using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterController : MonoBehaviour
{

    public float viewRadius = 5f;
    public float smoothRotation = 5f;
    float distance;

    public TransformValue playerPos;

    float currentSpeed;

    public bool attacking;
    public bool following;
    public bool chasing;

    public GameObject ChaseArea;

    Transform target;
    NavMeshAgent agent;
    //Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();

        //shootPoint.SetOwner(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        target = playerPos.GetValue();

        distance = Vector3.Distance(target.position, transform.position); //Poco eficiente.

        //animator.SetFloat("undeadSpeed", agent.velocity.magnitude/agent.speed);

        if(distance <= viewRadius || chasing)
        {
            //Iniciamos persecución
            following = true;
            //Mirar al jugador (si no está atacando)
            FaceTarget();
            //Detectar obstaculos entre el enemigo y el jugador
            //DetectWallBetweenPlayer();
            if (!attacking)
                agent.enabled = true;
            if (distance <= agent.stoppingDistance)
            {
                //Activar animación de ataque
                //Realizar ataque a distancia
            }
            else
            {
                //Desactivar animación de ataque
                if(!attacking)
                    agent.SetDestination(target.position);
            }
            //currentSpeed = agent.velocity.magnitude;
        }
        else
        {
            following = false;
            //currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * 1.5f);
            //animator.SetFloat("undeadSpeed", currentSpeed/agent.speed);
        }
    }

    /*
    IEnumerator ShootProjectile()
    {
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(0.1f);

        //Instance prefab from object pool
        Transform Projectile = objectPool.SpawnObject("Bullet", shootPoint.position, Quaternion.identity).GetComponent<Transform>();

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile.position = shootPoint.position;

        // Calculate distance to target
        //float target_Distance = Vector3.Distance(Projectile.position, target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }

        //Desintegrate projectile
        Projectile.GetComponent<Bullet>().Desintegrate();

        //Shoot again
        StartCoroutine(ShootProjectile());

    }
    */

    void FaceTarget()
    {
        if (!attacking)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * smoothRotation);
        }
    }

    /*
    void DetectWallBetweenPlayer()
    {
        RaycastHit shootHit;
        Vector3 direction = (target.position - shootPoint.position).normalized;
        Debug.DrawRay(shootPoint.position, direction * distance, Color.red);

        if (Physics.Raycast(shootPoint.position, direction, out shootHit, distance))
        {
            if (shootHit.collider.CompareTag("Player"))
            {
                Debug.Log("HOLA");
                agent.stoppingDistance = 16f;
            }
            else if(shootHit.collider.CompareTag("Enemy"))
            {
                Debug.Log("HAY UN ENEMIGO DELANTE");
                agent.stoppingDistance = 16f;
            }
            else
            {
                Debug.Log("DONDE ESTAS");
                agent.stoppingDistance = 0.1f;
            }
        }
    }
    */

    public void BeginAttack()
    {
        //weapon.BeginAttack();
    }

    public void EndAttack()
    {
        //weapon.EndAttack();
    }

    public void animationStarted()
    {
        agent.enabled = false;
        attacking = true;
    }

    public void animationEnded()
    {
        agent.enabled = true;
        attacking = false;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
