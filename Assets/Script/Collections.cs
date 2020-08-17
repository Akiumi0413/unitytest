using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collections : MonoBehaviour
{
    public void death()
    {
        FindObjectOfType<PlayerControl>().CollectionCount();
        Destroy(gameObject);
    }
}
