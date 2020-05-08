using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterController : MonoBehaviour, IEffectWhenDamaged
{

    public float viewRadius = 5f;
    public float smoothRotation = 5f;
    public float chaseSpeed;
    float distance;

    public TransformValue playerPos;

    float currentSpeed;

    public bool attacking;
    public bool following;
    public bool chasing;

    Transform target;
    NavMeshAgent agent;
    Animator animator;

    ShootAI shootAI;
    ChaseTrigger chaseTrigger;

    public Transform shootPoint;

    public EnemyHealthBarManager HealthBarManager;
    Damageable entityHP;


    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        shootAI = GetComponentInChildren<ShootAI>();
        chaseTrigger = GameObject.Find("GameManager").GetComponent<ChaseTrigger>();

        entityHP = gameObject.GetComponent<Damageable>();
        HealthBarManager.SetMaxHealth(entityHP.maxHp);
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
            //Iniciamos persecución
            following = true;
            agent.speed = chaseSpeed;
            //Mirar al jugador (si no está atacando)
            FaceTarget();
            currentSpeed = Mathf.Lerp(currentSpeed, agent.speed, Time.deltaTime * 1.5f);
            animator.SetFloat("rangeSpeed", currentSpeed);
            //Detectar obstaculos entre el enemigo y el jugador
            //DetectWallBetweenPlayer();
            if (!attacking)
                agent.enabled = true;
            if (distance <= agent.stoppingDistance)
            {
                //Realizar ataque
                animator.SetBool("attackPlayer", true);
            }
            else
            {
                //Desactivar animación de ataque
                animator.SetBool("attackPlayer", false);
                if (!attacking)
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

        HealthBarManager.SetHealth(entityHP.getHp());
        if (entityHP.getHp() <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    void FaceTarget()
    {
        if (!attacking || following)
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

        if (Physics.Raycast(shootPoint.position, direction, out shootHit, distance))
        {
            if (shootHit.collider.CompareTag("Player"))
            {
                Debug.Log("HOLA");
                agent.stoppingDistance = 12f;
            }
            else if(shootHit.collider.CompareTag("Enemy"))
            {
                Debug.Log("HAY UN ENEMIGO DELANTE");
                agent.stoppingDistance = 12f;
            }
            else
            {
                Debug.Log("DONDE ESTAS");
                agent.stoppingDistance = 0.1f;
            }
        }
    }
    

    public void shoot()
    {
        shootAI.ShootProjectile();
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
        Gizmos.color = Color.yellow;
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
