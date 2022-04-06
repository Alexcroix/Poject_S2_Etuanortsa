using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRIGGER : MonoBehaviour
{
    bool test = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag =="spawn")
        {
            test = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "spawn")
        {
            test = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "spawn")
        {
            test = true;
        }
    }
}
