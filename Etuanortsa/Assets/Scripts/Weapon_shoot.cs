using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Weapon_shoot : MonoBehaviourPunCallbacks
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    PhotonView View;
    public float FireRate;
    public static bool CanShoot = true;
    [SerializeField] int SoundOfThisWeapon;


    private void Start()
    {
        CanShoot = true;
    }
    void Update()
    {
        View = GetComponent<PhotonView>();
        if (View.IsMine)
        {
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f);
            if (Input.GetKey(KeyCode.Mouse0) && CanShoot)
            {
                CanShoot = false;
                Shoot();
                StartCoroutine(WeaponShotTimer(FireRate));
            }
        }
    }

    IEnumerator WeaponShotTimer(float f) 
    {
        yield return new WaitForSeconds(f);
        CanShoot = true;
    }
    void Shoot()
    {
        this.photonView.RPC("WeaponShot", RpcTarget.AllViaServer, SoundOfThisWeapon, new Vector3(firePoint.position.x,firePoint.position.y,-7));
        PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);      
    }

    [SerializeField] AudioClip[] Jukebox;

    [PunRPC]
    public void WeaponShot(int music, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(Jukebox[music], pos,Joueur.SoundEffect);
    }
}
