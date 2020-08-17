using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parellax : MonoBehaviour
{
    public Transform cam;
    public float Moverate;
    private float startpointx,startpointy;
    public bool locky;//預設false
    void Start()
    {
        startpointx = transform.position.x;
        startpointy = transform.position.y;
    }

    
    void Update()
    {
        if (locky)
        {
            transform.position = new Vector2(startpointx + cam.position.x * Moverate, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(startpointx + cam.position.x * Moverate, startpointy + cam.position.y*Moverate);
        }
        
    }
}
