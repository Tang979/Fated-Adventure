using System.Diagnostics;
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
    private float slideTime = .2f, attackTime;
    private float cdSlide = 1f, cdattack = 1f;

    private Health health;
    private float maxStamina = 100;
    [SerializeField] private float dame = 5;
    public float currentStamina;

    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Stamina staminaBar;
    private float leftRight;
    private bool facingRight = true;
    private float jumpPower = 7;
    public Transform grcheck, attackPoint;
    public LayerMask grLayer, enemyLayer;
    private bool doubleJump = false;
    private float delay = 1f;
    private float elapsed = 0;
    [SerializeField] private BoxCollider2D boxCollider;
    private Health enemyHealth;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        currentStamina = maxStamina;
        healthBar.SetMaxHealt(health.MaxHealth);
        staminaBar.SetMaxStamina(maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        if (health.Die)
            return;
        HealhStamina();
        if (isSlide)
            return;
        leftRight = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * leftRight, rb.velocity.y);
        flip();
        if (Input.GetKey(KeyCode.S) && canSlide && currentStamina > 20)
        {
            if (!isGrounded())
                return;
            StartCoroutine(Slide());
        }
        Attack();
        Jump();
        animator.SetFloat("xVelocity", Math.Abs(leftRight));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && canAttack == true)
        {
            attackTime = cdattack;
            animator.SetTrigger("attack");
            audioManager.PlaySFX(audioManager.attack);
            canAttack = false;
        }
        else
        {
            if (attackTime <= 0)
                canAttack = true;
            else
                attackTime -= Time.deltaTime;
        }
    }
    public void DameEnemy()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0, enemyLayer);
        if (hit != null)
        {
            enemyHealth = hit.transform.GetComponent<Health>();
            enemyHealth.TakeDame(dame);
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
                audioManager.PlaySFX(audioManager.jump);
                doubleJump = true;
            }
            else if (doubleJump)
            {
                animator.SetTrigger("doubleJump");
                audioManager.PlaySFX(audioManager.jump);
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
        audioManager.PlaySFX(audioManager.slide);
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
        staminaBar.SetStamina(currentStamina);
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
            staminaBar.SetStamina(currentStamina);
        }
    }
    private void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Trap"))
        {
            if (elapsed == 0)
            {
                health.TakeDame(5);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("At"))
        {
            dame = dame + 5;
            Destroy(collision.gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }
}
