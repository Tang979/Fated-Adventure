using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class statusBar : MonoBehaviour
{
    public Slider health;
    public Slider stamina;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHealt(float healthPlayer)
    {
        health.maxValue = healthPlayer;
        health.value = healthPlayer;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float healthPlayer)
    {
        health.value = healthPlayer;
        fill.color = gradient.Evaluate(health.normalizedValue);
    }

    public void SetMaxStamina(float staminaPlayer)
    {
        stamina.maxValue = staminaPlayer;
        stamina.value = staminaPlayer;
    }

    public void SetStamina(float staminaPlayer)
    {
        stamina.value = staminaPlayer;
    }
}
