using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Joueur : Game
{
    //Stats
    public static int hp = 100;
    public bool alive = false;
    protected int Current_hp = 100;

    //UI
    public GameObject pauseMenu;
    public GameObject playerUI;
    public Image bar;
    public Sprite[] HealthBar;

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

    //Player
    public Rigidbody2D player;
    public Animator animator;

    //Weapon
    public Rigidbody2D weapon;
    public Sprite Left_weapon;
    public Sprite Right_weapon;

    public float MovementSpeed = 10f;

    void Start()
    {
        View = GetComponent<PhotonView>();
        if (View.IsMine)
        {
            Game.joueurs.Add(this);
            pauseMenu.SetActive(false);
            playerUI.SetActive(true);
            PlayerCamera.SetActive(true);
        }
    }

    void Update()
    {
        if (View.IsMine)
        {
            //UI
            bar.sprite = HealthBar[(Current_hp / 10)];

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
                tempPos.z = -4;
            }
            else
            {
                tempPos.z = -6;
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
}

