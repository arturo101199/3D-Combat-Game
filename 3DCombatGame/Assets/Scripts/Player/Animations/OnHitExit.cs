using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitExit : StateMachineBehaviour
{
    AnimationEvents animationEvents;

    void Awake()
    {
        animationEvents = FindObjectOfType<AnimationEvents>();
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animationEvents != null)
            animationEvents.BeginHit();
        else
        {
            Debug.LogWarning("Animation Event not given");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animationEvents != null) 
        { 
            animationEvents.EndHit();
        }
        else
        {
            Debug.LogWarning("Animation Event not given");
        }
    }
}
