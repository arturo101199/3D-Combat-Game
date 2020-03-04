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
    public bool following = false;

    Transform target;
    Transform playerTarget;
    NavMeshAgent agent;
    Animator animator;

    MeleeWeapon weapon;

    private GameObject[] hitPoints;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<MeleeWeapon>();

        weapon.SetOwner(this.gameObject);
        hitPoints = GameObject.FindGameObjectsWithTag("HitPoint");
    }

    // Update is called once per frame
    void Update()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform; //Provisional. Habrá que almacenar la ubicación del jugador con ScripObj.

        float distance = Vector3.Distance(playerTarget.position, transform.position); //Poco eficiente.

        animator.SetFloat("undeadSpeed", agent.velocity.magnitude/agent.speed);

        if(distance <= viewRadius)
        {
            //if(!following)
            findClosestHitPoint();
            following = true;
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
            following = false;
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * 1.5f);
            animator.SetFloat("undeadSpeed", currentSpeed/agent.speed);
            //animator.SetBool("isWalking", false);
            //agent.enabled = false;
        }
    }

    void FaceTarget()
    {
        if (!attacking)
        {
            Vector3 direction = (playerTarget.position - transform.position).normalized;

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

    void findClosestHitPoint()
    {
        float closest = 1000f;
        float aux;

        foreach (GameObject i in hitPoints)
        {
            aux = Vector3.Distance(i.transform.position, transform.position);
            if(aux < closest)
            {
                closest = aux;
                if (!i.GetComponent<HitPoint>().taken)
                {
                    target = i.transform;
                    //if(Vector3.Distance(transform.position, target.position) < 2f)
                    i.GetComponent<HitPoint>().ChangeState();
                }
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
