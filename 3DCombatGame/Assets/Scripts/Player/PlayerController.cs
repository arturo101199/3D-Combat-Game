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
    float currentSpeed;
    bool inCombo;
    bool inDash;
    bool isRunning;
    //float timer;
    

    public MeleeWeapon meleeWeapon;
    public BoolValue isRunningValue;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

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

        isRunningValue.value = isRunning;

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Dash");

        animator.SetFloat("StateTime", Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));

        camDirection();

        Run();

        Move();

        SetGravity();

        //Jump();

        Dash();

        Attack();

        charctrl.Move(movement * Time.deltaTime);
    }

    void Move()
    {
        if (inDash)
        {
            movement = Vector3.ClampMagnitude(movement, 0.75f * runSpeed); //this is for not doing a longer dash when you are on runSpeed
            return;
        }
        if (!inCombo)
        {
            moveInput = playerInput.GetMoveInput();

            movement = moveInput.x * cameraXAxis + moveInput.z * cameraZAxis;
            movement *= currentSpeed;
            animator.SetFloat("Speed", (movement/runSpeed).magnitude);

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

    /*void Jump()
    {
        if (charctrl.isGrounded && playerInput.GetJumpInput())
        {
            fallVelocity = jumpForce;
            movement.y = fallVelocity;
        }
    }*/

    void Attack()
    {
        if (playerInput.GetAttackInput())
        {
            animator.SetTrigger("Attack");
        }
    }

    void Dash()
    {
        if (playerInput.GetDashInput() && movement.magnitude/walkSpeed > 0.8f)
        {
            animator.SetTrigger("Dash");
        }

    }

    void Run()
    {
        if (playerInput.GetRunInput())
        {
            if (movement.magnitude / walkSpeed > 0.8f)
            {
                isRunning = true;
                if (runSpeed - currentSpeed > 0.01f)
                    currentSpeed = Mathf.Lerp(currentSpeed, runSpeed, Time.deltaTime * 3f);
                else
                    currentSpeed = runSpeed;
            }
            else if (movement.magnitude / runSpeed <= 0.5f)
                isRunning = false;

        }
        else
        {
            isRunning = false;
            if (currentSpeed - walkSpeed > 0.01f)
                currentSpeed = Mathf.Lerp(currentSpeed, walkSpeed, Time.deltaTime * 3f);
            else
                currentSpeed = walkSpeed;
        }
        animator.SetBool("IsRunning", isRunning);
    }

    public void StartAttackCollision()
    {
        meleeWeapon.BeginAttack();
    }

    public void EndAttackCollision()
    {
        meleeWeapon.EndAttack();
        /*Time.timeScale = 0f;
        timer = 0f;*/
    }

    public void BeginCombo()
    {
        inCombo = true;
        Debug.Log("Begin Combo");
    }

    public void EndCombo()
    {
        inCombo = false;
        Debug.Log("End Combo");
    }

    public void BeginDash()
    {
        inDash = true;
    }
    
    public void EndDash()
    {
        inDash = false;
    }
}
