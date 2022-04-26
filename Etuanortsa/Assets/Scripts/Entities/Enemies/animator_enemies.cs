using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class animator_enemies : MonoBehaviour
{
    public Animator animator;
    public AIPath aiPath;
    public bool x;
    public bool y;
    public bool move;
    // Start is called before the first frame update
    void Start()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (aiPath.desiredVelocity.x >= 0.01f && move)
        {
            x = true;
        }
        if (aiPath.desiredVelocity.x <= -0.01f && move)
        {
            x = false;
        }
        if (aiPath.desiredVelocity.y >= 0.01f && move)
        {
            y = true;
        }
        if (aiPath.desiredVelocity.y <= -0.01f && move)
        {
            y = false;
        }
        if (aiPath.reachedEndOfPath == true)
        {
            move =false;
        }
        
        if (!aiPath.reachedEndOfPath)
        {
            move = true;
        }
        
        
      
    }
    private void FixedUpdate()
    {
        animator.SetBool("x", x);
        animator.SetBool("y", y);
        animator.SetBool("move", move);
    }
}
