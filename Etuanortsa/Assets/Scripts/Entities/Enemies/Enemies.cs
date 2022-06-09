using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public enum EnemyType
{
    STANDARD,
    DOG,
    BLOB,
    BOSS
}

public class Enemies : MonoBehaviour
{
    public static List<EnemyType> AllEnemies;
    public static readonly int BossStage = 30;
    
    public static void WaveGenerator(int wave)
    {
        AllEnemies = new List<EnemyType>();

        if (wave == BossStage)
        {
            AllEnemies.Add(EnemyType.DOG);
        }
        else
        {
            int x = StandardSpawn(wave);

            for (int i = 0; i < x; i++)
            {
                AllEnemies.Add(EnemyType.STANDARD);
            }

            x = BlobSpawn(wave);

            for (int i = 0; i < x; i++)
            {
                AllEnemies.Add(EnemyType.BLOB);
            }

            x = DogSpawn(wave);

            for (int i = 0; i < x; i++)
            {
                AllEnemies.Add(EnemyType.DOG);
            }

        }

        Shuffle(AllEnemies);
    }

    public static void Shuffle(List<EnemyType> enemies)
    {
        AllEnemies = new List<EnemyType>();

        var rnd = new Random();
        var randomized = enemies.OrderBy(item => rnd.Next());

        foreach (EnemyType enemy in randomized)
        {
            AllEnemies.Add(enemy);
        }
    }

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

    public static int StandardSpawn(int wave)
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