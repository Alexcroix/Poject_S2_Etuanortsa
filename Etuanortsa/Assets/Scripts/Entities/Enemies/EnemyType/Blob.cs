using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Blob : MonoBehaviourPunCallbacks
{
    private EnemyType CurrentEnemyType = EnemyType.BLOB;
    private string EnemyTag = "Blob";
    private int MaxHealth = 70;
    public int currentHealth = 70;
    private int EnemyDamage = 25;
    private int EnemySpeed = 7;
    private int EnemyGains = 2;


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