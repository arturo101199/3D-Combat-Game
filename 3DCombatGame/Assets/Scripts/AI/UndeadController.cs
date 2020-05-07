using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UndeadController : MonoBehaviour, IEffectWhenDamaged
{
    public float maxHealth = 100f;
    public EnemyHealthBarManager HealthBarManager;
    private float curretnHealth;

    public float viewRadius = 5f;
    public float smoothRotation = 5f;

    public TransformValue playerPos;

    float currentSpeed;
    float distance;

    bool attacking;
    public bool following;
    public bool chasing;

    Transform target;
    NavMeshAgent agent;
    NavMeshObstacle obstacle;
    Animator animator;

    MeleeWeapon weapon;

    ChaseTrigger chaseTrigger;

    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<MeleeWeapon>();
        chaseTrigger = GameObject.Find("GameManager").GetComponent<ChaseTrigger>();

        weapon.SetOwner(this.gameObject);

        //Inicializa la vida y establece la barra
        curretnHealth = maxHealth;
        HealthBarManager.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        target = playerPos.GetValue();

        distance = Vector3.Distance(target.position, transform.position); //Poco eficiente.

        if(distance <= viewRadius || chasing)
        {
            if (!chasing)
                chaseTrigger.createChaseArea(target);
            following = true;
            agent.speed = 2f;
            //Mirar al jugador (si no está atacando)
            FaceTarget();
            currentSpeed = Mathf.Lerp(currentSpeed, agent.speed, Time.deltaTime * 1.5f);
            animator.SetFloat("undeadSpeed", currentSpeed);
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
                if(!attacking)
                    agent.SetDestination(target.position);
            }
            currentSpeed = agent.velocity.magnitude;
        }
        else
        {
            following = false;
            agent.speed = 1f;
            if (chaseTrigger.InChaseArea(this.transform.position))
            {
                chasing = true;
                chaseTrigger.InsertInList(this.GetComponent<Collider>());
            }
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
        obstacle.enabled = true;
        attacking = true;
    }

    public void animationEnded()
    {
        agent.enabled = true;
        attacking = false;
    }

    public void DesactivateObstacle()
    {
        obstacle.enabled = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

    public void WhenDamaged(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            //Animacion de recibir daño
        }

        throw new System.NotImplementedException();
    }
}
