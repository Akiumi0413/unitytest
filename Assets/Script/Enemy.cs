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
        Destroy(gameObject);
    }
    public void jumpON()
    {
        anime.SetTrigger("death");
        DeathAudio.Play();
    }
}
