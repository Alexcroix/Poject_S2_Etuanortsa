using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Weapon
{
    public Lazer()
    {
        this.type = WeaponType.LAZER;
        this.damage = 0;
        this.cadence = 0;
    }
}
