using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Vector3 moveInput;
    bool run;
    bool attack;
    bool dash;

    void Update()
    {
        moveInput.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        run = Input.GetButton("Run");
        attack = Input.GetButton("Fire1");
        dash = Input.GetButtonDown("Fire3");
    }

    public Vector3 GetMoveInput()
    {
        moveInput = Vector3.ClampMagnitude(moveInput, 1f);
        return moveInput;
    }

    public bool GetRunInput()
    {
        return run;
    }
    
    public bool GetAttackInput()
    {
        return attack;
    }

    public bool GetDashInput()
    {
        return dash;
    }
}
