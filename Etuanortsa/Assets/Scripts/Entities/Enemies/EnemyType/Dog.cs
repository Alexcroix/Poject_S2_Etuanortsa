using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dog : MonoBehaviourPunCallbacks
{
    public EnemyType CurrentEnemyType;

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
    void Update()
    {   
        Seeker.listPlayer = Game.PosPlayer;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<Joueur>(out Joueur j) )
        {
            j.GetDamage(EnemyDamage);
        }
        
    }
}
