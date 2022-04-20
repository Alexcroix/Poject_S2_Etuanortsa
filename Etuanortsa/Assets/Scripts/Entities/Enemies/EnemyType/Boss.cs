using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public Boss()
    {
        this.type = EnemyType.BOSS;
        this.health = 1000;
        this.speed = 5;
        this.damage = 50;
        this.gains = 500;
    }

    public override void Hit(int x)
    {
        this.health -= x;
    }

    public override bool Kill()
    {
        if (this.health == 0)
        {
            return true;
        }
        return false;
    }
}
