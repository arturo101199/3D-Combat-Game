using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IEffectWhenDamaged
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
    bool inBetweenCombos;
    //float timer;
    

    public MeleeWeapon meleeWeapon;
    public BoolValue isRunningValue;
    public FloatValue currentStamina;
    public Stamina stamina;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float turnSpeed = 10f;
    public float dashCost = 20f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        charctrl = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        stamina = GetComponent<Stamina>();
        meleeWeapon.SetOwner(gameObject);
    }

    void Update()
    {
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

    Vector3 previousMovement;
    Vector3 auxMovement;

    void Move()
    {
        if (inDash)
        {
            movement = Vector3.ClampMagnitude(movement, 0.75f * runSpeed);
            return;
        }

        moveInput = playerInput.GetMoveInput();

        auxMovement = moveInput.x * cameraXAxis + moveInput.z * cameraZAxis;
        auxMovement *= currentSpeed;
        if (inBetweenCombos)
        {
            if (auxMovement.magnitude / currentSpeed > 0.1f)
                previousMovement = auxMovement;
        }
        if (!inCombo)
        {
            movement = auxMovement;

            animator.SetFloat("Speed", (movement / runSpeed).magnitude);

            if (movement.magnitude / currentSpeed > 0.05f)
                previousMovement = movement;
        }

        else
        {
            movement = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }

        if(previousMovement != Vector3.zero)
            charctrl.transform.rotation = Quaternion.Slerp(charctrl.transform.rotation, Quaternion.LookRotation(previousMovement), Time.deltaTime * turnSpeed);
    }
    void camDirection()
    {
        cameraZAxis = Camera.main.transform.forward;
        cameraXAxis = Camera.main.transform.right;

        cameraZAxis.y = 0f;
        cameraXAxis.y = 0f;

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

            SetOrientationDirectly();

            animator.SetTrigger("Attack");
        }
    }

    void Dash()
    {
        if (playerInput.GetDashInput() && !inDash && movement.magnitude/walkSpeed > 0.8f)
        {
            if(currentStamina.GetValue() > 0f)
            {
                SetOrientationDirectly();
                animator.SetTrigger("Dash");
                stamina.WasteStamina(dashCost);
            }
        }

    }

    void SetOrientationDirectly()
    {
        Vector3 direction = new Vector3(movement.x, 0f, movement.z);
        if (direction != Vector3.zero)
            charctrl.transform.rotation = Quaternion.LookRotation(direction);
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


    public void WhenDamaged(Vector3 direction)
    {
        throw new NotImplementedException();
    }

    #region AnimationEvents
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
    }

    public void EndCombo()
    {
        inCombo = false;
    }

    public void BeginDash()
    {
        inDash = true;
    }
    
    public void EndDash()
    {
        inDash = false;
    }
    
    public void InBetweenCombos()
    {
        inBetweenCombos = true;
    }
    
    public void NotInBetweenCombos()
    {
        inBetweenCombos = false;
    }
    #endregion
}
