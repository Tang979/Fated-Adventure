using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class CanineController : Enemy
{
    public Transform detectPoint, player, attackRange;
    public LayerMask layerMask, playerMask;
    public Animator animator;
    public float distance = 1.5f;
    public float range = 1.5f;
    public bool facingRight = true;

    private bool canAttack = true;
    private float attackTime;
    private float cdattack = .5f;
    public override void DectectingGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(detectPoint.position, 0.1f, layerMask);
        if (hit)
        {
            if (facingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                facingRight = false;
            }
            else if (!facingRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                facingRight = true;
            }
        }
    }

    public override void Move()
    {
        if (Vector2.Distance(player.position, transform.position) <= range)
        {
            if (canAttack)
            {
                attackTime = cdattack;
                animator.SetBool("isRange", true);
                canAttack = !canAttack;
            }

        }
        else
        {
            if (attackTime <= 0)
                canAttack = !canAttack;
            attackTime -= Time.deltaTime;
            animator.SetBool("isRange", false);
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
    }

    public void TakeDame(float dame)
    {
        Health -= dame;
        animator.SetTrigger("hit");
    }

    public void Death()
    {
        gameObject.SetActive(false);
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackRange.position, range, playerMask);
        if (hit)
        {
            hit.GetComponent<Player>().TakeDame(15);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            animator.SetTrigger("death");
        }
        else
        {
            Move();
            DectectingGround();
        }
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.DrawSphere(detectPoint.position, distance);
    }
}
