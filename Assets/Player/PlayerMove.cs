using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private float speed = 4;
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
    private bool checkJump;
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
        if (Input.GetKey("j"))
        {
            animator.SetTrigger("attack");
        }
        jump();
        healthBar.SetHealth(currentHealth);
        animator.SetFloat("xVelocity", Math.Abs(leftRight));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    void jump()
    {
        checkJump = Physics2D.OverlapCircle(grcheck.position, 0.2f, grLayer);
        if (Input.GetKey(KeyCode.W) && checkJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            animator.SetFloat("yVelocity", 0);
            animator.SetTrigger("jump");
            checkJump = false;
        }
    }
    // private void OnTriggerStay2D(Collider2D collider2D)
    // {
    //     if (collider2D.CompareTag("Trap"))
    //     {
    //         if (elapsed == 0)
    //         {
    //             currentHealth -= 2;
    //             healthBar.SetHealth(currentHealth);
    //         }
    //         elapsed += Time.deltaTime;
    //         if (elapsed >= delay)
    //             elapsed = 0;
    //     }
    // }
}
