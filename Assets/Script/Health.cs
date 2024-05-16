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

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

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
            if (!die)
            {
                animator.SetTrigger("death");
                die = true;
            }
        }
    }
    public void Death()
    {
        gameObject.SetActive(false);
    }
}
