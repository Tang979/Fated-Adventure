using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform detectPoint;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool facingRight;
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;
    [SerializeField] private float idleTimer;
    [SerializeField] private Animator animator;

    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(detectPoint.position, .1f, ground);
        if (!hit)
        {
            if (facingRight)
                Move(1);
            else    
                Move(-1);
        }
        else
        {
            Detetion();
        }
    }
    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }
    void Flip()
    {
        transform.localScale = new Vector3(- transform.localScale.x, 1, 1);
        facingRight = !facingRight;
    }

    void Detetion()
    {
        animator.SetBool("moving", false);
        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
        {
            Flip();
        }
    }

    public void Move(int moveRight)
    {
        idleTimer = 0;
        animator.SetBool("moving", true);
        transform.Translate(Vector2.right * speed * Time.deltaTime * moveRight);
    }
}