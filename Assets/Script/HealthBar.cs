using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;
    private Health health;
    
    public void SetMaxHealt(float healthSliderPlayer)
    {
        healthSlider.maxValue = healthSliderPlayer;
        healthSlider.value = health.CurrentHealth;
        fill.color = gradient.Evaluate(1f);
    }
    void Start()
    {
        health = GetComponent<Health>();
        SetMaxHealt(health.MaxHealth);
    }
    void Update()
    {
        SethealthSlider();
    }
    public void SethealthSlider()
    {
        healthSlider.value = health.CurrentHealth;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
}
