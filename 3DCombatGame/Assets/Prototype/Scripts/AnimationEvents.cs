using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public PlayerController playerController;

    public void AttackStart()
    {
        playerController.AttackStart();
    }

    public void AttackEnd()
    {
        playerController.AttackEnd();
    }

    public void BeginCombo()
    {
        playerController.BeginCombo();
    }

    public void EndCombo()
    {
        playerController.EndCombo();
    }

    public void BeginDash()
    {
        playerController.BeginDash();
    }

    public void EndDash()
    {
        playerController.EndDash();
    }
}
