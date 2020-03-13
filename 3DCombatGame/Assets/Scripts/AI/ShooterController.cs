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

    public GameObject bullet;

    bool attacking = false;
    public bool following = false;

    Transform target;
    NavMeshAgent agent;
    //Animator animator;

    Transform shootPoint;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        shootPoint = GetComponentInChildren<Transform>(); //Cambiar cuando pongamos el modelo

        //shootPoint.SetOwner(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        target = playerPos.GetValue();

        distance = Vector3.Distance(target.position, transform.position); //Poco eficiente.

        //animator.SetFloat("undeadSpeed", agent.velocity.magnitude/agent.speed);

        if(distance <= viewRadius)
        {
            following = true;
            //Mirar al jugador (si no está atacando)
            FaceTarget();
            //Detectar obstaculos entre el enemigo y el jugador
            DetectWallBetweenPlayer();
            if (!attacking)
                agent.enabled = true;
            if (distance <= agent.stoppingDistance)
            {
                //Realizar ataque normal
                //Activar animación de ataque
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

    void FaceTarget()
    {
        if (!attacking)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * smoothRotation);
        }
    }

    void DetectWallBetweenPlayer()
    {
        RaycastHit shootHit;
        Vector3 direction = (target.position - shootPoint.position).normalized;
        Debug.DrawRay(shootPoint.position, direction * distance, Color.red);

        if(Physics.Raycast(shootPoint.position, direction, out shootHit, distance))
        {
            Debug.Log(shootHit.collider.gameObject.name);
            if (shootHit.transform.tag == "Player")
            {
                Debug.Log("HOLA");
                agent.stoppingDistance = 8f;
            }
            else
            {
                Debug.Log("DONDE ESTAS");
                agent.stoppingDistance = 0.1f;
            }
        }
    }

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
