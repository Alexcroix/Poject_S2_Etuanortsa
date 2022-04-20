using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class UIPlayer : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject playerUI;
    PhotonView View;
    public Camera cam;
    public GameObject PlayerCamera;
    public Sprite[] HealthBar;

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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.activeSelf == true)
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
