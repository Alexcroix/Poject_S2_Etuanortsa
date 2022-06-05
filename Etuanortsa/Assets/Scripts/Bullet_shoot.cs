using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet_shoot : MonoBehaviourPunCallbacks
{
    public float speed = 50f;
    public Rigidbody2D rb;
    public int damage = 10;
    // Start is called before the first frame update
    [SerializeField] AudioClip[] Jukebox;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        if (hitInfo.gameObject.TryGetComponent<Dog>(out Dog d))
        {
            this.photonView.RPC("WeaponCollideAudio", RpcTarget.All, 1, this.transform.position);
            d.ReceiveDamage(damage);
        }
        else if (hitInfo.gameObject.TryGetComponent<Standard>(out Standard s))
        {
            this.photonView.RPC("WeaponCollideAudio", RpcTarget.All, 1, this.transform.position);
            s.ReceiveDamage(damage);
        }
        else if (hitInfo.gameObject.TryGetComponent<Boss>(out Boss b))
        {
            b.ReceiveDamage(damage);
        }
        else if (hitInfo.gameObject.TryGetComponent<Blob>(out Blob bl))
        {
            bl.ReceiveDamage(damage);
        }
        else
        {
            this.photonView.RPC("WeaponCollideAudio", RpcTarget.All, 0,this.transform.position);
        }
        if (hitInfo.tag != "spawn_enemy")
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        
    }

    [PunRPC]
    public void WeaponCollideAudio(int Sound,Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(Jukebox[Sound],pos,Joueur.SoundEffect);
    }
}