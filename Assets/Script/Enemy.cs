using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float dame;

    public float Health { get => health; set => health = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Dame { get => dame; set => dame = value; }

    public abstract void Move();
    public abstract void DectectingGround();
}
