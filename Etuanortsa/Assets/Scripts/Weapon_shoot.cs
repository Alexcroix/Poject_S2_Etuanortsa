using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Weapon_shoot : MonoBehaviourPunCallbacks
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    PhotonView View;

    void Update()
    {
        View = GetComponent<PhotonView>();
        if (View.IsMine)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f);
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
