using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public enum EnemyType
{
    DOG,
    STANDARD,
    BLOB,
    BOSS
}

public class Enemy : Enemies
{
    public EnemyType CurrentEnemyType;
    public GameObject EnemyPrefab;
    public string EnemyTag;
    public int MaxHealth;
    public int EnemyDamage;
    public int EnemySpeed;
    public int EnemyGains;

    private int currentHealth;

    public Enemy(string s)
    {
        switch (s)
        {
            case ("Standard"):
                this.CurrentEnemyType = EnemyType.STANDARD;
                // this.EnemyPrefab = null;
                this.EnemyTag = "Standard";
                this.MaxHealth = 60;
                this.currentHealth = 60;
                this.EnemyDamage = 15;
                this.EnemySpeed = 5;
                this.EnemyGains = 1;
                break;
            case ("Dog"):
                this.CurrentEnemyType = EnemyType.DOG;
                // this.EnemyPrefab = null;
                this.EnemyTag = "Dog";
                this.MaxHealth = 25;
                this.currentHealth = 25;
                this.EnemyDamage = 35;
                this.EnemySpeed = 12;
                this.EnemyGains = 5;
                break;
            case ("Blob"):
                this.CurrentEnemyType = EnemyType.BLOB;
                // this.EnemyPrefab = null;
                this.EnemyTag = "Blob";
                this.MaxHealth = 45;
                this.currentHealth = 45;
                this.EnemyDamage = 25;
                this.EnemySpeed = 7;
                this.EnemyGains = 2;
                break;
            case ("Boss"):
                this.CurrentEnemyType = EnemyType.BOSS;
                // this.EnemyPrefab = null;
                this.EnemyTag = "Boss";
                this.MaxHealth = 1000;
                this.currentHealth = 1000;
                this.EnemyDamage = 50;
                this.EnemySpeed = 5;
                this.EnemyGains = 500;
                break;
            default:
                throw new Exception("Enemy Init: Unkown tag");
        }
    }

    public void UpdateHealth(int newHealthValue)
    {
        currentHealth = newHealthValue;
    }

    public void ReceiveDamage(int damage)
    {
        var updatedHealth = currentHealth - damage;
        UpdateHealth(updatedHealth > 0 ? updatedHealth : 0);
    }
}
