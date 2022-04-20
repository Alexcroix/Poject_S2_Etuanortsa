using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public Gun()
    {
        this.type = WeaponType.GUN;
        this.damage = 0;
        this.cadence = 0;
    }
}
