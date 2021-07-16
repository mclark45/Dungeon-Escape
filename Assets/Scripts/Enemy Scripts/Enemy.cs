using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;

    //waypoint variables
    [SerializeField] protected Transform pointA, pointB;
    protected Transform currentTarget;
    protected bool movingLeft;

    //sprite references
    protected Animator enemyAnimator;
    protected SpriteRenderer enemySpriteRenderer;

    public virtual void Init()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (enemyAnimator == null)
            Debug.LogError(this.name + " animator is Null");

        if (enemySpriteRenderer == null)
            Debug.LogError(this.name + " sprite renderer is Null");
    }

    private void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;

        FlipSprite();
        EnemyMovement();
    }

    protected virtual void EnemyMovement()
    {
        if (Vector2.Distance(transform.position, pointA.position) <= 0.001f)
        {
            currentTarget = pointB;
            movingLeft = false;
            enemyAnimator.SetTrigger("Idle");
        }

        if (Vector2.Distance(transform.position, pointB.position) <= 0.001f)
        {
            currentTarget = pointA;
            movingLeft = true;
            enemyAnimator.SetTrigger("Idle");
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
    }

    protected virtual void FlipSprite()
    {
        if (!movingLeft)
            enemySpriteRenderer.flipX = false;

        if (movingLeft)
            enemySpriteRenderer.flipX = true;
    }
}
