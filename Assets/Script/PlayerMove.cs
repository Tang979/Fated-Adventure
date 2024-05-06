using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private float speed = 4;

    private bool canSlide = true;
    private bool isSlide;
    private float slidePower = 10f;
    private float slideTime = 0.2f;
    private float cdSlide = 1f;

    private float maxHealth = 100;
    private float maxStamina = 100;
    public float currentStamina;
    public float currentHealth;
    public HealthBar healthBar;
    private float leftRight;
    private bool facingRight = true;
    private float jumpPower = 7;
    public Transform grcheck;
    public LayerMask grLayer;
    private bool doubleJump = false;
    private float delay = 1f;
    private float elapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        healthBar.SetMaxHealt(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlide)
            return;
        leftRight = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * leftRight, rb.velocity.y);
        flip();
        if (Input.GetKey("j"))
        {
            animator.SetTrigger("attack");
        }
        if (Input.GetKey(KeyCode.K) && canSlide)
        {
            if (!isGrounded())
                return;
            StartCoroutine(Slide());
        }
        Jump();
        Slide();
        animator.SetFloat("xVelocity", Math.Abs(leftRight));
        animator.SetFloat("yVelocity", rb.velocity.y);
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
    private IEnumerator Slide()
    {
        canSlide = false;
        isSlide = true;
        animator.SetBool("isSlide", true);
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
        animator.SetTrigger("hurt");
        currentHealth -= dame;
        healthBar.SetHealth(currentHealth);
    }
}
