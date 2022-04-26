using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_enemy1 : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public GameObject gameObject;
    public CompositeCollider2D col;
    public float Xpos = 0;
    public float Ypos = 0;
    public int nbenemy = Game.AllEnemies.Count;
    public Vector2 pointspawn;
    // Start is called before the first frame update
    void Start()
    {
        new WaitForSeconds(10f);
        Enemies.WaveGenerator(1);
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
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
                        gameObject = gameObjects[0];
                        break;
                    case EnemyType.DOG:
                        gameObject = gameObjects[1];
                        break;
                    case EnemyType.BLOB:
                        gameObject = gameObjects[2];
                        break;
                    case EnemyType.BOSS:
                        gameObject = gameObjects[3];
                        break;
            }

            Instantiate(gameObject, pointspawn, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
    }
}
