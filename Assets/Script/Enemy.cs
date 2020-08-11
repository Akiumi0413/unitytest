using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   protected Animator anime;
     protected virtual void Start()
    {
        anime=GetComponent<Animator>();
    }

    public void death()
    {
        Destroy(gameObject);
    }
    public void jumpON()
    {
        anime.SetTrigger("death");
        //Debug.Log("Pass");
    }
}
