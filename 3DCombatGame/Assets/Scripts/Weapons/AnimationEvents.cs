using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    PlayerController playerController;

    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    public void StartAttackCollision()
    {
        playerController.StartAttackCollision();
    }

    public void EndAttackCollision()
    {
        playerController.EndAttackCollision();
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

    public void InBetweenCombos()
    {
        playerController.InBetweenCombos();
    }

    public void NotInBetweenCombos()
    {
        playerController.NotInBetweenCombos();
    }
}
