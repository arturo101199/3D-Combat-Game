using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float gravity = 9.8f;

    PlayerInput playerInput;
    CharacterController charctrl;
    Animator animator;
    Vector3 moveInput;
    Vector3 cameraXAxis;
    Vector3 cameraZAxis;
    Vector3 movement;
    float fallVelocity;
    bool inCombo;
    bool inDash;
    float timer;
    

    public MeleeWeapon meleeWeapon;
    public float speed = 5f;
    public float jumpForce = 5f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        charctrl = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        meleeWeapon.SetOwner(gameObject);
    }

    void Update()
    {
        /*timer += Time.unscaledDeltaTime;
        if (timer > 0.05f)
            Time.timeScale = 1;*/
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Dash");

        animator.SetFloat("StateTime", Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));

        camDirection();

        Move();

        SetGravity();

        Jump();

        Dash();

        Attack();

        charctrl.Move(movement * Time.deltaTime);
    }

    void Move()
    {
        if (inDash)
            return;
        if (!inCombo)
        {
            moveInput = playerInput.GetMoveInput();

            movement = moveInput.x * cameraXAxis + moveInput.z * cameraZAxis;
            movement *= speed;
            animator.SetFloat("Walk", (movement/speed).magnitude);

            charctrl.transform.LookAt(charctrl.transform.position + movement);
        }

        else
        {
            movement = Vector3.zero;
        }
    }
    void camDirection()
    {
        cameraZAxis = Camera.main.transform.forward;
        cameraXAxis = Camera.main.transform.right;

        cameraZAxis.y = 0f;
        cameraXAxis.y = 0f;

        cameraZAxis = cameraZAxis.normalized;
        cameraXAxis = cameraXAxis.normalized;
    }

    void SetGravity()
    {

        if (charctrl.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movement.y = fallVelocity;
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movement.y = fallVelocity;
        }
    }

    void Jump()
    {
        if (charctrl.isGrounded && playerInput.GetJumpInput())
        {
            fallVelocity = jumpForce;
            movement.y = fallVelocity;
        }
    }

    void Attack()
    {
        if (playerInput.GetAttackInput())
        {
            animator.SetTrigger("Attack");
        }
    }

    void Dash()
    {
        if (playerInput.GetDashInput() && movement.magnitude/speed > 0.8f)
        {
            animator.SetTrigger("Dash");
        }

    }

    public void AttackStart()
    {
        meleeWeapon.BeginAttack();
    }

    public void AttackEnd()
    {
        meleeWeapon.EndAttack();
        /*Time.timeScale = 0f;
        timer = 0f;*/
    }

    public void BeginCombo()
    {
        inCombo = true;
    }

    public void EndCombo()
    {
        inCombo = false;
    }

    public void BeginDash()
    {
        inDash = true;
        print("beginDash");
    }
    
    public void EndDash()
    {
        inDash = false;
        print("endDash");
    }
}
