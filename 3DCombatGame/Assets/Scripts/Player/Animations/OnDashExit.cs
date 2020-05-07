using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDashExit : StateMachineBehaviour
{
    AnimationEvents animationEvents;

    void Awake()
    {
        animationEvents = FindObjectOfType<AnimationEvents>();
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animationEvents != null)
            animationEvents.EndDash();
        else
        {
            Debug.LogWarning("Animation Event not given");
        }
    }


}
