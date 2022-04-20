using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies
{
    public int difficulty;
    public List<EnemyType> enemiesList;

    public Enemies(int level)
    {
        this.difficulty = level;
        this.enemiesList = new List<EnemyType>();
    }
}
