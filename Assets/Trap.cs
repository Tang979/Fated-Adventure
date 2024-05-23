using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Health playerHealth;
    float cdTrap = 1, cooldown = 0;
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (cooldown == 0)
            {
                playerHealth = collider.GetComponent<Health>();
                playerHealth.TakeDame(5);
            }
            cooldown += Time.deltaTime;
            if (cooldown >= cdTrap)
            {
                cooldown = 0;
            }
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        cooldown = 0;
    }
}
