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
    private static int MaxHealth = 25;
    private int currentHealth = MaxHealth;
    private int EnemyDamage = 35;
    private int EnemySpeed = 12;
    private int EnemyGains = 5;


    public void ReceiveDamage(int damage)
    {
        currentHealth = this.currentHealth - damage;
        if(currentHealth <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Joueur>(out Joueur j) )
        {
            j.GetDamage(EnemyDamage);
        }
        
    }
}
