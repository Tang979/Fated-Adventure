using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 4;
    public float leftRight;
    public bool facingRight = true;
    [SerializeField] float jumpPower;
    public Transform grcheck;
    public LayerMask grLayer;
    bool checkJump;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        leftRight = Input.GetAxis("Horizontal");
        if(facingRight && leftRight<0)
        {
            transform.localScale = new Vector3(-1,1,1);
            facingRight=false;
        }
        if(!facingRight && leftRight > 0)
        {
            transform.localScale=new Vector3(1,1,1);
            facingRight = true;
        }
        rb.velocity = new Vector2(speed*leftRight, rb.velocity.y);
        if(Input.GetKey("j"))
        {
            animator.SetTrigger("attack");
        }
        jump();
        animator.SetFloat("xVelocity", Math.Abs(leftRight));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    void jump()
    {
        checkJump = Physics2D.OverlapCircle(grcheck.position,0.2f,grLayer);
        if(Input.GetKey(KeyCode.W) && checkJump)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpPower);
            animator.SetFloat("yVelocity", 0);
            animator.SetTrigger("jump");
            checkJump=false;
        }
    }
}
