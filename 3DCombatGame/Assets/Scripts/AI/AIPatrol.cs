using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{

    UndeadController controller;
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
        controller = GetComponent<UndeadController>();
        agent = controller.GetComponent<NavMeshAgent>();
        animator = controller.GetComponent<Animator>();
        randomPoint = Random.Range(0, movePoints.Length);
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.following)
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

    }
}
