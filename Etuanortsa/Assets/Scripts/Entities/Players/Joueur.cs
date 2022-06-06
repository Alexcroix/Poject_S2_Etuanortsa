using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Joueur : MonoBehaviourPunCallbacks
{
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    //UI
    public GameObject shopMenu;
    public GameObject GunShop;
    public GameObject SoundMenu;
    public GameObject LazerGunShop;
    public GameObject MachineGunShop;
    public GameObject Reviveshop;
    public Sprite DefaultShop;
    public GameObject pauseMenu;
    public GameObject playerUI;
    public Image bar;
    public Sprite[] HealthBar;
    public Text MoneyCounterText;
    public Text WaveCounterText;
    //movement
    Vector2 movement;
    Vector2 mousePos;
    Vector3 tempPos;

    public bool Inv = false;
    public float mouse_x;
    public float mouse_y;
    public bool moving;
    public Camera cam;
    public GameObject PlayerCamera;
    PhotonView View;
    Transform pos;

    //Player
    public Rigidbody2D player;
    public Animator animator;

    //Weapon
    public Rigidbody2D weapon;
    public Sprite Left_weapon;
    public Sprite Right_weapon;

    public static float SoundEffect = 0.5f;
    public static float SoundMusic = 0.5f;

    public float MovementSpeed = 10f;

    void Start()
    {
        
        View = GetComponent<PhotonView>();
        pos = GetComponent<Transform>();
        
        MoneyCounterText.text = "" + Game.Money;
        if (View.IsMine)
        {
            PlayerCamera.SetActive(true);

            playerProperties["health"] = 100;
            playerProperties["name"] = PhotonNetwork.LocalPlayer.NickName;
            playerProperties["alive"] = true;
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);

            bar.sprite = HealthBar[(int)playerProperties["health"] / 10];
            pauseMenu.SetActive(false);
            shopMenu.SetActive(false);
            GunShop.SetActive(false);
            LazerGunShop.SetActive(false);
            MachineGunShop.SetActive(false);
            Reviveshop.SetActive(false);
            playerUI.SetActive(true);
        }
    }

    private void Update()
    {
        MoneyCounterText.text = "" + Game.Money;
        if (View.IsMine)
        {
            if (Game.IsWaiting)
            {
                WaveCounterText.text = "Wave : " + Game.WaveCounter;
            }
            else
            {
                WaveCounterText.text = "Time to prepare";
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (shopMenu.activeSelf)
                {
                    shopMenu.SetActive(false);
                }
                else
                {
                    if (pauseMenu.activeSelf)
                    {
                        pauseMenu.SetActive(false);
                    }
                    else
                    {
                        if (SoundMenu.activeSelf)
                        {
                            SoundMenu.SetActive(false);
                        }
                        pauseMenu.SetActive(true);
                    }
                }
            }


            /*
            if(!Game.TimeToWait && shopMenu.activeSelf)
            {
                shopMenu.SetActive(false);
            }
            */

            if (Input.GetKeyDown(KeyCode.E)) //a jouter Game.TimeToWait
            {
                if (!pauseMenu.activeSelf)
                {
                    GunShop.SetActive(true);
                    LazerGunShop.SetActive(false);
                    MachineGunShop.SetActive(false);
                    Reviveshop.SetActive(false);

                    //Item = DefaultShop;
                    ItemCost = 1000;

                    if (shopMenu.activeSelf)
                    {
                        shopMenu.SetActive(false);
                    }
                    else
                    {
                        shopMenu.SetActive(true);
                    }
                }
            }

            //PlayerAnim
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            mousePos = cam.ScreenToViewportPoint(Input.mousePosition);

            mouse_x = (mousePos.x - 0.5f);
            mouse_y = (mousePos.y - 0.5f);
            moving = movement != Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (View.IsMine)
        {
            //VectorMovement
            player.MovePosition(player.position + movement * MovementSpeed * Time.deltaTime);
            weapon.MovePosition(player.position + movement * MovementSpeed * Time.deltaTime);

            //WeaponAim
            Vector2 loorDir = mousePos;
            loorDir.x = loorDir.x - 0.5f;
            loorDir.y = loorDir.y - 0.5f;


            //SetValueAnimator
            animator.SetFloat("mouse_x", mouse_x);
            animator.SetFloat("mouse_y", mouse_y);
            animator.SetBool("moving", moving);

            //SpiteWeapon
            tempPos = weapon.transform.position;

            if (loorDir.y > 0)
            {
                tempPos.z = -1;
                weapon.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
            else
            {
                tempPos.z = -7;
                weapon.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }

            if (loorDir.x <= 0)
            {
                weapon.GetComponent<SpriteRenderer>().sprite = Left_weapon;
            }
            else
            {
                weapon.GetComponent<SpriteRenderer>().sprite = Right_weapon;
            }

            weapon.transform.position = tempPos;
        }

    }

    public void GetDamage(int damage)
    {
        if (!Inv)
        {
            Inv = true;

            StartCoroutine(OnBecameInvinsible());
            playerProperties["health"] = (int)playerProperties["health"] - damage;

            bar.sprite = HealthBar[(int)playerProperties["health"] / 10];




            if ((int)playerProperties["health"] <= 0)
            {
                playerProperties["alive"] = false;
                PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
                playerIsDead();
            }
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
        }
    }


    IEnumerator OnBecameInvinsible()
    {
        yield return new WaitForSeconds(1f);
        Inv = false;
    }

    public void playerIsDead()
    {
        
        PhotonNetwork.Destroy(this.photonView);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");

    }

    public static int ItemCost;
    //public static Sprite Item;

    public void Buy()
    {
        if (Game.Money >= ItemCost)
        {
            this.photonView.RPC("BuyWeapon", RpcTarget.All, ItemCost);
            /*
            Left_Weapon = sprite;
            Right_Weapon = sprite;
            */
        }
    }


    public void OnClickSoundButton()
    {
        pauseMenu.SetActive(false);
        SoundMenu.SetActive(true);
    }


    [PunRPC]
    public void BuyWeapon(int itemCost)
    {
        Game.Money -= itemCost;
    }


    //MUSIC

    public void OnMovingSliderMusic(float Value)
    {
        SoundMusic = Value;
        Game.ChangeVolumeOfMusique(Value);
    }
    public void OnMovingSliderEffect(float Value)
    {
        SoundEffect = Value;
    }
}

