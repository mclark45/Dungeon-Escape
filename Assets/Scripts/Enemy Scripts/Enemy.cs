using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected GameObject diamond;
    protected GameObject[] diamonds;

    //waypoint variables
    [SerializeField] protected Transform pointA, pointB;
    protected Transform currentTarget;
    protected bool movingLeft;

    //sprite references
    protected Animator enemyAnimator;
    protected SpriteRenderer enemySpriteRenderer;

    protected Player player;
    protected Diamond diamondScript;

    protected bool isHit = false;
    protected bool isDead = false;

    public virtual void Init()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (enemyAnimator == null)
            Debug.LogError(this.name + " animator is Null");

        if (enemySpriteRenderer == null)
            Debug.LogError(this.name + " sprite renderer is Null");

        if (player == null)
            Debug.LogError("Player script is Null");
    }

    private void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        if (enemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !enemyAnimator.GetBool("InCombat"))
            return;

        if (isDead)
            return;

        FlipSprite();
        EnemyMovement();
        StopCombatMode();
    }

    protected virtual void EnemyMovement()
    {
        Vector2 direction = player.transform.position - transform.position;

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

        if (!isHit)
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);

        if (isHit)
        {
            //gets the skeleton facing the player correctly

            if (direction.x < 0 && isHit)
                transform.localScale = new Vector2(-1f, transform.localScale.y);

            if (direction.x > 0 && isHit)
                transform.localScale = new Vector2(1f, transform.localScale.y);
        }
    }

    protected virtual void FlipSprite()
    {
        if (!movingLeft)
            transform.localScale = new Vector2(1f, transform.localScale.y);

        if (movingLeft)
            transform.localScale = new Vector2(-1f, transform.localScale.y);
    }

    protected void StopCombatMode()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance < 3.5f)
            return;

        if (distance > 3.5f)
        {
            enemyAnimator.SetBool("InCombat", false);
            isHit = false;
        }
    }

    protected void DeathAnimation()
    {
        isDead = true;
        StartCoroutine(Die());
    }

    protected void SpawnDiamonds()
    {
        Instantiate(diamond, transform.position, Quaternion.identity);
        diamonds = GameObject.FindGameObjectsWithTag("Diamond");
        diamondScript = diamonds[diamonds.Length - 1].GetComponent<Diamond>();

        if (diamondScript != null)
        {
            diamondScript.Gems(gems);
        }
    }

    IEnumerator Die()
    {
        Physics2D.IgnoreLayerCollision(6, 8, true);
        enemyAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(2.0f);
        Physics2D.IgnoreLayerCollision(6, 8, false);
        Destroy(this.gameObject);
    }
}
