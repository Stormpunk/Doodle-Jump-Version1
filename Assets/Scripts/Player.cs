using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Player : MonoBehaviour
{
    public float moveX;
   public float moveSpeed;
    public Rigidbody2D rb2d;
    public Animator anim;
    public AudioSource aS;
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
    /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("solidPlatform") && rb2d.velocity.y <= 0f)
        {
            aS.Play();
            anim.SetTrigger("Bounce");
        }
    } */
    public void PlayerBounce()
    {
        aS.Play();
        anim.SetTrigger("Bounce");
    }
}
