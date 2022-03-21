using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    public Animator animator;
    public bool x;
    public bool y;

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            x = true;
        }
        if (aiPath.desiredVelocity.x <= -0.01f)
        {
            x = false;
        }
        if (aiPath.desiredVelocity.y >= 0.01f)
        {
            y = true;
        }
        if (aiPath.desiredVelocity.y <= -0.01f)
        {
            y = false;
        }
    }
}
