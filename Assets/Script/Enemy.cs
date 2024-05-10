using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float dame;
    [SerializeField] private float range;
    public Rigidbody2D rb;
    public Transform detect;
    public LayerMask groundLayer;
    private bool rightFace = true;

    public float Health { get => health; set => health = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Dame { get => dame; set => dame = value; }
    public float Range { get => range; set => range = value; }

    public void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(detect.position, Vector2.right, Range, groundLayer);
        if(hit)
        {
            if(rightFace)
            {
                rightFace = false;
                transform.eulerAngles = new Vector3(0,180,0);
            }
            else
            {
                rightFace = true;
                transform.eulerAngles = new Vector3(0,0,0);
            }
        }
    }
    public void Attack()
    {
        GetComponent<Player>().TakeDame(dame);
    }
    public void TakeDame(float dame)
    {
        Health -= dame;
    }
    public void Death()
    {

    }
    public void OnDrawnGizmosSelected()
    {
        Gizmos.DrawRay(rb.position, Vector2.right * Range);
    }
}