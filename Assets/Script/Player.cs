using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private float speed = 4;

    private bool canSlide = true, canAttack = true;
    private bool isSlide;
    private float slidePower = 10f;
    private float slideTime = 0.2f, attackTime;
    private float cdSlide = 1f, cdattack = 1f;

    private float maxHealth = 100;
    private float maxStamina = 100;
    private float dame = 5;
    public float currentStamina;
    public float currentHealth;
    [SerializeField] private statusBar statusBar;
    private float leftRight;
    private bool facingRight = true;
    private float jumpPower = 7;
    public Transform grcheck, attackPoint;
    public LayerMask grLayer, enemyLayer;
    private bool doubleJump = false;
    private float delay = 1f;
    private float elapsed = 0;
    private float attackRange = 2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        statusBar.SetMaxHealt(maxHealth);
        statusBar.SetMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        HealhStamina();
        if (isSlide)
            return;
        leftRight = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * leftRight, rb.velocity.y);
        flip();
        Attack();
        if (Input.GetKey(KeyCode.S) && canSlide && currentStamina>20)
        {
            if (!isGrounded())
                return;
            StartCoroutine(Slide());
        }
        Jump();
        animator.SetFloat("xVelocity", Math.Abs(leftRight));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    public void Attack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);
        if (Input.GetKeyDown(KeyCode.J) && canAttack == true)
        {
            attackTime = cdattack;
            animator.SetTrigger("attack");
            if (hit)
            {
                hit.GetComponent<CanineController>().TakeDame(dame);
            }
            canAttack = false;
        }
        else
        {
            if(attackTime<=0)
                canAttack = true;
            else
                attackTime-=Time.deltaTime;
        }
    }
    void flip()
    {
        if (facingRight && leftRight < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }
        if (!facingRight && leftRight > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(grcheck.position, 0.1f, grLayer);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                animator.SetTrigger("jump");
                doubleJump = true;
            }
            else if (doubleJump)
            {
                animator.SetTrigger("doubleJump");
                rb.velocity = new Vector2(rb.velocity.x, jumpPower * 0.8f);
                doubleJump = false;
            }
        }
    }
    public IEnumerator Slide()
    {
        canSlide = false;
        isSlide = true;
        animator.SetBool("isSlide", true);
        Stamina(20);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * slidePower, 0f);
        yield return new WaitForSeconds(slideTime);
        rb.gravityScale = originalGravity;
        isSlide = false;
        animator.SetBool("isSlide", false);
        yield return new WaitForSeconds(cdSlide);
        canSlide = true;
    }
    private void Stamina(float stamina)
    {
        currentStamina -= stamina;
        statusBar.SetStamina(currentStamina);
    }
    private void HealhStamina()
    {
        if (currentStamina >= maxStamina)
        {
            currentStamina = 100;
            return;
        }
        else
        {
            currentStamina += Time.deltaTime;
            statusBar.SetStamina(currentStamina);
        }
    }
    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Trap"))
        {
            if (elapsed == 0)
            {
                TakeDame(5);
            }
            elapsed += Time.deltaTime;
            if (elapsed >= delay)
                elapsed = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Trap"))
            elapsed = 0;
    }
    public void TakeDame(float dame)
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("Death", true);
            return;
        }
        animator.SetTrigger("hurt");
        currentHealth -= dame;
        statusBar.SetHealth(currentHealth);
    }
}
