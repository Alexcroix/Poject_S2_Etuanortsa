using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Standard : MonoBehaviourPunCallbacks
{
    private EnemyType CurrentEnemyType = EnemyType.STANDARD;
    private string EnemyTag = "Standard";
    private int MaxHealth = 45;
    public int currentHealth = 45;
    private int EnemyDamage = 15;
    private int EnemySpeed = 5;
    private int EnemyGains = 1;


    public void UpdateHealth(int newHealthValue)
    {
        this.currentHealth = newHealthValue;
    }

    public void ReceiveDamage(int damage)
    {
        int updatedHealth = this.currentHealth - damage;
        UpdateHealth(updatedHealth > 0 ? updatedHealth : 0);
    }
    void Update()
    {
        Seeker.listPlayer = Game.PosPlayer;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<Joueur>(out Joueur j))
        {
            j.GetDamage(EnemyDamage);
        }

    }
}