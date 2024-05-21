using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuluthuBoss : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform player;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        Vector2 target = new Vector2(player.position.x, this.transform.position.y);
        transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.fixedDeltaTime);
    }
}
