using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpeed : MonoBehaviour
{
    public BoolValue isRunning;
    
    DragonAnimation dragonAnimation;
    float minSpeed = 1f;
    float maxSpeed = 2f;
    float currentSpeed;

    void Awake()
    {
        dragonAnimation = GetComponent<DragonAnimation>();    
    }

    void Update()
    {
        if (isRunning.value)
        {
            if (maxSpeed - currentSpeed > 0.01)
                currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * 3f);
            else
                currentSpeed = maxSpeed;
        }

        else
        {
            if (currentSpeed - minSpeed > 0.01)
                currentSpeed = Mathf.Lerp(currentSpeed, minSpeed, Time.deltaTime * 3f);
            else
                currentSpeed = minSpeed;
        }
        dragonAnimation.UpdateSpeed(currentSpeed);
    }
}
