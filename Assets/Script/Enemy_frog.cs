using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : Enemy
{ 
    private Rigidbody2D rb;
    //private Animator anime;
    private Collider2D coll;

    public Transform leftpoint, rightpoint;
    public float speed,jumpforce;
    public float leftx, rightx;
    public LayerMask ground;

    private bool Faceleft = true;
 
     protected override void Start()
    {
        base.Start();  
        rb = GetComponent<Rigidbody2D>();
        //anime = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    
    void Update()
    {
        //movement();
        switchanime();
    }
    void movement()
    {
        if (Faceleft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                anime.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpforce);
            }
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                anime.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpforce);
            }
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

    void switchanime()
    {
        if (anime.GetBool("jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                anime.SetBool("jumping", false);
                anime.SetBool("falling", true);
            }
        }
        if(coll.IsTouchingLayers(ground) && anime.GetBool("falling"))
        {
            anime.SetBool("falling", false);
        }
    }
    /*void death()
    {
        Destroy(gameObject);
    }
    public void jumpON()
    {
        anime.SetTrigger("death");
        //Debug.Log("Pass");
    }*/
}
