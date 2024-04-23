using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float sizePlayer = 2.5f;
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 4;
    public float leftRight;
    public bool facingRight = true;
    [SerializeField] int jumpPower;
    bool isJumping = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        leftRight = Input.GetAxis("Horizontal");
        if(facingRight && leftRight<0)
        {
            transform.localScale = new Vector3(-sizePlayer,sizePlayer,sizePlayer);
            facingRight=false;
        }
        if(!facingRight && leftRight > 0)
        {
            transform.localScale=new Vector3(sizePlayer,sizePlayer,sizePlayer);
            facingRight = true;
        }
        rb.velocity = new Vector2(speed*leftRight, rb.velocity.y);
        animator.SetFloat("run", Math.Abs(leftRight));
        if(Input.GetKey("j"))
        {
            animator.SetTrigger("attack");
        }
        if(Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }
}
