using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class IA : MonoBehaviour
{

    public AIPath aiPath;
    public float stoppingDistance;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <=stoppingDistance)
        {
            // attack the player
            
        }

    }
   

}
