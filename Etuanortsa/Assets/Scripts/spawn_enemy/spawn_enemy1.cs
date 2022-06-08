using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class spawn_enemy1 : MonoBehaviourPunCallbacks
{
    public List<GameObject> gameObjects;
    public GameObject Mob;
    public CompositeCollider2D col;
    public float Xpos = 0;
    public float Ypos = 0;
    
    public Vector3 pointspawn;
    public List<(int, int)> coor_salle;  //
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (Game.launchWave && PhotonNetwork.IsMasterClient)
        {
            
            coor_salle = new List<(int, int)> {(-54,-44),(-11,-14), (-11,-48), (25,-20), (25,-45), (81,-17), (92,-41), (129,35), (35,8), (93,43), (-13,11), (34,34), (-58,-8), (-13,28)};
            Enemies.WaveGenerator(Game.WaveCounter);
            StartCoroutine(EnemyDrop());
        }
    }

    IEnumerator EnemyDrop()
    {
        foreach (EnemyType enemy in Enemies.AllEnemies)
        {
            
            int rng = Random.Range(0, 7);       //
            rng *= 2;                       //
            (int, int) coor1 = coor_salle[rng];  //
            (int, int) coor2 = coor_salle[rng+1];      //
            Xpos = Random.Range(coor1.Item1, coor2.Item1);           // faut voir si ca marche
            Ypos = Random.Range(coor1.Item2, coor2.Item2);
            //Xpos = Random.Range(-55, 24); 
            //Ypos = Random.Range(-14, -49);
            pointspawn.x = Xpos;
            pointspawn.y = Ypos;
            
            while (!col.OverlapPoint(pointspawn))
            {
                Xpos = Random.Range(coor1.Item1, coor2.Item1);           // faut voir si ca marche
                Ypos = Random.Range(coor1.Item2, coor2.Item2);
                //Xpos = Random.Range(-55, 24);
                //Ypos = Random.Range(-14, -49);
                pointspawn.x = Xpos;
                pointspawn.y = Ypos;
            }

            switch (enemy)
            {
                default:
                    case EnemyType.STANDARD:
                        Mob = gameObjects[0];
                        break;
                    case EnemyType.DOG:
                        Mob = gameObjects[1];
                        break;
                    case EnemyType.BLOB:
                        Mob = gameObjects[2];
                        break;
                    case EnemyType.BOSS:
                        Mob = gameObjects[3];
                        break;
            }

            PhotonNetwork.InstantiateRoomObject(Mob.name, pointspawn, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            

            
        }
    }
}
