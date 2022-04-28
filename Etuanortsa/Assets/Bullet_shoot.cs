using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_shoot : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 10;
    // Start is called before the first frame update

    //Vitesse des balles
    void Start()
    {
        rb.velocity = -transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Destroy(gameObject);
        //ajouter chaque ennemie
        Dog dog_ennemi = hitInfo.GetComponent<Dog>();
        Standard standard_ennemi = hitInfo.GetComponent<Standard>();
        Boss boss_ennemi = hitInfo.GetComponent<Boss>();
        Blob blob_ennemi = hitInfo.GetComponent<Blob>();
        if (dog_ennemi != null)
        {
            //changer la variable damage avec les degats de l'arme que le joueur a dans la main
            dog_ennemi.ReceiveDamage(damage);
        }
        else if (standard_ennemi != null)
        {
            standard_ennemi.ReceiveDamage(damage);
        }
        else if (boss_ennemi != null)
        {
            boss_ennemi.ReceiveDamage(damage);
        }
        else if (blob_ennemi != null)
        {
            blob_ennemi.ReceiveDamage(damage);
        }
    }
}
