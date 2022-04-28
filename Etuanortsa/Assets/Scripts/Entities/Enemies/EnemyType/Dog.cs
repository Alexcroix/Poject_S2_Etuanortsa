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
    private EnemyType CurrentEnemyType = EnemyType.DOG;
    private string EnemyTag = "Dog";
    private int MaxHealth = 25;
    public int currentHealth = 25;
    private int EnemyDamage = 35;
    private int EnemySpeed = 12;
    private int EnemyGains = 5;


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
