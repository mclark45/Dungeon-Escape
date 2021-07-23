using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public float Health { get; set; }

    //Used for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        Health--;
        enemyAnimator.SetTrigger("Hit");
        enemyAnimator.SetBool("InCombat", true);
        isHit = true;

        if (Health < 1)
            DeathAnimation();
    }
}
