using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standard : Enemy
{
    public Standard()
    {
        this.type = EnemyType.STANDARD;
        this.health = 60;
        this.speed = 5;
        this.damage = 15;
        this.gains = 10;
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
