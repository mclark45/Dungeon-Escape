using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;
    protected Transform currentTarget;
    protected bool movingLeft;

    protected abstract void Update();

    protected abstract void EnemyMovement();

    protected virtual void Attack()
    {
        Debug.Log("My name is " + this.gameObject.name);
    }
}
