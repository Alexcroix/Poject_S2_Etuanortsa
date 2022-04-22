using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected WeaponType type;
    protected int damage;
    protected int cadence;
    protected int tmpReoald;
    protected int maxMunition;
    protected int munition;
    private float activationtime;

    public WeaponType Type => type;
    public int Damage => damage;
    public int Cadence => cadence;

    //a finir
    public void reload(int munition, int tmpreoald)
    {
        activationtime = 0;
        activationtime += Time.deltaTime;
        if (activationtime >= tmpReoald)
        {
            munition = maxMunition;
        }
    }
}
