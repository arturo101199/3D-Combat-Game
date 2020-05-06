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

    bool isUndead;
    bool isRanged;

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
                isUndead = true;
                break;
            case "EnemyRange":
                range = GetComponent<ShooterController>();
                agent = range.GetComponent<NavMeshAgent>();
                //animator = range.GetComponent<Animator>();
                isRanged = true;
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
        if(isUndead)
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

        else if(isRanged)
            if (!range.following)
            {
                Debug.Log("ESTO FUNCIONA?");
                //animator.SetFloat("undeadSpeed", 0.5f);
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
                        //currentSpeed = Mathf.Lerp(currentSpeed, 0f, Time.deltaTime * 1.5f);
                        //animator.SetFloat("undeadSpeed", currentSpeed / agent.speed);
                    }
                }
            }

    }
}
