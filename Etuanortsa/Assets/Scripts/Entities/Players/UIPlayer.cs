using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPlayer : Joueur
{
    public GameObject pauseMenu;
    public GameObject playerUI;
    PhotonView View;
    public Image bar;
    public Camera cam;
    public GameObject PlayerCamera;
    public Sprite[] HealthBar;
    protected int Current_hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        
        View = GetComponent<PhotonView>();
        if (View.IsMine)
        {
            pauseMenu.SetActive(false);
            playerUI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (View.IsMine)
        {
            bar.sprite = HealthBar[(Current_hp / 10)];
            Current_hp -= 1;
            
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
        }
    }
}
