using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPatrol : MonoBehaviour
{

    UndeadController controller;
    NavMeshAgent agent;
    Animator animator;

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

            animator.SetFloat("undeadSpeed", agent.velocity.magnitude / (agent.speed * 1.7f));
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
                }
            }
        }

    }
}
