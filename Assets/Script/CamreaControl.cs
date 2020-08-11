using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreaControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Player.position.x,0, -10f);
    }
}
