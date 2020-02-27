using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UndeadController : MonoBehaviour
{

    public float viewRadius = 5f;
    public float smoothRotation = 5f;

    float currentSpeed;

    bool attacking = false;

    Transform target;
    NavMeshAgent agent;
    Animator animator;

    MeleeWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<MeleeWeapon>();

        weapon.SetOwner(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //Provisional. Habrá que almacenar la ubicación del jugador con ScripObj.

        float distance = Vector3.Distance(target.position, transform.position); //Poco eficiente.

        animator.SetFloat("undeadSpeed", agent.velocity.magnitude/agent.speed);

        if(distance <= viewRadius)
        {
            //Mirar al jugador (si no está atacando)
            FaceTarget();
            if (!attacking)
                agent.enabled = true;
            if (distance <= agent.stoppingDistance)
            {
                //Realizar ataque normal
                animator.SetBool("attackPlayer", true);
            }
            else
            {
                animator.SetBool("attackPlayer", false);
                //animator.SetBool("isWalking", true);
                if(!attacking)
                    agent.SetDestination(target.position);
            }
            currentSpeed = agent.velocity.magnitude;
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * 1.5f);
            animator.SetFloat("undeadSpeed", currentSpeed/agent.speed);
            //animator.SetBool("isWalking", false);
            agent.enabled = false;
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

    public void BeginAttack()
    {
        weapon.BeginAttack();
    }

    public void EndAttack()
    {
        weapon.EndAttack();
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
