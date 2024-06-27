using System.Collections;



using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    private Animator animator;
    private bool die = false;

    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool Die { get => die; set => die = value; }

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(currentHealth<=0)
        {
            if (!Die)
            {
                animator.SetTrigger("death");
                Die = true;
            }
        }
    }
    public void TakeDame(float Damage)
    {
        if (currentHealth > 0)
        {
            animator.SetTrigger("hit");
            currentHealth -= Damage;
        }
        else
        {
            if (!Die)
            {
                animator.SetTrigger("death");
                Die = true;
            }
        }
    }
    public void Death()
    {
        Destroy(gameObject);
    }
}
