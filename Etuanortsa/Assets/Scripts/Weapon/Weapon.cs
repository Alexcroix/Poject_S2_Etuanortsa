using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float moveSpeed = 10f;

    public Rigidbody2D weapon;
    public Sprite Left_weapon;
    public Sprite Right_weapon;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
    Vector3 tempPos;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        weapon.MovePosition(weapon.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 loorDir = mousePos;
        loorDir.x = loorDir.x - (float)0.5;
        loorDir.y = loorDir.y - (float)0.5;
        float angle = Mathf.Atan2(loorDir.y, loorDir.x) * Mathf.Rad2Deg - 180f;
        weapon.rotation = angle;

        tempPos = weapon.transform.position;

        if(loorDir.y > 0)
        {
            tempPos.z = 1;
        }
        else
        {
            tempPos.z = 0;
        }

        if (loorDir.x <= 0)
        {
            weapon.GetComponent<SpriteRenderer>().sprite = Left_weapon;
        }
        else
        {
            weapon.GetComponent<SpriteRenderer>().sprite = Right_weapon;
        }
        
        transform.position = tempPos;
    }
}

