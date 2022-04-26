using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob
{
    public EnemyType CurrentEnemyType;
    public GameObject gameObject;
    public string EnemyTag;
    public int MaxHealth;
    public int currentHealth;
    public int EnemyDamage;
    public int EnemySpeed;
    public int EnemyGains;

    public void UpdateHealth(int newHealthValue)
    {
        this.currentHealth = newHealthValue;
    }

    public void ReceiveDamage(int damage)
    {
        int updatedHealth = this.currentHealth - damage;
        UpdateHealth(updatedHealth > 0 ? updatedHealth : 0);
    }
}
