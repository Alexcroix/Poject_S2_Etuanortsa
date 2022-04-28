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

    //Sprite
    private SpriteRenderer character;
    private Color col;
    private bool invisible;

    //movement
    Vector2 movement;
    Vector2 mousePos;
    Vector3 tempPos;

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
            playerProperties["alive"] = true;
            playerProperties["name"] = PhotonNetwork.LocalPlayer.NickName;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);

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
            float angle = Mathf.Atan2(loorDir.y, loorDir.x) * Mathf.Rad2Deg - 180f;
            weapon.rotation = angle;

            //SetValueAnimator
            animator.SetFloat("mouse_x", mouse_x);
            animator.SetFloat("mouse_y", mouse_y);
            animator.SetBool("moving", moving);

            //SpiteWeapon
            tempPos = weapon.transform.position;

            if (loorDir.y > 0)
            {
                tempPos.z = -1;
            }
            else
            {
                tempPos.z = -8;
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
        playerProperties["health"] = (int)playerProperties["health"] - damage;
        bar.sprite = HealthBar[(int)playerProperties["health"] / 10];
        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["health"] <= 0)
        {
            PhotonNetwork.LocalPlayer.CustomProperties["alive"] = false;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    // le met en invisible et enleve tout les armes
    public void playerIsDead()
    {
        invisible = true;
        col.a = .2f;
        character.color = col;
        weapons.Clear();
    }

    // enleve l'invisibilité et ajouter l'arme de base
    public void playerIsRevive()
    {
        invisible = false;
        col.a = 1;
        character.color = col;
        //ajouter a la liste du joueur l'arme basique
    }

}

