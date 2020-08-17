using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement; 

public class PlayerControl : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    private Animator anime;
    public Collider2D coll;
    public Collider2D discoll;
    public float Speed = 10;
    public float jumpforce;
    public LayerMask ground;
    public Transform cellingcheck;
    public int collections = 0;
    public Text Collection_Num;
    private bool isHurt;//默認是false
    public AudioSource JumpAudio,CoinAudio,HurtAudio;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    
    void FixedUpdate()
    {
        if (!isHurt) 
        {
            Movement();

        }
        switchanime();
    }
    private void Update()
    {
        jump();
        Crouch();
    }

    void Movement()//移動
    {
        float Move=Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        if (Move != 0)//角色移動
        {
            rb.velocity = new Vector2(Move * Speed  * Time.fixedDeltaTime, rb.velocity.y);
            anime.SetFloat("running", Mathf.Abs(facedirection));//Abs(絕對值)

        }
        if (facedirection != 0)//角色方向
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
            
        }
    }
    void switchanime() //切換動畫效果
    {
        //anime.SetBool("idle", false);
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
                //anime.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anime.SetBool("falling", false);
            //anime.SetBool("idle", true);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision) //碰撞觸發器
    {
        if(collision.tag == "Cherry")//收集物品
        {
            collision.GetComponent<Animator>().Play("isGet");
            //Destroy(collision.gameObject);
            CoinAudio.Play();
            //collections ++;
            //Collection_Num.text = collections.ToString();
        }else if (collision.tag == "Gem")
        {
            collision.GetComponent<Animator>().Play("isGot");
            //Destroy(collision.gameObject);
            CoinAudio.Play();
            //collections ++;
            //Collection_Num.text = collections.ToString();
        }

        if (collision.tag == "Deathline")//死亡
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("restart", 0.5f);
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
                HurtAudio.Play();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(2, rb.velocity.y);
                HurtAudio.Play();
                isHurt = true;
            }
        }
    }
    
    void jump()
    {
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))//角色跳躍
        {

            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            JumpAudio.Play();
            anime.SetBool("jumping", true);
        }
        Crouch();
    }
    void Crouch()//趴下
    {
            if (!Physics2D.OverlapCircle(cellingcheck.position,0.2f,ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anime.SetBool("crouching", true);
                discoll.enabled = false;
            }
            else 
            {
                anime.SetBool("crouching", false);
                discoll.enabled = true;
            }
        }
    }
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Debug.Log("觸發");
    }

    public void CollectionCount()
    {
        collections++;
        Collection_Num.text = collections.ToString();
    }
}
