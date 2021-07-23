using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public float Health { get; set; }

    //Used for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    protected override void Update()
    {
        EnemyMovement();
    }

    public void Damage()
    {
        Health--;

        if (Health < 1f)
            DeathAnimation();
    }

    protected override void EnemyMovement()
    {
        Vector2 direction = player.transform.position - transform.position;

        if (direction.x < 5.5f)
            enemyAnimator.SetBool("InCombat", true);

        if (direction.x > 5.5f)
            enemyAnimator.SetBool("InCombat", false);
    }
}
