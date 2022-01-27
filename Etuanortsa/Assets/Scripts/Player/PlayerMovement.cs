using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Vector2 movement;
    
    public float mouse_x;
    public float mouse_y;
    public bool moving;
    public Camera camera;

    public float MovementSpeed = 10f;
        
    public Rigidbody2D rb;
    public Animator animator;
    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mouse_x = (camera.ScreenToViewportPoint(Input.mousePosition)).x - (float) 0.5;
        mouse_y = (camera.ScreenToViewportPoint(Input.mousePosition)).y - (float) 0.5;
        moving = movement != Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * MovementSpeed * Time.deltaTime);
        animator.SetFloat("mouse_x", mouse_x);
        animator.SetFloat("mouse_y", mouse_y);
        animator.SetBool("moving", moving);
    }
}
