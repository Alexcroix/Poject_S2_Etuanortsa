using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviourPunCallbacks
{
    public static List<EnemyType> AllEnemies = new List<EnemyType>();
    public static List<Transform> PosPlayer = new List<Transform>();
    public static int WaveCounter = 0;
    public static bool IsAWave = false;
    public static bool IsWaiting = false;
    private float timer = 0.0f;
    private float waitingTime = 0.0f;
    public static bool TimeToWait = true;
    public static bool launchWave = false;
    private bool WaveExist = false;//verifier si tous les enemies sont mort a chaque mort d'un enemies,passe a true si ils sont tous mort
    public static int Money;
    public static  AudioSource Music;

    private void Start()
    {
        Money = 0;
        GameObject[] OldMusic = GameObject.FindGameObjectsWithTag("GameMusic");
        foreach (var m in OldMusic)
        {
            Destroy(m);
        }
        Music = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        launchWave = false;
        if (TimeToWait) 
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                timer = 0.0f;
                TimeToWait = false;
                IsAWave = true;
            }
        }
        else
        {
            if (IsAWave && WaveCounter < 31)
            {
                this.photonView.RPC("ChangeToMusicOfPhase", RpcTarget.AllViaServer);
                WaveCounter++;
                launchWave = true;
                IsAWave = false;
                WaveExist = true;
            }
            if (!WaveExist)
            {
                CheckEndGame(WaveCounter);
                TimeToWait = true;
                this.photonView.RPC("ChangeToMusicOfShop", RpcTarget.AllViaServer);
            }
        }
    }

    public static void CheckEndGame(int WaveCounter)
    {
        if (WaveCounter == 31)
        {
            PhotonNetwork.LoadLevel("WinScene");
        }
        bool Dead = true;
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if ((bool)player.CustomProperties["alive"])
            {
                Dead = false;
            }
        }
        if (Dead)
        {
            PhotonNetwork.LoadLevel("LooseScene");
        }
    }

    //Music

   
    [SerializeField] AudioClip[] JukeboxPhase;
    [SerializeField] AudioClip[] JukeboxShop;
    

    [PunRPC]
    public void ChangeToMusicOfPhase()
    {
        Music.Stop();
        Music.clip = JukeboxPhase[0];
        Music.Play();
    }

    [PunRPC]
    public void ChangeToMusicOfShop()
    {
        Music.Stop();
        Music.clip = JukeboxShop[0];
        Music.Play();
    }
}
