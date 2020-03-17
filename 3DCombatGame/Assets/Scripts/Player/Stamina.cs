using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public FloatValue stamina;
    public float maxStamina = 100f;
    public float staminaUpSpeed = 2f;
    public float StopRegenerationTime = 1f;

    float currentStamina;
    float timer;

    void Start()
    {
        stamina.SetValue(maxStamina);
        timer = 0f;
    }

    public void WasteStamina(float staminaWasted)
    {
        currentStamina = Mathf.Clamp(currentStamina - staminaWasted, 0f, maxStamina);
        stamina.SetValue(currentStamina);
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;
        currentStamina = stamina.GetValue();
        UpStamina();
        Debug.Log(currentStamina);
    }

    void UpStamina()
    {
        if(timer > StopRegenerationTime && currentStamina < maxStamina)
        {
            currentStamina = Mathf.Clamp(currentStamina + Time.deltaTime * staminaUpSpeed, 0f, maxStamina);
        }
        stamina.SetValue(currentStamina);
    }
}
