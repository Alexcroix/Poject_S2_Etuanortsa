using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Enemy
{
    public Dog()
    {
        this.type = EnemyType.DOG;
        this.health = 25;
        this.speed = 10;
        this.damage = 10;
        this.gains = 5;
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
