using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy
{
    private Animator _mossGiantAnimator;
    private SpriteRenderer _mossGiantSpriteRenderer;

    void Start()
    {
        _mossGiantAnimator = GetComponentInChildren<Animator>();
        _mossGiantSpriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (_mossGiantAnimator == null)
            Debug.LogError("Moss Giant Animator is Null");

        if (_mossGiantSpriteRenderer == null)
            Debug.LogError("Moss Giant Sprite Renderer is Null");
    }

    protected override void Update()
    {
        if (_mossGiantAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;

        FlipSprite();
        EnemyMovement();
    }

    protected override void EnemyMovement()
    {
        if (Vector2.Distance(transform.position, pointA.position) <= 0.001f)
        {
            currentTarget = pointB;
            movingLeft = false;
            _mossGiantAnimator.SetTrigger("Idle");
        }

        if (Vector2.Distance(transform.position, pointB.position) <= 0.001f)
        {
            currentTarget = pointA;
            movingLeft = true;
            _mossGiantAnimator.SetTrigger("Idle");
        }

        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
    }

    private void FlipSprite()
    {
        if (!movingLeft)
            _mossGiantSpriteRenderer.flipX = false;

        if (movingLeft)
            _mossGiantSpriteRenderer.flipX = true;
    }
}
