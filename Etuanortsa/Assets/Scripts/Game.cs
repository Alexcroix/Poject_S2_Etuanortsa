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
    
    public static int WaveCounter = 0;
    public static bool IsAWave = false;
    public static bool IsWaiting = false;
    private float timer = 0.0f;
    private float waitingTime = 15.0f;
    public static bool TimeToWait = true;
    public static bool launchWave = false;
    public static bool WaveExist = false;//verifier si tous les enemies sont mort a chaque mort d'un enemies,passe a true si ils sont tous mort
    public static int Money;
    public static AudioSource Music;
    public static int WaveSize;

    private void Start()
    {
        Music = GetComponent<AudioSource>();
        Money = 0;
        GameObject[] OldMusic = GameObject.FindGameObjectsWithTag("GameMusic");
        foreach (var m in OldMusic)
        {
            Destroy(m);
        }
    }

    public static void MobDrop(int i)
    {
        Money += i;
    }

    private void FixedUpdate()
    {
        
        WaveExist = !(GameObject.FindGameObjectsWithTag("Enemy").Length == 0);


        this.photonView.RPC("ChangeSeeker", RpcTarget.All);
        
        
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

    [PunRPC]
    public void GiveMoney(int itemCost)
    {
        Game.Money -= itemCost;
    }

    public static void CheckEndGame(int WaveCounter)
    {
        if (WaveCounter == 31)
        {
            PhotonNetwork.LoadLevel("WinScene");
        }
    }

    public static void RemovePlayer(GameObject p)
    {
        Destroy(p);
        PhotonNetwork.Destroy(p);   
    }

    //Music


    [SerializeField] AudioClip[] JukeboxPhase;
    [SerializeField] AudioClip[] JukeboxShop;

    public static void ChangeVolumeOfMusique(float value)
    {
        Music.volume = value;
    }

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

    [PunRPC]
    void ChangeSeeker()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        List<Transform> trans = new List<Transform>();
        foreach (var t in g)
        {     
            trans.Add(t.transform);
        }
        Pathfinding.Seeker.listPlayer = trans;
    }

}
