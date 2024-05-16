using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    float cdhit = .2f;
    float hitTimer = 0;
    bool hiting = false;
    private Animator animator;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }
    private void Update()
    {
        if (health <= 0)
        {
            enemyPatrol.enabled = false;
            animator.SetBool("death", true);
        }
        if(hiting)
        {
            hitTimer+=Time.deltaTime;
            enemyPatrol.enabled = false;
            if(hitTimer > cdhit)
            {
                hiting = false;
                enemyPatrol.enabled = true;
                hitTimer=0;
            }
        }
    }
    
}
