﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float moveX;
   public float moveSpeed;
    public Rigidbody2D rb2d;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    { 
        moveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;
    }
    private void FixedUpdate()
    {
        Vector2 velocity = rb2d.velocity;
        velocity.x = moveX ;
        rb2d.velocity = velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("solidPlatform"))
        {
            anim.SetTrigger("Bounce");
        }
    }
}
