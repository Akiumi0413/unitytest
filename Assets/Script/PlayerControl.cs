using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    private Animator anime;
    public Collider2D coll;
    public float Speed = 10;
    public float jumpforce;
    public LayerMask ground;
    public int collections = 0;
    public Text Collection_Num;
    private bool isHurt;//默認是false
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (!isHurt) 
        {
            Movement();
        }
        switchanime();
    }

   void Movement()
    {
        float move=Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        if (move != 0)//角色移動
        {
            rb.velocity = new Vector2(move * Speed /* * Time.deltaTime*/, rb.velocity.y);
            anime.SetFloat("running", Mathf.Abs(facedirection));//Abs(絕對值)

        }
        if (facedirection != 0)//角色方向
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
            
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))//角色跳躍
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpforce /* * Time.deltaTime*/);
            anime.SetBool("jumping", true);
        }
    }
    void switchanime() //切換動畫效果
    {
        anime.SetBool("idle", false);
        if(rb.velocity.y<0.1f && !coll.IsTouchingLayers(ground))
        {
            anime.SetBool("falling", true);
        }
        if (anime.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anime.SetBool("jumping", false);
                anime.SetBool("falling", true);
            }
        }
        else if (isHurt)
        {
            anime.SetBool("hurt", true);
            anime.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anime.SetBool("hurt", false);
                anime.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anime.SetBool("falling", false);
            anime.SetBool("idle", true);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision) //收集物品
    {
        if(collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            collections ++;
            Collection_Num.text = collections.ToString();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)//消滅敵人
    {
        if (collision.gameObject.tag == "Enemies")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            //Enemy_frog frog = collision.gameObject.GetComponent<Enemy_frog>();
            if (anime.GetBool("falling"))
            {
                enemy.jumpON();
                //Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anime.SetBool("jumping", true);
            }
            else if(transform.position.x<collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-2, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(2, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
