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
    public int nbenemy = Game.AllEnemies.Count;
    public Vector3 pointspawn;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (Game.launchWave)
        {
            Enemies.WaveGenerator(Game.WaveCounter);
            StartCoroutine(EnemyDrop());
        }
    }

    IEnumerator EnemyDrop()
    {
        
        int i = 0;
        foreach (EnemyType enemy in Enemies.AllEnemies)
        {
            Xpos = Random.Range(-55, 24);
            Ypos = Random.Range(-14, -49);
            pointspawn.x = Xpos;
            pointspawn.y = Ypos;
            
            while (!col.OverlapPoint(pointspawn))
            {
                Xpos = Random.Range(-55, 24);
                Ypos = Random.Range(-14, -49);
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

            PhotonNetwork.Instantiate(Mob.name, pointspawn, Quaternion.identity);
            yield return new WaitForSeconds(1f);
            if (i == 1)
            {
                break;
            }
            i++;
        }
    }
}
