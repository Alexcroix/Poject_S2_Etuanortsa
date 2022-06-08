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
    private int EnemyDamage = 10;
    private int Gain = 25;

    //public void UpdateHealth(int newHealthValue)
    //{
    //    this.currentHealth = newHealthValue;
    //}

    public void ReceiveDamage(int damage)
    {
        currentHealth = this.currentHealth - damage;
        if (currentHealth <= 0)
        {
            this.photonView.RPC("Gains", RpcTarget.All);
            PhotonNetwork.Destroy(gameObject);
        }
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<Joueur>(out Joueur j))
        {
            j.GetDamage(EnemyDamage);
        }

    }

    [PunRPC]
    public void Gains()
    {
        Game.Money += Gain;
    }
}