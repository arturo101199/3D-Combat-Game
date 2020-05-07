using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{

    UndeadController undead;
    ShooterController range;
    NavMeshAgent agent;
    Animator animator;

    float currentSpeed;

    public float waitTime;
    public float startWaitTime;

    public Transform[] movePoints;
    private int randomPoint;


    // Start is called before the first frame update
    void Start()
    {
        switch (this.tag)
        {
            case "EnemyUndead":
                undead = GetComponent<UndeadController>();
                agent = undead.GetComponent<NavMeshAgent>();
                animator = undead.GetComponent<Animator>();
                break;
            case "EnemyRange":
                range = GetComponent<ShooterController>();
                agent = range.GetComponent<NavMeshAgent>();
                animator = range.GetComponent<Animator>();
                break;
            default:
                break;
        }
        randomPoint = Random.Range(0, movePoints.Length);
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.tag)
        {
            case "EnemyUndead":
                if (!undead.following)
                {
                    animator.SetFloat("undeadSpeed", 0.5f);
                    agent.SetDestination(movePoints[randomPoint].position);

                    if (Vector3.Distance(transform.position, movePoints[randomPoint].position) < 1f)
                    {
                        if (waitTime <= 0f)
                        {
                            randomPoint = Random.Range(0, movePoints.Length);
                            waitTime = startWaitTime;
                            agent.SetDestination(movePoints[randomPoint].position);
                        }
                        else
                        {
                            waitTime -= Time.deltaTime;
                            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * 1.5f);
                            animator.SetFloat("undeadSpeed", currentSpeed / agent.speed);
                        }
                    }
                }
            break;
            case "EnemyRange":
                if (!range.following)
                {
                    agent.stoppingDistance = 1;
                    animator.SetFloat("rangeSpeed", 0.5f);
                    agent.SetDestination(movePoints[randomPoint].position);

                    if (Vector3.Distance(transform.position, movePoints[randomPoint].position) < 1f)
                    {
                        if (waitTime <= 0f)
                        {
                            randomPoint = Random.Range(0, movePoints.Length);
                            waitTime = startWaitTime;
                            agent.SetDestination(movePoints[randomPoint].position);
                        }
                        else
                        {
                            waitTime -= Time.deltaTime;
                            currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * 1.5f);
                            animator.SetFloat("rangeSpeed", currentSpeed / agent.speed);
                        }
                    }
                }
                else
                    agent.stoppingDistance = 12;
                break;
            default:
                break;
        }      
    }
}
