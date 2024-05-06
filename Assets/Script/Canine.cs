using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DemonController : Enemy
{
    public Transform detectPoint, player, attackRange;
    public LayerMask layerMask, playerMask;
    public Animator animator;
    public float distance = 0.5f;
    public float range = 1.5f;
    private float delay = 2f;
    private float elapsed = 0;
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
            animator.SetBool("isRange", true);
        }
        else
        {
            animator.SetBool("isRange", false);
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
    }

    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackRange.position, range, playerMask);
        if (hit)
        {
            hit.GetComponent<PlayerMove>().TakeDame(15);
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
        Move();
        DectectingGround();
    }
}
