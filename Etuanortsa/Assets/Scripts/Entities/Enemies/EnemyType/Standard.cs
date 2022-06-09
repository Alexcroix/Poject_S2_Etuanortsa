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
    [SerializeField]
    public int currentHealth = 20;
    private int EnemyDamage = 15;
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