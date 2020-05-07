using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider staminaSlider;

    public FloatValue stamina;
    Damageable playerHp;

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    public void takeDamage(float damage)
    {
        SetHealth(healthSlider.value - damage);
    }

    public void SetMaxStamina(float stamina)
    {
        staminaSlider.maxValue = stamina;
        staminaSlider.value = stamina;
    }

    public void SetStamina(float stamina)
    {
        staminaSlider.value = stamina;
    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        Stamina staminaManager = player.GetComponent<Stamina>();
        playerHp = player.GetComponent<Damageable>();
        SetMaxStamina(staminaManager.maxStamina);
    }

    private void Update()
    {
        SetStamina(stamina.GetValue());
        
        //Con playerHP.getHp() tienes la vida actual
        print(playerHp.getHp());
        //Con playerHp.maxHp() tienes la vida maxima
        print(playerHp.maxHp);
    }
}
