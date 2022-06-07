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
    [SerializeField]
    private int currentHealth = 15;
    private int EnemyDamage = 25;
    


    public void ReceiveDamage(int damage)
    {
        currentHealth = this.currentHealth - damage;
        if(currentHealth <= 0)
        {
            this.photonView.RPC("BuyWeapon", RpcTarget.All, -25);
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

    [PunRPC]
    public void BuyWeapon(int itemCost)
    {
        Game.Money -= itemCost;
    }


}
