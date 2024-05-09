using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CanineController : Enemy
{
    public Transform detectPoint, player, attackRange;
    public LayerMask layerMask, playerMask;
    public Animator animator;
    public float distance = 1.5f;
    public float range = 1.5f;
    private bool canattack = true;
    private bool isattack;
    private float attackTime = 0.2f;
    private float cdattack = 1f;
    public bool facingRight = true;
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
            isRange();
        }
        else
        {
            animator.SetBool("isRange", false);
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
    }

    public IEnumerator isRange()
    {
        canattack = false;
        isattack = true;
        animator.SetBool("isRange", true);
        yield return new WaitForSeconds(attackTime);
        isattack = false;
        animator.SetBool("isRange", false);
        yield return new WaitForSeconds(cdattack);
        canattack = true;
    }

    public void TakeDame(float dame)
    {
        Health -= dame;
        animator.SetTrigger("hit");
    }

    public void Death()
    {
        Destroy(gameObject);
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
            return;
        }
        Move();
        DectectingGround();
    }
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.DrawSphere(detectPoint.position, distance);
    }
}
