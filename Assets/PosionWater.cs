using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PosionWater : MonoBehaviour
{
    Health playerHealth;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            playerHealth = collider.GetComponent<Health>();
            playerHealth.TakeDame(150);
        }
    }
}
