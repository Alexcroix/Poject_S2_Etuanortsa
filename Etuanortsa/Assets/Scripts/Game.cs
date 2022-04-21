using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviourPunCallbacks
{
    public List<Joueur> joueurs = new List<Joueur>();

    private void Start()
    {
        joueurs.Add(GameObject.FindObjectOfType<Joueur>());
        foreach(Joueur j in joueurs)
        {
            print(j);
        }
    }
}
