using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuluthuBoss : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float damage;
    [SerializeField] Transform player;
    private Health playerHealth;
    [SerializeField] float range;
    public bool isFlipped = false;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerMask;
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped=false;
        }
        else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped=true;
        }
    }
    public void Attack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0, playerMask);
        if(hit.collider != null)
        {
            playerHealth = hit.collider.GetComponent<Health>();
            playerHealth.TakeDame(damage);
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }
}
