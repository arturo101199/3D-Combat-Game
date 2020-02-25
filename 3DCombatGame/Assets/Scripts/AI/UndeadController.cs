﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UndeadController : MonoBehaviour
{

    public float viewRadius = 5f;
    public float smoothRotation = 5f;

    Transform target;
    NavMeshAgent agent;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; //Provisional. Habrá que almacenar la ubicación del jugador con ScripObj.

        float distance = Vector3.Distance(target.position, transform.position); //Poco eficiente.

        if(distance <= viewRadius)
        {
            agent.enabled = true;
            animator.SetBool("isWalking", true);
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                //Realizar ataque normal
                animator.SetBool("attackPlayer", true);
                //Mirar al jugador
                FaceTarget();
            }
            else
            {
                animator.SetBool("attackPlayer", false);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            agent.enabled = false;
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * smoothRotation);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
