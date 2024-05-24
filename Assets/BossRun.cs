using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float attackCooldown;
    [SerializeField] private float cooldownTimer = 0;
    [SerializeField] float attackRange = 3f;
    [SerializeField] Transform player;
    [SerializeField] Health health;
    [SerializeField] Rigidbody2D rb;
    CuluthuBoss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<CuluthuBoss>();
        health = animator.GetComponent<Health>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(health.Die)
        {
            animator.SetTrigger("death");
            return;
        }
        cooldownTimer += Time.deltaTime;
        boss.LookAtPlayer();
        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("attack1");
            }
        }
            Vector2 target = new Vector2(player.position.x, animator.transform.position.y);
            Vector2 newPos = Vector2.MoveTowards(animator.transform.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
