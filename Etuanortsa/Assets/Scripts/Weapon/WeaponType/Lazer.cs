using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Weapon
{
    public Lazer()
    {
        this.type = WeaponType.GUN;
        this.damage = 0;
        this.cadence = 0;
        this.tmpReoald = 0;
        this.munition = 20;
        this.maxMunition = 20;
    }
}
