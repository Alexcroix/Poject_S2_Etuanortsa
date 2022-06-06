using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviourPunCallbacks
{
    private EnemyType CurrentEnemyType = EnemyType.BOSS;
    private string EnemyTag = "Boss";
    private int MaxHealth = 10000;
    public int currentHealth = 10000;
    private int EnemyDamage = 50;
    private int EnemySpeed = 5;
    private int EnemyGains = 500;


    public void UpdateHealth(int newHealthValue)
    {
        this.currentHealth = newHealthValue;
    }

    public void ReceiveDamage(int damage)
    {
        int updatedHealth = this.currentHealth - damage;
        UpdateHealth(updatedHealth > 0 ? updatedHealth : 0);
    }
   
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<Joueur>(out Joueur j))
        {
            j.GetDamage(EnemyDamage);
        }

    }
}
