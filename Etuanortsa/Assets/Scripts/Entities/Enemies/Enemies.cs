using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public List<Enemy> AllEnemies;
    public readonly int BossStage = 30;

    public void WaveGeneration(int wave)
    {
        AllEnemies = new List<Enemy>();

        if (wave == BossStage)
        {
            AllEnemies.Add(new Enemy("Boss"));
        }
        else
        {
            int x = StantardSpawn(wave);

            for (int i = 0; i < x; i++)
            {
                AllEnemies.Add(new Enemy("Stantard"));
            }

            x = DogSpawn(wave);

            for (int i = 0; i < x; i++)
            {
                AllEnemies.Add(new Enemy("Dog"));
            }

            x = BlobSpawn(wave);

            for (int i = 0; i < x; i++)
            {
                AllEnemies.Add(new Enemy("Blob"));
            }
        }
    }

    /**public void SpawnEnemy(List<Enemy> AllEnemies)
     * {
     * 
     * }
    **/

    public static bool SpawnAbility(int range, int wave)
    {
        if (wave % range == 0)
        {
            return true;
        }

        return false;
    }

    public static int Spawn(int start, int wave, float coefficient, int minSpawn)
    {
        if (start == wave)
        {
            return minSpawn;
        }

        return (int)(start * coefficient + Spawn(start + 1, wave, coefficient, minSpawn));
    }

    public static int Spawn(int start, int wave, int range, float coefficient, int minSpawn)
    {
        if (start == wave)
        {
            return minSpawn;
        }

        return (int)(start * coefficient + Spawn(start + range, wave, range, coefficient, minSpawn));
    }

    public static int StantardSpawn(int wave)
    {
        return Spawn(1, wave, 1.2f, 40);
    }

    public static int DogSpawn(int wave)
    {
        if (SpawnAbility(5, wave))
        {
            return Spawn(5, wave, 5, 1.25f, 15);
        }

        return 0;
    }

    public static int BlobSpawn(int wave)
    {
        if (SpawnAbility(2, wave))
        {
            return Spawn(2, wave, 2, 1f, 10);
        }

        return 0;
    }
}
