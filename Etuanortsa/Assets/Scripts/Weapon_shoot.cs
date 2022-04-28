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
