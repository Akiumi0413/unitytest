using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemy_eagle : Enemy
{
    private Rigidbody2D rb;
    //private Animator anime;
    private Collider2D coll;
    public Transform uppoint, downpoint;

    public bool isup=true;
    public int speed;
    public float upy, downy;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //anime = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        upy = uppoint.position.y;
        downy = downpoint.position.y;
        Destroy(uppoint.gameObject);
        Destroy(downpoint.gameObject);
    }

    
    void Update()
    {
        movement();
    }
    void movement()
    {
        if (isup)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if(transform.position.y > upy)
            {
                isup = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < downy)
            {
                isup = true;
            }
        }
    }
}
