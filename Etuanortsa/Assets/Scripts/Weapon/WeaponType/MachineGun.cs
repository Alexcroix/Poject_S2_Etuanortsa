using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public MachineGun()
    {
        this.type = WeaponType.MACHINE_GUN;
        this.damage = 0;
        this.cadence = 0;
    }
}
