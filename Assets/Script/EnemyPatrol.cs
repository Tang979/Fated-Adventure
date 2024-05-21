using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform detectPoint, enemy;
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool facingRight;
    [SerializeField] private float raycastDistance;
    [SerializeField] private float speed;
    private float idleDuration = .5f;
    private float idleTimer;
    [SerializeField] private Animator animator;

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(detectPoint.position, Vector2.down, raycastDistance, ground);
        if(hit.collider == null)
        {
            Detetion();
        }
        else
        {
            if (facingRight)
                Move(1);
            else
                Move(-1);
        }
        
    }
    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }
    void Flip()
    {
        enemy.localScale = new Vector3(-enemy.localScale.x, 1, 1);
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
        enemy.Translate(Vector2.right * speed * Time.deltaTime * moveRight);
    }
}