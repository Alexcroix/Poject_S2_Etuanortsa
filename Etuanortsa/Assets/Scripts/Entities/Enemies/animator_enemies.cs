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
    public bool hit;
    // Start is called before the first frame update
    void Start()
    {
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (aiPath.desiredVelocity.x >= 0.01f && !hit)
        {
            x = true;
        }
        if (aiPath.desiredVelocity.x <= -0.01f && !hit)
        {
            x = false;
        }
        if (aiPath.desiredVelocity.y >= 0.01f && !hit)
        {
            y = true;
        }
        if (aiPath.desiredVelocity.y <= -0.01f && !hit)
        {
            y = false;
        }
        if (aiPath.reachedEndOfPath == true)
        {
            hit =true;
        }
        
        if (!aiPath.reachedEndOfPath)
        {
            hit = false;
        }
        
        
      
    }
    private void FixedUpdate()
    {
        animator.SetBool("x", x);
        animator.SetBool("y", y);
        animator.SetBool("hit", hit);
    }
}
