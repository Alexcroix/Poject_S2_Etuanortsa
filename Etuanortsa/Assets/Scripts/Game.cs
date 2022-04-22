using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviourPunCallbacks
{
    public static List<Joueur> joueurs = new List<Joueur>();
    
    public bool endGame(List<Joueur> joueur, int round)
    {
        bool rep = false;
        if (round == 31)
        {
            rep = true;
        }
        for (int i = 0; i < joueur.Count; i++)
        {
            if ((bool)joueur[i].PlayerProperties["alive"] == true)
            {
                rep = true;
            }
        }
        return rep;
    }
}
