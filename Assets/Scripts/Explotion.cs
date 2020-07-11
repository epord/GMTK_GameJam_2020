using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
    
    public float ExplotionDelay = 2.0f; //This implies a delay of 2 seconds.
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, ExplotionDelay);
    }

}
