using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider health;
    public Slider stamina;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHealt(int healthPlayer)
    {
        health.maxValue = healthPlayer;
        health.value = healthPlayer;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int healthPlayer)
    {
        health.value = healthPlayer;
        fill.color = gradient.Evaluate(health.normalizedValue);
    }

    public void SetMaxStamina(int staminaPlayer)
    {
        stamina.maxValue = staminaPlayer;
        stamina.value = staminaPlayer;
    }

    public void SetStamina(int staminaPlayer)
    {
        stamina.value = staminaPlayer;
    }
}
