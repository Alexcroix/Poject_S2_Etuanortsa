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
    public static bool IsAWave = false;//verifier si tous les enemies sont mort a chaque mort d'un enemies,passe a true si ils sont tous mort
    public static bool IsWaiting = false;
    private float timer = 0.0f;
    public float waitingTime = 60.0f;
    public bool TimeToWait = true;
    public bool GameIsFinish = false;
   
    private void FixedUpdate()
    {
        if (TimeToWait) 
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                timer = 0f;
                TimeToWait = false;
                IsAWave = true;
            }
        }
        else
        {
            if (IsAWave && WaveCounter < 31)
            {
                WaveCounter++;
                Enemies.StandardSpawn(WaveCounter);
                IsAWave = false;
            }
            else
            {
                CheckEndGame(WaveCounter);
                TimeToWait = true;
            }
        }
    }

    public void CheckEndGame(int WaveCounter)
    {
        if (WaveCounter == 31)
        {
            PhotonNetwork.LoadLevel("WinScene");
        }
        bool Dead = false;
        // a true
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
}
