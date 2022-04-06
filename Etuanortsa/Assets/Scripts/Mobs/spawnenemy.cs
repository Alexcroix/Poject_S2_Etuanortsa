using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnenemy : MonoBehaviour
{
    private int i =0;
    private int j =0;   
    public GameObject enemy;
    public float xPos;
    public float yPos;
    public int nbenemy;
    public bool test= true;
    private List<double> zonespawn = new List<double>();
    Rigidbody2D boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Enemyspawn());

    }
    IEnumerator Enemyspawn ()
    {
        
        while( i < nbenemy)
        {
            xPos = Random.Range(1, 50);
            yPos = Random.Range(1, 50);
            this.transform.position = new Vector3(xPos, yPos, 0);


        }
        xPos = -10000000000000;
        yPos = -10000000000000;
        yield return null;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Instantiate(enemy, new Vector3(xPos, yPos, 0), Quaternion.identity);
        i++;

    }




}
