using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   protected Animator anime;
    protected AudioSource DeathAudio;
     protected virtual void Start()
    {
        anime=GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
    }

    public void death()
    {
        GetComponent<Collider2D>().enabled=false;
        Destroy(gameObject);
    }
    public void jumpON()
    {
        anime.SetTrigger("death");
        DeathAudio.Play();
    }
}
