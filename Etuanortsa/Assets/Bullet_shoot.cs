using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_shoot : MonoBehaviour
{
    public float speed = 50f;
    public Rigidbody2D rb;
    public int damage = 10;
    // Start is called before the first frame update

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Destroy(gameObject);
        if (hitInfo.gameObject.TryGetComponent<Dog>(out Dog d))
        {
            d.ReceiveDamage(damage);
        }
        if (hitInfo.gameObject.TryGetComponent<Standard>(out Standard s))
        {
            s.ReceiveDamage(damage);
        }
        if (hitInfo.gameObject.TryGetComponent<Boss>(out Boss b))
        {
            b.ReceiveDamage(damage);
        }
        if (hitInfo.gameObject.TryGetComponent<Blob>(out Blob bl))
        {
            bl.ReceiveDamage(damage);
        }
    }
}