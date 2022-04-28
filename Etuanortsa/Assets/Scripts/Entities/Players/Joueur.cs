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
    public GameObject pauseMenu;
    public GameObject playerUI;
    public Image bar;
    public Sprite[] HealthBar;
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
    public List<Weapon> weapons = new List<Weapon>();

    public float MovementSpeed = 10f;

    void Start()
    {
        View = GetComponent<PhotonView>();
        pos = GetComponent<Transform>();
        Game.PosPlayer.Add(pos);
        if (View.IsMine)
        {
            PlayerCamera.SetActive(true);

            playerProperties["health"] = 100;
            playerProperties["name"] = PhotonNetwork.LocalPlayer.NickName;
            playerProperties["alive"] = true;
            PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);

            bar.sprite = HealthBar[(int)playerProperties["health"] / 10];
            pauseMenu.SetActive(false);
            playerUI.SetActive(true);

        }
    }

    private void Update()
    {
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
                if (pauseMenu.activeSelf)
                {
                    pauseMenu.SetActive(false);
                }
                else
                {
                    pauseMenu.SetActive(true);
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
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            PhotonNetwork.LoadLevel("LooseScene");
        }
        else
        {
            Game.CheckEndGame(Game.WaveCounter);
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            playerUI.SetActive(false);
        }   
    }
}

