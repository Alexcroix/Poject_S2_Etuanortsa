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

    private void Update()
    {
        foreach(Joueur j in joueurs)
        {
            print(j);
        }
    }
}
