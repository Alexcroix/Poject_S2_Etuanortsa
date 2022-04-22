using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Game
{
    protected EnemyType type;
    protected int health;
    protected int speed;
    protected int damage;
    protected int gains;


    public EnemyType Type => type;
    public int Health => health;
    public int Speed => speed;
    public int Damage => damage;
    public int Gains => gains;

    public abstract void Hit(int x);
    public abstract bool Kill();
}
