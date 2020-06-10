using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadCallSoundAttack : StateMachineBehaviour
{
    private AudioManager audioManager;
    private float StepTime = 0f;
    private int attack = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        audioManager = animator.GetComponentInParent<AudioManager>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StepTime += Time.deltaTime;
        if (StepTime > 0.6 && attack<3)
        {
            StepTime = 0f;
            audioManager.Attack();
            attack += 1;
        }
        else if(StepTime > 1.25)
        {
            StepTime = 0f;
            audioManager.Attack();
            attack = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
