using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private float speed = 4;
    private int slide = 10;
    public float cdSlide = 3;
    private int maxHealth = 100;
    private int maxStamina = 100;
    public int currentStamina;
    public int currentHealth;
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

        leftRight = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * leftRight, rb.velocity.y);
        flip();
        if (Input.GetKey("j"))
        {
            animator.SetTrigger("attack");
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
        return Physics2D.OverlapCircle(grcheck.position, 0.2f, grLayer);
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
    void Slide()
    {
        if (Input.GetKey(KeyCode.K))
        {
            rb.velocity = new Vector2(rb.velocity.x + slide * transform.localScale.x, rb.velocity.y);
        }
        
    }
    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Trap"))
        {
            if (elapsed == 0)
            {
                currentHealth -= 2;
                healthBar.SetHealth(currentHealth);
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
}
