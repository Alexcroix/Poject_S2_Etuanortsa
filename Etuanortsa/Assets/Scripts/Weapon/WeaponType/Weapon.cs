using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected WeaponType type;
    protected int damage;
    protected int cadence;

    public WeaponType Type => type;
    public int Damage => damage;
    public int Cadence => cadence;
}
