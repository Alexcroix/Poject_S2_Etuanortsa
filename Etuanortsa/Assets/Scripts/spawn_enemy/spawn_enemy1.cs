using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_enemy1 : MonoBehaviour
{
    public Enemy enemy;
    public Collider2D col;
    public float Xpos = 0;
    public float Ypos = 0;
    public int nbenemy = Game.AllEnemies.Count;
    public Vector2 pointspawn;
    // Start is called before the first frame update
    void Start()
    {
        new WaitForSeconds(10f);
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        for (int i = 0; i < nbenemy; i++)
        {
            enemy = Game.AllEnemies[i];
            while(i%5 != 0)
            {
                Xpos = Random.Range(0, 50);
                Ypos = Random.Range(0, 50);
                pointspawn.x = Xpos;
                pointspawn.y = Ypos;
                while (!col.OverlapPoint(pointspawn))
                {
                    Xpos = Random.Range(0, 50);
                    Ypos = Random.Range(0, 50);
                    pointspawn.x = Xpos;
                    pointspawn.y = Ypos;
                }
                Instantiate(enemy, pointspawn, Quaternion.identity);
            }
            yield return new WaitForSeconds(1f);

        }
    }
}
